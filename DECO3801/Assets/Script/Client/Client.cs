using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

public class Client : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private Thread receiveThread;

    public string serverIP = "127.0.0.1"; // or your Python IP
    public int port = 5050;

    public void ConnectToServer()
    {
        try
        {
            client = new TcpClient(serverIP, port);
            stream = client.GetStream();

            Debug.Log("Connected to Python server");

            receiveThread = new Thread(ReceiveData);
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }
        catch (SocketException e)
        {
            Debug.LogError("Socket error: " + e);
        }
    }

    public void SendMessageToServer(string message)
    {
        if (stream != null)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Debug.Log("Sent: " + message);
        }
    }

    private void ReceiveData()
    {
        while (true)
        {
            if (stream == null) break;

            // Read 4-byte length prefix
            byte[] lengthBytes = new byte[4];
            int read = stream.Read(lengthBytes, 0, 4);
            if (read == 0) break;

            int messageLength = BitConverter.ToInt32(lengthBytes, 0);

            // Read the actual message
            byte[] messageBytes = new byte[messageLength];
            int totalRead = 0;

            while (totalRead < messageLength)
            {
                int chunkRead = stream.Read(messageBytes, totalRead, messageLength - totalRead);
                if (chunkRead == 0) break;
                totalRead += chunkRead;
            }

            string response = Encoding.UTF8.GetString(messageBytes);
            Debug.Log("Received: " + response);

        }
    }
}
