using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool {

	public class PrefabAssetBundleItem : BaseAssetBundleItem {

        const string prefabAssetFolder = @"Prefabs/";
        //public const string iconFolder = @"Icon/";

        public PrefabAssetBundleItem(string folder, string name) {
            assetCategoryPath = prefabAssetFolder;
            this.folder = folder;
            this.name = name;
            m_fullPath = string.Format("{0}{1}{2}.u", assetCategoryPath, folder, name);
        }

        public GameObject prefab {
            get {
                return (GameObject)m_obj;
            }
        }
    }
}