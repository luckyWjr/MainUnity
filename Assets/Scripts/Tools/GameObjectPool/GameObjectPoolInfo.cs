using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool {
    public class GameObjectPoolInfo : MonoBehaviour {
        /// <summary>
        /// 对象显示的持续时间，若=0，则不隐藏
        /// </summary>
        [HideInInspector] public float lifetime = 0;
        /// <summary>
        /// 所属对象池的唯一id
        /// </summary>
        [HideInInspector] public string poolName;

        WaitForSeconds m_waitTime;

        void Awake() {
            if(lifetime > 0) {
                m_waitTime = new WaitForSeconds(lifetime);
            }
        }

        void OnEnable() {
            if(lifetime > 0) {
                StartCoroutine(CountDown(lifetime));
            }
        }

        IEnumerator CountDown(float lifetime) {
            yield return m_waitTime;
            //将对象加入对象池
            GameObjectPoolManager.instance.RemoveGameObject(poolName, gameObject);
        }
    }
}