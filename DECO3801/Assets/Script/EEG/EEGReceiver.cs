using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class EEGReceiver : MonoBehaviour
{
    TcpClient client;
    NetworkStream stream;
    Message message;

    // Current levels
    private int currentFrustration;
    private int currentFatigue;
    private int currentFocus;

    // Latest received levels
    private int receivedFrustration;
    private int receivedFatigue;
    private int receivedFocus;

    // Timers
    private float frustrationTimer;
    private float fatigueTimer;
    private float focusTimer;

    private const float lockDuration = 7f;

    private void Start()
    {
        client = new TcpClient("127.0.0.1", 12345);
        stream = client.GetStream();
        Debug.Log("Connected to Python TCP server!");

        currentFrustration = receivedFrustration = 0;
        currentFatigue = receivedFatigue = 0;
        currentFocus = receivedFocus = 0;
    }

    void Update()
    {
        ReceiveMessage();
        UpdateEmotionalState(ref currentFrustration, ref receivedFrustration, ref frustrationTimer, "Frustration");
        UpdateEmotionalState(ref currentFatigue, ref receivedFatigue, ref fatigueTimer, "Fatigue");
        UpdateEmotionalState(ref currentFocus, ref receivedFocus, ref focusTimer, "Focus");
        GameManager.Instance.SetFrustration((Frustration)currentFrustration);
        GameManager.Instance.SetFatigue((Fatigue)currentFrustration);
        GameManager.Instance.SetFocus((Focus)currentFrustration);
    }

    private void ReceiveMessage()
    {
        if (stream != null && stream.DataAvailable)
        {
            byte[] buffer = new byte[1024];
            int bytes = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytes);
            try
            {
                message = JsonUtility.FromJson<Message>(response);
                receivedFrustration = message.frustration;
                receivedFatigue = message.fatigue;
                receivedFocus = message.focus;
                Debug.Log($"Received: {response}");
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error: " + e.Message);
            }
        }

    }

    private void UpdateEmotionalState(ref int current, ref int received, ref float timer, string name)
    {
        timer += Time.deltaTime;

        if (timer < lockDuration)
            return;

        if (current != received)
        {
            current = received;
            timer = 0f; // reset timer to lock again
            Debug.Log($"{name} level changed to {current}");
        }
    }
}
