import websocket
import ssl
import json
import threading
import time
import pandas as pd
from datetime import datetime

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
                    "streams": ["pow"]
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
            self.data.append({
                "Timestamp": datetime.fromtimestamp(timestamp),
                "AF3_alpha": af3_alpha,
                "AF3_betal": af3_betal,
                "AF3_betah": af3_betah,
                "AF3_theta": af3_theta,
                "AF4_alpha": af4_alpha,
                "AF4_betal": af4_betal,
                "AF4_betah": af4_betah,
                "AF4_theta": af4_theta
            })
            print(f"record ：{self.data[-1]}")
            
            if len(self.data) >= 480:
                df = pd.DataFrame(self.data)
                df.to_csv("af3_af4_alpha_beta.csv", index=False)
                print("export af3_af4_alpha_beta.csv")
                self.ws.close()

    def on_error(self, ws, error):
        print(f"erro ：{error}")

    def on_close(self, ws, close_status_code, close_msg):
        print("connection closed")

if __name__ == "__main__":
    client = CortexClient()
    client.connect()
