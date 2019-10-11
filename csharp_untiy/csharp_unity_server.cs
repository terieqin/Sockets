using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class csharp_unity_server : MonoBehaviour
{
    private static byte[] result = new byte[1024];

    private static int myPort = 8885;
    static Socket serverSocket;

    void Start()
    {
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Bind(new IPEndPoint(ip, myPort));
        serverSocket.Listen(10);

        Thread myThread = new Thread(ListenClientConnect);
        myThread.Start();
    }

    private static void ListenClientConnect()
    {
        while(true)
        {
            Socket clientSocket = serverSocket.Accept();
            clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
            Thread receiveThread = new Thread(ReceiveMessage);
            receiveThread.Start(clientSocket);
        }
    }

    private static void ReceiveMessage(object clientSocket)
    {
        Socket myClientSocket = (Socket)clientSocket;
        while (true)
        {
            try
            {
                int receiveNumber = myClientSocket.Receive(result);
                Debug.Log("接收客户端{0}消息{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(result, 0, receiveNumber));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                myClientSocket.Shutdown(SocketShutdown.Both);
                myClientSocket.Close();
                break;
            }
        }
    }
}