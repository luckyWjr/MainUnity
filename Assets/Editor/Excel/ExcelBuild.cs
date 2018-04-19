using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace EditorTool {
    public class ExcelBuild : Editor {
        /// <summary>
        /// 第一步，生成对应的cs文件
        /// </summary>
        [MenuItem("CustomEditor/Excel/Create CS")]
        public static void CreateCSByExcel() {
            List<string> list = GetExcelFilesPath();
            if(list != null) {
                foreach(string path in list) {
                    ExcelTool.ExcelToCS(path);
                }
            }
            AssetDatabase.Refresh();
        }

        [MenuItem("CustomEditor/Excel/Clear All CS")]
        public static void DeleteCSFiles() {
            if(Directory.Exists(ExcelConfig.dataCSItemPath)) {
                Directory.Delete(ExcelConfig.dataCSItemPath, true);
            }
            if(Directory.Exists(ExcelConfig.dataCSManagerPath)) {
                Directory.Delete(ExcelConfig.dataCSManagerPath, true);
            }
            AssetDatabase.Refresh();
        }

        

        /// <summary>
        /// 第二步，导出数据
        /// </summary>
        [MenuItem("CustomEditor/Excel/Export Data")]
        public static void ExcuteBuild() {
            if(!Directory.Exists(ExcelConfig.assetPath)) {
                Directory.CreateDirectory(ExcelConfig.assetPath);
            }
            List<string> list = GetExcelFilesPath();
            if(list != null) {
                foreach(string path in list) {
                    //获取对应的类
                    string className = Path.GetFileNameWithoutExtension(path);
                    string classManagerName = className + "Manager";
                    Assembly assembly = Assembly.Load("Assembly-CSharp");
                    Type classType = assembly.GetType("Data." + className);
                    Type classManagerType = assembly.GetType("Data." + classManagerName);
                    if(classType == null || classManagerType == null) {
                        throw new InvalidOperationException(string.Format("c# class {0},{1} is not exists.", className, classManagerType));
                    }

                    string assetPath = string.Format("{0}{1}.asset", ExcelConfig.assetPath, className);
                    var asset = AssetDatabase.LoadAssetAtPath(assetPath, classManagerType);
                    if(asset == null) {
                        Debug.Log(assetPath);
                        var obj = ScriptableObject.CreateInstance(classManagerType);
                        AssetDatabase.CreateAsset(obj, assetPath);
                        asset = AssetDatabase.LoadAssetAtPath(assetPath, classManagerType);
                    }

                    ExcelTool.ExcelExportToAsset(path, classManagerType, classType, asset);
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("CustomEditor/Excel/Clear All Asset")]
        public static void DeleteAssetFiles() {
            if(Directory.Exists(ExcelConfig.assetPath)) {
                Directory.Delete(ExcelConfig.assetPath, true);
            }
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 获取excel文件路径
        /// </summary>
        /// <returns>文件路径</returns>
        static List<string> GetExcelFilesPath() {
            List<string> list = new List<string>();
            string[] excelFileArray = Directory.GetFiles(ExcelConfig.excelsFolderPath);
            if(excelFileArray == null || excelFileArray.Length == 0) {
                Debug.Log("没有excel文件需要转化");
                return null;
            }
            for(int i = 0; i < excelFileArray.Length; i++) {
                if(Path.GetExtension(excelFileArray[i]).Equals(".xlsx")) {
                    list.Add(excelFileArray[i]);
                }
            }
            return list;
        }
    }
}

