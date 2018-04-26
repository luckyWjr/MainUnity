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

        Fu GetFu() {
            Zi z = new Zi();
            z.a = 100;
            z.x = 10;
            return z as Fu;
        }
	}

    public class Fu {
        public int a;
    }

    public class Zi : Fu {
        public int x;
    }
}