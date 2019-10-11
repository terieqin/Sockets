using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class ClientSocket : MonoBehaviour
{
    public Text text;
    private static byte[] result = new byte[1024];
    // Start is called before the first frame update
    void Start()
    {
        //设定服务器IP地址 
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(new IPEndPoint(ip, 8889));
            Debug.Log("连接服务器成功");
        }
        catch
        {
            Debug.Log("连接服务器失败，请按回车键退出！");
            return;
        }
        //通过clientSocket接收数据 
        int receiveLength = clientSocket.Receive(result);
        Debug.Log(string.Format("接收服务器消息：{0}", Encoding.ASCII.GetString(result, 0, receiveLength)));
        //通过 clientSocket 发送数据 
        for (int i = 0; i < 10; i++)
        {
            try
            {
                Thread.Sleep(1000);    //等待1秒钟 
                string sendMessage = "client send Message Hellp" + DateTime.Now;
                clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                Debug.Log("向服务器发送消息：{0}" + sendMessage);
            }
            catch
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                break;
            }
        }
        Debug.Log("发送完毕，按回车键退出");
        Console.ReadLine();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
