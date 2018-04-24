using UnityEngine;
using Tool;

namespace Examples {

	public class Hotfix : MonoBehaviour {

		void Start () {
            XLuaManager.instance.Start();
            Show();
        }

        void Show() {
            Debug.Log("Show!!!");
        }
	}
}