using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tool;

namespace Examples {

	public class GameObjectPoolDemo : MonoBehaviour {

        [SerializeField] Button m_addSphereBtn;
        [SerializeField] Button m_addCubeBtn;
        [SerializeField] GameObject m_spherePrefab;

        BaseGameObjectPool m_spherePool;
        CubePool m_cubePool;

        void Start () {
            m_spherePool = GameObjectPoolManager.instance.CreatGameObjectPool<BaseGameObjectPool>("SpherePool");
            m_spherePool.prefab = m_spherePrefab;

            m_cubePool = GameObjectPoolManager.instance.CreatGameObjectPool<CubePool>("CubePool");

            m_addSphereBtn.onClick.AddListener(() => {
                float x = Random.Range(-15, 15);
                float y = Random.Range(-10, 10);
                m_spherePool.Get(new Vector3(x, y, 0), 1);
                //GameObjectPoolManager.instance.GetGameObject("SpherePool", new Vector3(x, y, 0), 1);
            });

            m_addCubeBtn.onClick.AddListener(() => {
                float x = Random.Range(-15, 15);
                float y = Random.Range(-10, 10);
                GameObjectPoolManager.instance.GetGameObject("CubePool", new Vector3(x, y, 0), 1);
            });
        }
		
		void Update () {
			
		}
	}
}