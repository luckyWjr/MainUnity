using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool {
    public class BaseGameObjectPool {

        /// <summary>
        /// 队列，存放对象池中没有用到的对象，即可分配对象
        /// </summary>
        protected Queue m_queue;
        /// <summary>
        /// 对象池中存放最大数量
        /// </summary>
        protected int m_maxCount;
        /// <summary>
        /// 对象预设
        /// </summary>
        protected GameObject m_prefab;
        /// <summary>
        /// 该对象池的transform
        /// </summary>
        protected Transform m_trans;
        /// <summary>
        /// 每个对象池的名称，当唯一id
        /// </summary>
        protected string m_poolName;
        /// <summary>
        /// 默认最大容量
        /// </summary>
        protected const int m_defaultMaxCount = 10;

        public BaseGameObjectPool() {
            m_maxCount = m_defaultMaxCount;
            m_queue = new Queue();
        }

        public virtual void Init(string poolName, Transform trans) {
            m_poolName = poolName;
            m_trans = trans;
        }

        public GameObject prefab {
            set {
                m_prefab = value;
            }
        }

        public int maxCount {
            set {
                m_maxCount = value;
            }
        }

        /// <summary>
        /// 生成一个对象
        /// </summary>
        /// <param name="position">起始坐标</param>
        /// <param name="lifetime">对象存在的时间</param>
        /// <returns>生成的对象</returns>
        public virtual GameObject Get(Vector3 position, float lifetime) {
            if(lifetime < 0) {
                //lifetime<0时，返回null  
                return null;
            }
            GameObject returnObj;
            if(m_queue.Count > 0) {
                //池中有待分配对象
                returnObj = (GameObject)m_queue.Dequeue();
            } else {
                //池中没有可分配对象了，新生成一个
                returnObj = GameObject.Instantiate(m_prefab) as GameObject;
                returnObj.transform.SetParent(m_trans);
                returnObj.SetActive(false);
            }
            //使用PrefabInfo脚本保存returnObj的一些信息
            GameObjectPoolInfo info = returnObj.GetComponent<GameObjectPoolInfo>();
            if(info == null) {
                info = returnObj.AddComponent<GameObjectPoolInfo>();
            }
            info.poolName = m_poolName;
            if(lifetime > 0) {
                info.lifetime = lifetime;
            }
            returnObj.transform.position = position;
            returnObj.SetActive(true);
            return returnObj;
        }

        /// <summary>
        /// “删除对象”放入对象池
        /// </summary>
        /// <param name="obj">对象</param>
        public virtual void Remove(GameObject obj) {
            //待分配对象已经在对象池中  
            if(m_queue.Contains(obj)) {
                return;
            }
            if(m_queue.Count > m_maxCount) {
                //当前池中object数量已满，直接销毁
                GameObject.Destroy(obj);
            } else {
                //放入对象池，入队
                m_queue.Enqueue(obj);
                obj.SetActive(false);
            }
        }

        /// <summary>
        /// 销毁该对象池
        /// </summary>
        public virtual void Destroy() {
            m_queue.Clear();
        }
    }
}