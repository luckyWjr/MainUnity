using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool {

	public class UnityCallAndroid {

        const string javaClassName = "javaClassName";

        public void Call(string methodName, params object[] args) {
            AndroidJavaClass javaClass = new AndroidJavaClass(javaClassName);
            javaClass.CallStatic(methodName, args);
        }

        public string CallHaveReturn(string methodName, params object[] args) {
            AndroidJavaClass javaClass = new AndroidJavaClass(javaClassName);
            return javaClass.CallStatic<string>(methodName, args);
        }
    }
}