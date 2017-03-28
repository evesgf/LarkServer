using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LarkServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //中文支持
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.WriteLine("LarkServer");

            //socket
            //创建一个套接字（地址簇，套接字类型，套接字协议）
            Socket listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            //Bind
            //将给listenfd套接字绑定IP和端口
            IPAddress ipAdr = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEp = new IPEndPoint(ipAdr, 1234);
            listenfd.Bind(ipEp);
            
            //Listen
            //开启监听等待客户端连接，参数指定队列中最多可容纳等待接收的连接数，0表示不受限
            listenfd.Listen(0);
            Console.WriteLine("[服务器]启动成功");

            while (true)
            {
                //Accept
                //接收客户端连接，本例为阻塞方法，当没有客户端连接时进程阻塞
                //当接收连接时Accept返回一个新的客户端Spcket，listenfd接收连接
                //connfd处理接收客户端的数据
                Socket connfd = listenfd.Accept();
                Console.WriteLine("[服务器]Accept");

                //Recv
                //通过connfd接收客户端数据，也是阻塞方法
                //带一个byte[]类型的参数，存储接收到的数据
                byte[] readBuff = new byte[1024];
                int count = connfd.Receive(readBuff);
                string str = Encoding.UTF8.GetString(readBuff, 0, count);
                Console.WriteLine("[服务器接收]" + str);

                //Send
                //服务器通过connfi.Send发送数据，byte[]类型参数为要发送的内容
                //返回值为发送数据的长度
                byte[] bytes = Encoding.UTF8.GetBytes("[serv echo]" + str+"["+DateTime.Now.ToString() + "");
                connfd.Send(bytes);
            }

            Console.ReadKey(true);
        }
    }
}
