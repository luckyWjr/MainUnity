using System.IO;

namespace Tool {

    public class FileTool {

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="filePath">需要创建的目录路径</param>
        public static void CreateDirectory(string filePath) {
            if(!string.IsNullOrEmpty(filePath)) {
                string dirName = Path.GetDirectoryName(filePath);
                if(!Directory.Exists(dirName)) {
                    Directory.CreateDirectory(dirName);
                }
            }
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="fromFilePath">需要拷贝的文件全路径</param>
        /// <param name="toFilePath">目标文件全路径</param>
        public static void CopyFile(string fromFilePath, string toFilePath) {
            CreateDirectory(toFilePath);
            File.Copy(fromFilePath, toFilePath, true);
        }


        public static void CopyFiles(string fromFolderPath, string toFolderPath, string except = "") {
            string[] files = Directory.GetFiles(fromFolderPath, "*.*", SearchOption.AllDirectories);
            for(int i = 0; i < files.Length; i++) {
                if(!string.IsNullOrEmpty(except) && files[i].Contains(except)) {
                    continue;
                }
                string relativePath = files[i].Substring(fromFolderPath.Length);
                string targetFile = string.Format("{0}{1}", toFolderPath, relativePath);
                CopyFile(files[i], targetFile);
            }
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="bytes">文件内容</param>
        public static void CreatFile(string filePath, byte[] bytes) {
            FileInfo file = new FileInfo(filePath);
            Stream stream = file.Create();

            stream.Write(bytes, 0, bytes.Length);

            stream.Close();
            stream.Dispose();
        }
    }
}