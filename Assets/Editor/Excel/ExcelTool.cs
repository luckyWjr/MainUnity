using Excel;
using System.Data;
using System.IO;
using System;
using System.Reflection;
using System.Collections;

namespace EditorTool {
    public class ExcelTool {

        public static void ExcelToCS(string filePath) {
            int columnNum = 0, rowNum = 0;
            DataRowCollection collect = ReadExcel(filePath, ref columnNum, ref rowNum);

            string[] valueTypeList = new string[columnNum];
            string[] valueNameList = new string[columnNum];

            for(int i = 0, j = 0; i < rowNum; i++) {
                //根据excel的定义，第二行为属性名称，第三行为属性类型
                if(i == 1) {
                    for(j = 0; j < columnNum; j++) {
                        valueNameList[j] = collect[i][j].ToString();
                    }
                }

                if(i == 2) {
                    for(j = 0; j < columnNum; j++) {
                        valueTypeList[j] = collect[i][j].ToString();
                    }
                }
            }
            CreateCSTool.CreateCSByExcel(Path.GetFileNameWithoutExtension(filePath), valueTypeList, valueNameList);
        }

        public static void ExcelExportToAsset(string filePath, Type classManagerType, Type classType, object asset) {
            int columnNum = 0, rowNum = 0;
            DataRowCollection collect = ReadExcel(filePath, ref columnNum, ref rowNum);

            string[] valueNameList = new string[columnNum];

            ConstructorInfo[] managerCInfo = classManagerType.GetConstructors();
            object manager = managerCInfo[0].Invoke(null);
            var field = classManagerType.GetField("dataArray");

            //根据参数类型获取所有的构造函数
            ConstructorInfo itemCInfo = classType.GetConstructor(Type.EmptyTypes);

            ArrayList arrayList = new ArrayList();

            for(int i = 0, j = 0; i < rowNum; i++) {
                if(i == 1) {
                    for(j = 0; j < columnNum; j++) {
                        valueNameList[j] = collect[i][j].ToString();
                    }
                }

                if(i > 2) {
                    //导出数据
                    //调用构造函数生成对象 
                    object item = itemCInfo.Invoke(null);

                    for(j = 0; j < columnNum; j++) {

                        var itemField = classType.GetField(valueNameList[j]);
                        object valueObj = GetCSharpValue(collect[i][j].ToString(), itemField.FieldType);
                        itemField.SetValue(item, valueObj);
                    }
                    arrayList.Add(item);
                }
            }

            field.SetValue(asset, arrayList.ToArray(classType));
        }

        /// <summary>
        /// 读取excel文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="columnNum">行数</param>
        /// <param name="rowNum">列数</param>
        /// <returns></returns>
        static DataRowCollection ReadExcel(string filePath, ref int columnNum, ref int rowNum) {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            DataSet result = excelReader.AsDataSet();
            columnNum = result.Tables[0].Columns.Count;
            rowNum = result.Tables[0].Rows.Count;
            return result.Tables[0].Rows;
        }

        static object GetCSharpValue(string srcValue, Type type) {
            object target;
            UnityEngine.Debug.Log(type.ToString());
            switch(type.ToString()) {
                // ulong
                case "System.UInt64":
                    srcValue = srcValue.Replace(" ", "");
                    if(string.IsNullOrEmpty(srcValue)) {
                        target = default(ulong);
                    }
                    else {
                        ulong ul;
                        if(!ulong.TryParse(srcValue, out ul)) {
                            throw new InvalidOperationException(srcValue + " is not a ulong.");
                        }
                        target = ul;
                    }
                    break;

                // int
                case "System.Int32":
                    srcValue = srcValue.Replace(" ", "");
                    if(string.IsNullOrEmpty(srcValue)) {
                        target = default(int);
                    }
                    else {
                        int i;
                        if(!int.TryParse(srcValue, out i)) {
                            throw new InvalidOperationException(srcValue + " is not a int.");
                        }
                        target = i;
                    }
                    break;

                // uint
                case "System.UInt32":
                    srcValue = srcValue.Replace(" ", "");
                    if(string.IsNullOrEmpty(srcValue)) {
                        target = default(uint);
                    }
                    else {
                        uint i;
                        if(!uint.TryParse(srcValue, out i)) {
                            throw new InvalidOperationException(srcValue + " is not a uint.");
                        }
                        target = i;
                    }
                    break;

                // ushort
                case "System.UInt16":
                    srcValue = srcValue.Replace(" ", "");
                    if(string.IsNullOrEmpty(srcValue)) {
                        target = default(ushort);
                    }
                    else {
                        ushort b;
                        if(!ushort.TryParse(srcValue, out b)) {
                            throw new InvalidOperationException(string.Format("{0} is not a ushort", srcValue));
                        }
                        target = b;
                    }
                    break;

                // byte
                case "System.Byte":
                    srcValue = srcValue.Replace(" ", "");
                    if(string.IsNullOrEmpty(srcValue)) {
                        target = default(byte);
                    }
                    else {
                        byte b;
                        if(!byte.TryParse(srcValue, out b)) {
                            throw new InvalidOperationException(srcValue + " is not a byte.");
                        }
                        target = b;
                    }
                    break;

                // float
                case "System.Single":
                    srcValue = srcValue.Replace(" ", "");
                    if(string.IsNullOrEmpty(srcValue)) {
                        target = default(float);
                    }
                    else {
                        float f;
                        if(!float.TryParse(srcValue, out f)) {
                            throw new InvalidOperationException(srcValue + " is not a float.");
                        }
                        target = f;
                    }
                    break;

                // string
                case "System.String":
                    target = srcValue.TrimStart('"').TrimEnd('"');
                    break;

                // bool
                case "System.Boolean":
                    if(string.IsNullOrEmpty(srcValue)) {
                        target = default(bool);
                    }
                    else {
                        bool b;
                        if(!bool.TryParse(srcValue.ToLower(), out b)) {
                            throw new InvalidOperationException(srcValue + " is not a boolean.");
                        }
                        target = b;
                    }
                    break;

                default:
                    target = srcValue;
                    throw new InvalidOperationException("Unexpected c# type: " + type.ToString());
            }

            return target;
        }
    }
}