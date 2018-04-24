using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool {
    public class CustomCube : MonoBehaviour {

        void OnDrawGizmos() {
            //每帧调用

            //绘制一个cube边框
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(GetComponent<Renderer>().bounds.center, GetComponent<Renderer>().bounds.size);

            //绘制一条直线，指向物体正上方
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + transform.up * 2);
        }

        void OnDrawGizmosSelected() {
            //被选中的时候每帧调用
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(GetComponent<Renderer>().bounds.center, GetComponent<Renderer>().bounds.size);
        }
    }
}