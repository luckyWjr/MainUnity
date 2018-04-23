using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool {
    /// <summary>
    /// 存储单个图片资源信息
    /// </summary>
	public class ImageAssetBundleItem : BaseAssetBundleItem {

        const string imageAssetFolder = @"Images/";
        public const string iconFolder = @"Icon/";
        public const string backgroundFolder = @"Background/";

        public ImageAssetBundleItem(string folder, string name) {
            assetCategoryPath = imageAssetFolder;
            this.folder = folder;
            this.name = name;
            m_fullPath = string.Format("{0}{1}{2}.u", assetCategoryPath, folder, name);
        }

        public Texture2D texture {
            get {
                return (Texture2D)m_obj;
            }
        }

        //public override void Load(Action callback = null) {
        //    base.Load(callback);

        //}
    }
}