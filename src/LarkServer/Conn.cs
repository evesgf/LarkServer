using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace LarkServer
{
    public class Conn
    {
        //常量
        public const int BUFFER_SIZE = 1024;
        //Socket
        public Socket socket;
        //是否使用
        public bool isUse = false;
        //Buff
        public byte[] readBuff = new byte[BUFFER_SIZE];
        public int buffCount = 0;

        public Conn()
        {
            readBuff = new byte[BUFFER_SIZE];
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="socket"></param>
        public void Init(Socket socket)
        {
            this.socket = socket;
            isUse = true;
            buffCount = 0;
        }

        /// <summary>
        /// 缓冲区剩余字节数
        /// </summary>
        /// <returns></returns>
        public int BuffRemain()
        {
            return BUFFER_SIZE - buffCount;
        }

        /// <summary>
        /// 获取客户端地址
        /// </summary>
        /// <returns></returns>
        public string GetAddress()
        {
            if (!isUse) return"[--警告--]无法获取地址";

            return socket.RemoteEndPoint.ToString();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (!isUse) return;

            Console.WriteLine("[断开连接]" + GetAddress());

            //TODO:socket.Close();
            //socket.Close();
            isUse = false;
        }
    }
}
