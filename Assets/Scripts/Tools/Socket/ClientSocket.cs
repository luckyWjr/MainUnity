using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace Tool {

    public class ClientSocket : SingleClass<ClientSocket> {

        public delegate void OnGetReceive(string message);//接收到消息的委托
        public OnGetReceive onGetReceive;

        byte[] m_result = new byte[1024];
        Socket m_clientSocket;

        //是否已连接的标识  
        public bool isConnected {
            get {
                return m_clientSocket.Connected;
            }
        }

        public ClientSocket() {
            m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>  
        /// 连接指定IP和端口的服务器  
        /// </summary>  
        /// <param name="ip"></param>  
        /// <param name="port"></param>  
        public void ConnectServer(string ip, int port) {
            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

            try {
                m_clientSocket.Connect(ipEndPoint);
                Debug.Log("连接服务器成功");
                Thread thread = new Thread(RecieveMessage);
                thread.Start();
            } catch {
                Debug.Log("连接服务器失败");
                return;
            }
        }

        /// <summary>  
        /// 发送数据给服务器  
        /// </summary>  
        public void SendMessage(string data) {
            if(isConnected == false) {
                return;
            }
            try {
                NetBufferWriter writer = new NetBufferWriter();
                writer.WriteString(data);
                m_clientSocket.Send(writer.Finish());
            } catch {
                Disconnect();
            }
        }

        /// <summary>  
        /// 发送数据给服务器  
        /// </summary>  
        public void Disconnect() {
            if(isConnected) {
                m_clientSocket.Shutdown(SocketShutdown.Both);
                m_clientSocket.Close();
            }
        }

        /// <summary>  
        /// 接收服务器端Socket的消息  
        /// </summary>  
        void RecieveMessage() {
            while(isConnected) {
                try {
                    int receiveLength = m_clientSocket.Receive(m_result);
                    NetBufferReader reader = new NetBufferReader(m_result);
                    string data = reader.ReadString();
                    Debug.Log("服务器返回数据：" + data);
                    if(onGetReceive != null) {
                        onGetReceive(data);
                    }

                } catch(Exception ex) {
                    Console.WriteLine(ex.Message);
                    Disconnect();
                    break;
                }
            }
        }
    }
}