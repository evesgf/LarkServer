using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using UnityEngine.UI;
using System.Text;

public class Net : MonoBehaviour {

    //与服务端的套接字
    Socket socket;
    //接收缓冲区
    const int BUFFER_SIZE = 1024;
    byte[] readBuff = new byte[BUFFER_SIZE];

    //服务端的IP和端口
    public InputField hostInput;
    public InputField portInput;
    //文本框
    public Text recvText;
    public Text clientText;

    public void Connention()
    {
        //Socket
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //Connect
        string host = hostInput.text;
        int port = int.Parse(portInput.text);

        socket.Connect(host, port);

        clientText.text = "[客户端地址]" + socket.LocalEndPoint.ToString();

        //Send
        string str = "Hello for Client";
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        socket.Send(bytes);

        //Recv
        int count = socket.Receive(readBuff);
        str = Encoding.UTF8.GetString(readBuff, 0, count);

        recvText.text = str;

        //Close
        socket.Close();
    }
}
