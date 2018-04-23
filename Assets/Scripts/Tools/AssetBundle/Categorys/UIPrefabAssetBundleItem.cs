using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool {
    /// <summary>
    /// 存储单个图片资源信息
    /// </summary>
	public class UIPrefabAssetBundleItem : BaseAssetBundleItem {

        const string imageAssetFolder = @"UIPrefabs/";
        //public const string iconFolder = @"Icon/";

        public UIPrefabAssetBundleItem(string folder, string name) {
            assetCategoryPath = imageAssetFolder;
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