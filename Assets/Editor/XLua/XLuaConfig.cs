using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using XLua;

namespace EditorTool {

	public static class XLuaConfig {

        [LuaCallCSharp]
        public static List<Type> luaCallCSharpList = new List<Type>() {
                typeof(System.Object),
                typeof(UnityEngine.Object),
                typeof(Vector2),
                typeof(Vector3),
                typeof(Vector4),
                typeof(Quaternion),
                typeof(Color),
                typeof(Ray),
                typeof(Bounds),
                typeof(Ray2D),
                typeof(Time),
                typeof(GameObject),
                typeof(Component),
                typeof(Behaviour),
                typeof(Transform),
                typeof(Resources),
                typeof(TextAsset),
                typeof(Keyframe),
                typeof(AnimationCurve),
                typeof(AnimationClip),
                typeof(MonoBehaviour),
                typeof(ParticleSystem),
                typeof(SkinnedMeshRenderer),
                typeof(Renderer),
                typeof(WWW),
                typeof(List<int>),
                typeof(Action<string>),
                typeof(Debug),
                typeof(SimpleJSON.JSONObject)
            };

        [CSharpCallLua]
        public static List<Type> csharpCallLuaList = new List<Type>() {
                typeof(Action),
                typeof(Func<double, double, double>),
                typeof(Action<string>),
                typeof(Action<double>),
                typeof(Action<SimpleJSON.JSONObject>),
                typeof(UnityEngine.Events.UnityAction),
                typeof(System.Collections.IEnumerator)
            };

        [Hotfix]
        public static List<Type> hotfixList {
            get {
                string[] allowNamespaces = new string[] {
                    "Tool",
                    "Examples",
                };

                return (from type in Assembly.Load("Assembly-CSharp").GetTypes()
                        where allowNamespaces.Contains(type.Namespace)
                        select type).ToList();
            }
        }

        [BlackList]
        public static List<List<string>> blackListList = new List<List<string>>()  {
                new List<string>(){"UnityEngine.WWW", "movie"},
                new List<string>(){"UnityEngine.GameObject", "networkView"}, //4.6.2 not support
                new List<string>(){"UnityEngine.Component", "networkView"},  //4.6.2 not support
                new List<string>(){"System.IO.FileInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.FileInfo", "SetAccessControl", "System.Security.AccessControl.FileSecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.DirectoryInfo", "SetAccessControl", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "CreateSubdirectory", "System.String", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "Create", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"UnityEngine.MonoBehaviour", "runInEditMode"},
            };

        [CSObjectWrapEditor.GenPath]
        public static string genPath = "Assets/Scripts/Tools/XLua/Gen/";

        [CSObjectWrapEditor.GenCodeMenu]
        public static void XLuaGenerateCodeFinish() {
            Debug.Log("XLuaGenerateCodeFinish");
        }
    }
}