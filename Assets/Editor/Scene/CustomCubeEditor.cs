using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Tool;

namespace EditorTool {

    //声明要处理的组件类型
    [CustomEditor(typeof(CustomCube))]
    public class CustomCubeEditor : Editor {

        CustomCube m_cube;
        Transform m_trans;

        void OnEnable() {
            //包含该组件的物体被选中的时候调用
            m_cube = (CustomCube)target;
            m_trans = m_cube.transform;
        }

        public override void OnInspectorGUI() {
        }

        void OnSceneGUI() {
            //坐标位置取整
            Vector3 posTemp = m_trans.localPosition;
            float x = Mathf.RoundToInt(posTemp.x);
            float y = Mathf.RoundToInt(posTemp.y);
            float z = Mathf.RoundToInt(posTemp.z);
            m_trans.localPosition = new Vector3(x, y, z);

            //显示坐标
            Handles.Label(m_trans.position + Vector3.up * 3, m_cube.name + " : " + m_trans.position.ToString());

            Handles.BeginGUI();

            //规定GUI显示区域
            GUILayout.BeginArea(new Rect(100, 100, 100, 100));

            //GUI绘制按钮
            if(GUILayout.Button("上移")) {
                m_trans.position += Vector3.up;
            }
            if(GUILayout.Button("下移")) {
                m_trans.position += Vector3.down;
            }
            if(GUILayout.Button("左移")) {
                m_trans.position += Vector3.left;
            }
            if(GUILayout.Button("右移")) {
                m_trans.position += Vector3.right;
            }
            GUILayout.EndArea();

            Handles.EndGUI();
        }
    }
}