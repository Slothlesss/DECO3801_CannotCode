## Requirement
- This example works with Python >= 3.7
- Install websocket client via  `pip install websocket-client`
- Install python-dispatch via `pip install python-dispatch`
- Install pandas via `pip install pandas`
- Install joblib via `pip install joblib`
- Install numpy via `pip install numpy`
- Install scikit-learn via `pip install scikit-learn`

## Files
- `export_data.py`: Generates the marked CSV files required for the experiment.  
- `train_model.py`: Loads the exported CSV, trains the stress classification model and scaler, and saves them.  
- `predict_level.py`: Loads the saved model (`stress_model.pkl`) and scaler (`scaler.pkl`) to predict stress levels on new data.  (base on `export_data.py`)
- `servery.py`: Sends the predicted stress level to the Unity client in real time via WebSocket/TCP.  (base on `predict_level.py` ) 
- `scaler.pkl`: The `StandardScaler` object saved by `train_model.py`.  
- `stress_model.pkl`: The stress classification model ( `RandomForestClassifier`) saved by `train_model.py`.  
- `README.md`: Project documentation, including installation and usage instructions.  

## Usage
- run `send_to_unity.py` then run unity


## References
- https://github.com/Emotiv/cortex-example 
- https://emotiv.gitbook.io/cortex-api
- https://github.com/scikit-learn/scikit-learn
- tcp ip communication