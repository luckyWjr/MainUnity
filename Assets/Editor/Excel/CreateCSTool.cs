using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace EditorTool {

    public class CreateCSTool : MonoBehaviour {

        public static string CreateCSByExcel(string className, string[] valueType, string[] valueName) {

            StringBuilder code = new StringBuilder();

            //添加常见且必须的引用字符串
            code.Append("using UnityEngine; \n");
            code.Append("using System.Collections; \n\n");
            //namespace start
            code.Append("namespace Data{\n\t");

            //存放每行表数据的类
            code.Append("[System.Serializable]\n\t");
            code.Append("public partial class " + className + " { \n");
            for(int i = 0; i < valueType.Length; i++) {
                code.Append("\t\tpublic " + valueType[i] + " " + valueName[i] + ";\n");
            }
            code.Append("\t}\n");

            //namespace end
            code.Append("}");

            if(!Directory.Exists(ExcelConfig.dataCSItemPath)) {
                Directory.CreateDirectory(ExcelConfig.dataCSItemPath);
            }

            FileStream itemfs = new FileStream(ExcelConfig.dataCSItemPath + "/" + className + ".cs", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter itemsw = new StreamWriter(itemfs, Encoding.UTF8);
            itemsw.Write(code.ToString());
            itemsw.Close();
            itemfs.Close();
            Debug.Log("成功生成c#的Class文件" + className + ".cs" + "在目录:" + ExcelConfig.dataCSItemPath + " 中");


            code.Length = 0;
            //添加常见且必须的引用字符串
            code.Append("using UnityEngine; \n");
            code.Append("using System.Collections; \n\n");
            //namespace start
            code.Append("namespace Data{\n\t");

            //管理表数据的类
            code.Append("public class " + className + "Manager : ScriptableObject { \n");
            code.Append("\t\tpublic " + className + "[] dataArray;\n");
            code.Append("\t}\n");


            //namespace end
            code.Append("}");

            if(!Directory.Exists(ExcelConfig.dataCSManagerPath)) {
                Directory.CreateDirectory(ExcelConfig.dataCSManagerPath);
            }

            FileStream managerfs = new FileStream(ExcelConfig.dataCSManagerPath + "/" + className + "Manager.cs", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter managersw = new StreamWriter(managerfs, Encoding.UTF8);
            managersw.Write(code.ToString());
            managersw.Close();
            managerfs.Close();
            Debug.Log("成功生成c#的Class文件" + className + ".cs" + "在目录:" + ExcelConfig.dataCSManagerPath + " 中");

            return null;
        }
    }
}