﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool {
    /// <summary>
    /// 存储单个图片资源信息
    /// </summary>
	public class SpriteAssetBundleItem : GroupAssetBundleItem {
        const string m_spriteAssetFolder = @"Sprites/";

        public const string iconFolder = @"Icon";

        public SpriteAssetBundleItem(string folder) {
            assetCategoryPath = m_spriteAssetFolder;
            this.folder = folder;
            m_fullPath = string.Format("{0}{1}.u", assetCategoryPath, folder);
        }

        public Sprite GetSprite(string name) {
            return assetBundleItem.LoadAsset(name, typeof(Sprite)) as Sprite;
        }
    }
}