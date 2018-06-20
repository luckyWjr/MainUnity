using UnityEngine;
using Tool;
using System.Collections.Generic;
using SimpleJSON;
using System;

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

        public List<int> GetList() {
            List<int> l = new List<int>();
            l.Add(1);
            l.Add(3);
            l.Add(7);
            l.Add(6);
            l.Add(0);
            return l;
        }

        public List<string> GetSList() {
            List<string> l = new List<string>();
            l.Add("aa");
            l.Add("sdf");
            l.Add("wer");
            l.Add("rr");
            l.Add("w3");
            return l;
        }

        public Dictionary<int, string> GetDic() {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(100, "ad");
            dic.Add(103, "ewq");
            dic.Add(140, "2s");
            dic.Add(600, "fs");
            return dic;
        }

        public void Js(Action<SimpleJSON.JSONObject> a) {
            Debug.Log("111111");
            SimpleJSON.JSONObject jSONObject = new SimpleJSON.JSONObject();
            a(jSONObject);
        }
    }

    public class Zi : Fu {
        public int x;
    }
}