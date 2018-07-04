using Tool;
using UnityEngine;
using UnityEngine.UI;

namespace Examples {

	public class TestSocket : MonoBehaviour {

        [SerializeField] Button m_connectBtn;
        [SerializeField] InputField m_input;
        [SerializeField] Button m_sendBtn;
        [SerializeField] Button m_disconnectBtn;
        [SerializeField] Text m_receiveText;

        string m_receiveMessage = "wait...";

        void Start () {
            m_connectBtn.onClick.AddListener(SocketConnect);
            m_sendBtn.onClick.AddListener(SocketSendMessage);
            m_disconnectBtn.onClick.AddListener(SocketDisconnect);

            ClientSocket.instance.onGetReceive = ShowReceiveMessage;
        }
		
		void SocketConnect() {
            ClientSocket.instance.ConnectServer("127.0.0.1", 8078);
        }

        void SocketSendMessage() {
            string content = m_input.text;
            if(!string.IsNullOrEmpty(content)) {
                ClientSocket.instance.SendMessage(content);
            }
        }

        void SocketDisconnect() {
            ClientSocket.instance.Disconnect();
            m_receiveMessage = "已断开连接";
        }

        void ShowReceiveMessage(string message) {
            m_receiveMessage = message;
        }

        void Update() {
            m_receiveText.text = m_receiveMessage;
        }
    }
}