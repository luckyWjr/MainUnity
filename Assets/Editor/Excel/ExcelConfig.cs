using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorTool {

	public class ExcelConfig {
        /// <summary>
        /// 存放excel表文件夹的的路径
        /// </summary>
        public static readonly string excelsFolderPath = Application.dataPath + "/Excels/";

        /// <summary>
        /// 存放Excel转化CS文件的文件夹路径,每项数据的类
        /// </summary>
        public static readonly string dataCSItemPath = Application.dataPath + "/Scripts/Data/Item";

        /// <summary>
        /// 存放Excel转化CS文件的文件夹路径，管理每项数据的类
        /// </summary>
        public static readonly string dataCSManagerPath = Application.dataPath + "/Scripts/Data/Manager";

        /// <summary>
        /// 存放Excel转化CS文件的文件夹路径
        /// </summary>
        public static readonly string assetPath = "Assets/Resources/DataAssets/";
    }
}