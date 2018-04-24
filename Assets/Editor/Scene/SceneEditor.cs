using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EditorTool {

	public class SceneEditor : MonoBehaviour {

        [DrawGizmo(GizmoType.Selected)]
        static void DrawGameObjectName(Transform transform, GizmoType gizmoType) {
            //Handles.Label(transform.position, transform.gameObject.name);
        }
    }
}