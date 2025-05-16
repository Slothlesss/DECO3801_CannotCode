import pandas as pd
import numpy as np
import glob
import os
import joblib
from sklearn.ensemble import RandomForestClassifier
from sklearn.preprocessing import StandardScaler
from sklearn.model_selection import train_test_split
from sklearn.metrics import classification_report

label_map = {'relax': 0, 'mild': 1, 'high': 2}

# read all csv files in the current directory
all_files = glob.glob("*.csv")
all_data = []

for file in all_files:
    filename = os.path.basename(file).lower()
    label = None
    for key in label_map:
        if key in filename:
            label = label_map[key]
            break
    if label is None:
        print(f" file {file} does not match any label, skipping...")
        continue

    df = pd.read_csv(file)
    df = df.iloc[:, 1:]  # delete the first column
    df['AF3_beta'] = df['AF3_betal'] + df['AF3_betah']
    df['AF4_beta'] = df['AF4_betal'] + df['AF4_betah']
    df.drop(columns=['AF3_betal', 'AF3_betah', 'AF4_betal', 'AF4_betah'], inplace=True)
    df['label'] = label
    all_data.append(df)

# combine all dataframes into one
full_df = pd.concat(all_data, ignore_index=True)

# get the columns of interest
def extract_features(segment):
    af3_alpha = segment['AF3_alpha']
    af4_alpha = segment['AF4_alpha']
    af3_beta = segment['AF3_beta']
    af4_beta = segment['AF4_beta']
    af3_theta = segment['AF3_theta']
    af4_theta = segment['AF4_theta']

    alpha = af3_alpha.mean() + af4_alpha.mean()
    beta = af3_beta.mean() + af4_beta.mean()
    theta = af3_theta.mean() + af4_theta.mean()

    return [alpha / (beta + 1e-6), 
            theta / (beta + 1e-6),alpha / 2,
            beta / 2,
            theta / 2,
            (af3_alpha.std() + af4_alpha.std()) / 2,     # alpha std
            (af3_beta.std() + af4_beta.std()) / 2,       # beta std
            (af3_theta.std() + af4_theta.std()) / 2      # theta std
    ]

# sliding window parameters
sampling_rate = 8
window_size = 3 * sampling_rate
step_size = sampling_rate

X = []
y = []

for start in range(0, len(full_df) - window_size + 1, step_size):
    end = start + window_size
    segment = full_df.iloc[start:end]
    features = extract_features(segment)
    label = segment['label'].mode()[0]
    X.append(features)
    y.append(label)

X = np.array(X)
y = np.array(y)

# normalize the data
scaler = StandardScaler()
X_scaled = scaler.fit_transform(X)

X_train, X_test, y_train, y_test = train_test_split(
    X_scaled, y, test_size=0.2, random_state=42
)

clf = RandomForestClassifier()
clf.fit(X_train, y_train)

# evaluate the model
y_pred = clf.predict(X_test)
print(classification_report(y_test, y_pred))

joblib.dump(clf, 'stress_model.pkl')
joblib.dump(scaler, 'scaler.pkl')
print("model saved as stress_model.pkl")
print("normalizer saved as scaler.pkl")
