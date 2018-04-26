using System.Collections;
using UnityEngine;
using System;
using System.Net;
using System.IO;

namespace Tool {

    public abstract class DownloadItem {

        /// <summary>
        /// 网络资源url路径
        /// </summary>
        protected string m_srcUrl;
        /// <summary>
        /// 资源下载存放路径，不包含文件名
        /// </summary>
        protected string m_savePath;
        /// <summary>
        /// 文件名,不包含后缀
        /// </summary>
        protected string m_fileNameWithoutExt;
        /// <summary>
        /// 文件后缀
        /// </summary>
        protected string m_fileExt;
        /// <summary>
        /// 下载文件全路径，路径+文件名+后缀
        /// </summary>
        protected string m_saveFilePath;
        /// <summary>
        /// 原文件大小
        /// </summary>
        protected long m_fileLength;
        /// <summary>
        /// 当前下载好了的大小
        /// </summary>
        protected long m_currentLength;
        /// <summary>
        /// 是否开始下载
        /// </summary>
        protected bool m_isStartDownload;
        public bool isStartDownload {
            get {
                return m_isStartDownload;
            }
        }

        public DownloadItem(string url, string path) {
            m_srcUrl = url;
            m_savePath = path;
            m_isStartDownload = false;
            m_fileNameWithoutExt = Path.GetFileNameWithoutExtension(m_srcUrl);
            m_fileExt = Path.GetExtension(m_srcUrl);
            m_saveFilePath = string.Format("{0}/{1}{2}", m_savePath, m_fileNameWithoutExt, m_fileExt);
        }

        /// <summary>
        /// 开始下载
        /// </summary>
        /// <param name="callback">下载完成回调</param>
        public virtual void StartDownload(Action callback = null) {
            if(string.IsNullOrEmpty(m_srcUrl) || string.IsNullOrEmpty(m_savePath)) {
                return;
            }
            //若存放目录不存在则创建目录
            FileTool.CreateDirectory(m_saveFilePath);
        }

        /// <summary>
        /// 获取下载进度
        /// </summary>
        /// <returns>进度，0-1</returns>
        public abstract float GetProcess();

        /// <summary>
        /// 获取当前下载了的文件大小
        /// </summary>
        /// <returns>当前文件大小</returns>
        public abstract long GetCurrentLength();

        /// <summary>
        /// 获取要下载的文件大小
        /// </summary>
        /// <returns>文件大小</returns>
        public abstract long GetLength();

        public abstract void Destroy();
    }

    /// <summary>
    /// WWW的方式下载
    /// </summary>
    public class WWWDownloadItem : DownloadItem {

        WWW m_www;

        public WWWDownloadItem(string url, string path) : base(url, path) {

        }

        public override void StartDownload(Action callback = null) {
            base.StartDownload();
            UICoroutine.instance.StartCoroutine(Download(callback));
        }

        IEnumerator Download(Action callback = null) {
            m_www = new WWW(m_srcUrl);
            m_isStartDownload = true;
            yield return m_www;
            //WWW读取完成后，才开始往下执行
            m_isStartDownload = false;

            if(m_www.isDone) {
                byte[] bytes = m_www.bytes;
                //创建文件
                FileTool.CreatFile(m_saveFilePath, bytes);
            } else {
                Debug.Log("Download Error:" + m_www.error);
            }

            if(callback != null) {
                callback();
            }
        }

        public override float GetProcess() {
            if(m_www != null) {
                return m_www.progress;
            }
            return 0;
        }

        public override long GetCurrentLength() {
            if(m_www != null) {
                return m_www.bytesDownloaded;
            }
            return 0;
        }

        public override long GetLength() {
            return 0;
        }

        public override void Destroy() {
            if(m_www != null) {
                m_www.Dispose();
                m_www = null;
            }
        }
    }

    /// <summary>
    /// HTTP的方式下载，支持断点续传
    /// </summary>
    public class HttpDownloadItem : DownloadItem {
        /// <summary>
        /// 临时文件后缀名
        /// </summary>
        string m_tempFileExt = ".temp";
        /// <summary>
        /// 临时文件全路径
        /// </summary>
        string m_tempSaveFilePath;

        public HttpDownloadItem(string url, string path) : base(url, path) {
            m_tempSaveFilePath = string.Format("{0}/{1}{2}", m_savePath, m_fileNameWithoutExt, m_tempFileExt);
        }

        public override void StartDownload(Action callback = null) {
            base.StartDownload();
            UICoroutine.instance.StartCoroutine(Download(callback));
        }

        IEnumerator Download(Action callback = null) {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(m_srcUrl);
            request.Method = "GET";

            FileStream fileStream;
            if(File.Exists(m_tempSaveFilePath)) {
                //若之前已下载了一部分，继续下载
                fileStream = File.OpenWrite(m_tempSaveFilePath);
                m_currentLength = fileStream.Length;
                fileStream.Seek(m_currentLength, SeekOrigin.Current);

                //设置下载的文件读取的起始位置
                request.AddRange((int)m_currentLength);
            } else {
                //第一次下载
                fileStream = new FileStream(m_tempSaveFilePath, FileMode.Create, FileAccess.Write);
                m_currentLength = 0;
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            //总的文件大小=当前需要下载的+已下载的
            m_fileLength = response.ContentLength + m_currentLength;

            m_isStartDownload = true;
            int lengthOnce;
            int bufferMaxLength = 1024 * 20;

            while(m_currentLength < m_fileLength) {

                byte[] buffer = new byte[bufferMaxLength];
                if(stream.CanRead) {
                    //读写操作
                    lengthOnce = stream.Read(buffer, 0, buffer.Length);
                    m_currentLength += lengthOnce;
                    fileStream.Write(buffer, 0, lengthOnce);
                } else {
                    break;
                }
                yield return null;
            }

            m_isStartDownload = false;
            response.Close();
            stream.Close();
            fileStream.Close();

            //临时文件转为最终的下载文件
            File.Move(m_tempSaveFilePath, m_saveFilePath);

            if(callback != null) {
                callback();
            }
        }

        public override float GetProcess() {
            if(m_fileLength > 0) {
                return Mathf.Clamp((float)m_currentLength / m_fileLength, 0, 1);
            }
            return 0;
        }

        public override long GetCurrentLength() {
            return m_currentLength;
        }

        public override long GetLength() {
            return m_fileLength;
        }

        public override void Destroy() {
        }
    }
}