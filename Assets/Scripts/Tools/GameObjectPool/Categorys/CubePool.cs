using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool {

	public class CubePool : BaseGameObjectPool {

        PrefabAssetBundleItem m_cubeAsset;

        public CubePool() : base() {
        }

        public override void Init(string poolName, Transform trans) {
            base.Init(poolName, trans);
            m_cubeAsset = new PrefabAssetBundleItem("", "Cube");
            m_cubeAsset.Load();
            m_prefab = m_cubeAsset.prefab;
        }

        public override GameObject Get(Vector3 position, float lifetime) {
            lifetime = 3;
            return base.Get(position, lifetime);
        }

        public override void Destroy() {
            base.Destroy();
            m_cubeAsset.Destroy();
        }
    }
}