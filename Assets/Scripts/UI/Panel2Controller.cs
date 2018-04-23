using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tool;

namespace UI {

	public class Panel2Controller : MonoBehaviour {

        [SerializeField] Button m_unloadFalseBtn;
        [SerializeField] Button m_unloadTrueBtn;
        [SerializeField] RawImage m_rawImage;

        UIPrefabAssetBundleItem m_panel3Asset;
        ImageAssetBundleItem m_imageAsset;

        void Start () {
            m_unloadFalseBtn.onClick.AddListener(UnloadAssetBundleFalse);
            m_unloadTrueBtn.onClick.AddListener(UnloadAssetBundleTrue);

            m_panel3Asset = new UIPrefabAssetBundleItem("","panel3");
            m_panel3Asset.Load();
            GameObject panel3 = Instantiate(m_panel3Asset.prefab);
            panel3.transform.SetParent(transform.parent);
            panel3.transform.localPosition = new Vector3(-300, 0, 0);
            panel3.transform.localScale = Vector3.one;

            m_imageAsset = new ImageAssetBundleItem(ImageAssetBundleItem.backgroundFolder, "BG1");
            m_imageAsset.Load();
            m_rawImage.texture = m_imageAsset.texture;

        }

        void Update () {
			
		}

        void UnloadAssetBundleFalse() {
            m_panel3Asset.Destroy();
            m_imageAsset.Destroy();
            System.GC.Collect();
        }

        void UnloadAssetBundleTrue() {
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }
    }
}