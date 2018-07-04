using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

namespace Examples {

    public class DownloadDemo : MonoBehaviour {

        DownloadItem m_item;
        string testScrUrl = "http://dlsw.baidu.com/sw-search-sp/soft/ca/13442/Thunder_dl_7.9.42.5050.1449557123.exe";
        string apkUrl = "http://cdn.uniugame.com/asset/dance520.apk";
        int count = 0;

        void Start() {
            Debug.Log(Application.persistentDataPath);

            //m_item = new WWWDownloadItem(testScrUrl, Application.persistentDataPath);
            //m_item.StartDownload(DownloadFinish);

            //m_item = new HttpDownloadItem(testScrUrl, Application.persistentDataPath);
            //m_item.StartDownload(DownloadFinish);

            m_item = new WebRequestDownloadItem(testScrUrl, Application.persistentDataPath);
            m_item.StartDownload(DownloadFinish);
        }

        void Update() {
            count++;

            if(count % 20 == 0) {
                if(m_item != null && m_item.isStartDownload) {
                    Debug.Log("下载进度------" + m_item.GetProcess() + "------已下载大小---" + m_item.GetCurrentLength());
                }
            }
        }

        void DownloadFinish() {
            Debug.Log("DownloadFinish！！！");
        }
    }
}