import websocket
import ssl
import json
import threading
import time
import pandas as pd
from datetime import datetime
import joblib
import numpy as np

CLIENT_ID = "NS3Lof9vEO5RFP4a08sMQskVJAym1A4333oIGEr6"
CLIENT_SECRET = "TEYPdQVHWgXcc4HZDDG6WZreO7iaD0ZDyweoCu0yuFts2T2MaeBlxaWqm3WxQHrMvjRfAO1THWJ6PT9KCvTypYSlx4TUL3vToghg3Oze7bDN6vCYaNpnbFvWuzSGtK1c"

class CortexClient:
    def __init__(self):
        self.ws = None
        self.auth_token = None
        self.headset_id = None
        self.session_id = None
        self.request_id = 1
        self.lock = threading.Lock()
        self.data = []
        self.columns = []
        self.model = joblib.load('stress_model.pkl')
        self.scaler = joblib.load('scaler.pkl')
        self.buffer = []
        self.sampling_rate = 8  #8hz
        self.window_size = 3 * self.sampling_rate
        self.last_frustration_time = 0  
        self.columns_pow = []
        self.columns_met = []
        self.last_fatigue_time = 0
        self.last_focus_time = 0

    def connect(self):
        self.ws = websocket.WebSocketApp(
            "wss://localhost:6868",
            on_message=self.on_message,
            on_error=self.on_error,
            on_close=self.on_close,
            on_open=self.on_open
        )
        self.ws.run_forever(sslopt={"cert_reqs": ssl.CERT_NONE})

    def send_request(self, method, params=None):
        if params is None:
            params = {}
        with self.lock:
            request = {
                "jsonrpc": "2.0",
                "id": self.request_id,
                "method": method,
                "params": params
            }
            self.request_id += 1
        self.ws.send(json.dumps(request))

    def on_open(self, ws):
        print(" WebSocket opened")
        # ask for access
        self.send_request("requestAccess", {
            "clientId": CLIENT_ID,
            "clientSecret": CLIENT_SECRET
        })

    def on_message(self, ws, message):
        response = json.loads(message)
        # deal with the response
        if "id" in response:
            method_id = response["id"]
            result = response.get("result", {})
            if method_id == 1:
                # requestAccess response
                print("ask for access")
                # authorize
                self.send_request("authorize", {
                    "clientId": CLIENT_ID,
                    "clientSecret": CLIENT_SECRET,
                    "debit": 1
                })
            elif method_id == 2:
                # authorize response
                self.auth_token = result.get("cortexToken")
                print(f"get token：{self.auth_token}")
                # queryHeadsets
                self.send_request("queryHeadsets")
            elif method_id == 3:
                # queryHeadsets response
                headsets = result
                if headsets:
                    self.headset_id = headsets[0]["id"]
                    print(f"find device ，ID：{self.headset_id}")
                    # connect to the headset
                    self.send_request("controlDevice", {
                        "command": "connect",
                        "headset": self.headset_id
                    })
                else:
                    print("finf nothing")
            elif method_id == 4:
                # controlDevice (connect) response
                print("connected to headset")
                # createSession
                self.send_request("createSession", {
                    "cortexToken": self.auth_token,
                    "headset": self.headset_id,
                    "status": "active"
                })
            elif method_id == 5:
                # createSession response
                self.session_id = result.get("id")
                print(f"session created，ID：{self.session_id}")
                # subscribe to pow data stream
                self.send_request("subscribe", {
                    "cortexToken": self.auth_token,
                    "session": self.session_id,
                    "streams": ["pow","met"]
                })
            elif method_id == 6:
                # subscribe response
                print("subscribed to pow data stream")
                # get cols
                success = result.get("success", [])
                for stream in success:
                    if stream.get("streamName") == "pow":
                        self.columns = stream.get("cols", [])
                        print(f"data tag：{self.columns}")
                    elif stream.get("streamName") == "met":
                        self.columns_met = stream.get("cols", [])
                        print(f"met columns: {self.columns_met}")
        elif "pow" in response:
            # deal with pow data
            pow_values = response["pow"]
            timestamp = response.get("time", time.time())
            if not self.columns:
                print("data tag is empty")
                return
            
            data_dict = dict(zip(self.columns, pow_values))
            
            af3_alpha = data_dict.get("AF3/alpha")
            af3_betal = data_dict.get("AF3/betaL")
            af3_betah = data_dict.get("AF3/betaH")
            af3_theta = data_dict.get("AF3/theta")
            af4_alpha = data_dict.get("AF4/alpha")
            af4_betal = data_dict.get("AF4/betaL")
            af4_betah = data_dict.get("AF4/betaH")
            af4_theta = data_dict.get("AF4/theta")

            af3_beta = af3_betal + af3_betah
            af4_beta = af4_betal + af4_betah
            self.buffer.append({
                "AF3_alpha": af3_alpha,
                "AF3_beta": af3_beta,
                "AF3_theta": af3_theta,
                "AF4_alpha": af4_alpha,
                "AF4_beta": af4_beta,
                "AF4_theta": af4_theta
            })
            self.frustration_level()

        elif "met" in response:
            met_values = response["met"]
            if not self.columns_met:
                return  
        
            data_dict_met = dict(zip(self.columns_met, met_values))
            
            relax = data_dict_met.get("rel", 0)
            interest = data_dict_met.get("int", 0)

            self.fatigue_level(relax)
            self.focus_level(interest)
            
           

            self.buffer.append({
                "relax": relax,
                "interest": interest
            })
            
    def frustration_level(self):
        current_time = time.time()
        if len(self.buffer) >= self.window_size and current_time - self.last_frustration_time >= 1:
            segment = pd.DataFrame(self.buffer[-self.window_size:])
            features = self.extract_features(segment)
            features_scaled = self.scaler.transform([features])
            prediction = self.model.predict(features_scaled)[0]
            
            label_map = {0: 'Relax', 1: 'Mild Stress', 2: 'High Stress'}
            frustration_map = {0: 2, 1: 3, 2: 4}
            label = label_map[prediction]
            frustration = frustration_map[prediction]

            print(f"real time prediction：{label} → frustration={frustration}")
            self.last_frustration_time = current_time
            return frustration

    def fatigue_level(self, relax):
        current_time = time.time()

        if current_time - self.last_fatigue_time < 1:
            return None 
        
        self.last_fatigue_time = current_time

        if 0 < relax < 0.20:
            print(f"relax={relax:.2f} → fatigue=4")
            return 4
        elif 0.20 <= relax < 0.40:
            print(f"relax={relax:.2f} → fatigue=3")
            return 3
        elif relax >= 0.40:
            print(f"relax={relax:.2f} → fatigue=2")
            return 2
        
    def focus_level(self, interest):
        current_time = time.time()

        if current_time - self.last_focus_time < 1:
            return None 
        
        self.last_focus_time = current_time

        if 0 < interest < 0.20:
            print(f"interest={interest:.2f} → focus=2")
            return 2
        elif 0.20 <= interest < 0.40:
            print(f"interest={interest:.2f} → focus=3")
            return 3
        elif interest >= 0.40:
            print(f"interest={interest:.2f} → focus=4")
            return 4
    
    
    def on_error(self, ws, error):
        print(f"erro ：{error}")

    def on_close(self, ws, close_status_code, close_msg):
        print("connection closed")

    def extract_features(self, segment):
        af3_alpha = segment['AF3_alpha']
        af4_alpha = segment['AF4_alpha']
        af3_beta = segment['AF3_beta']
        af4_beta = segment['AF4_beta']
        af3_theta = segment['AF3_theta']
        af4_theta = segment['AF4_theta']

        alpha = af3_alpha.mean() + af4_alpha.mean()
        beta = af3_beta.mean() + af4_beta.mean()
        theta = af3_theta.mean() + af4_theta.mean()

        return [
            alpha / (beta + 1e-6),
            theta / (beta + 1e-6),
            alpha / 2,
            beta / 2,
            theta / 2,
            (af3_alpha.std() + af4_alpha.std()) / 2,
            (af3_beta.std() + af4_beta.std()) / 2,
            (af3_theta.std() + af4_theta.std()) / 2
        ]

if __name__ == "__main__":
    client = CortexClient()
    client.connect()
