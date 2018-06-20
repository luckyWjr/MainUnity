using SocketIO;
using System.Collections.Generic;
using UnityEngine;

namespace Tool {

	public class SocketManager : MonoBehaviour {

        SocketIOComponent m_socket;

		void Start () {
            m_socket = GetComponent<SocketIOComponent>();

            if(m_socket != null) {

                //系统的事件
                m_socket.On("open", OnSocketOpen);
                m_socket.On("error", OnSocketError);
                m_socket.On("close", OnSocketClose);
                //自定义的事件
                m_socket.On("ClientListener", OnClientListener);

                Invoke("SendToServer", 3);
            }
        }

        public void SendToServer() {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["email"] = "some@email.com";
            m_socket.Emit("ServerListener", new JSONObject(data), OnServerListenerCallback);

            //断开连接,会触发close事件
            //socket.Close();
        }

        #region 注册的事件

        public void OnSocketOpen(SocketIOEvent ev) {
            Debug.Log("OnSocketOpen updated socket id " + m_socket.sid);
        }

        public void OnClientListener(SocketIOEvent e) {
            Debug.Log(string.Format("OnClientListener name: {0}, data: {1}", e.name, e.data));
        }

        public void OnSocketError(SocketIOEvent e) {
            Debug.Log("OnSocketError: " + e.name + " " + e.data);
        }

        public void OnSocketClose(SocketIOEvent e) {
            Debug.Log("OnSocketClose: " + e.name + " " + e.data);
        }

        #endregion

        public void OnServerListenerCallback(JSONObject json) {
            Debug.Log(string.Format("OnServerListenerCallback data: {0}", json));
        }
    }
}