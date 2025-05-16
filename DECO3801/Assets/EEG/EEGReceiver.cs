using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class EEGReceiver : MonoBehaviour
{
    TcpClient client;
    NetworkStream stream;

    public void Connect()
    {
        client = new TcpClient("127.0.0.1", 12345);
        stream = client.GetStream();
        Debug.Log("Connected to Python TCP server!");
    }

    void Update()
    {
        if (stream != null && stream.DataAvailable)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytes = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytes);
                Debug.Log("EEG JSON: " + message);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Read error: " + e.Message);
            }
        }
    }
}
