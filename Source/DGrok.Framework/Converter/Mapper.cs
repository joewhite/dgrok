using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DGrok.Converter
{
    public class Mapper
    {
        public static ArrayList allDelphiTypes = new ArrayList();

        public static Dictionary<string, string> typeMap = new Dictionary<string, string>()
        {
            {"LongWord", "uint"},
            {"Integer", "int"},
            {"integer", "int"},
            {"Currency", "decimal"},
            {"Boolean", "bool"},
            {"boolean", "bool"},
            {"WordBool", "bool"},
            {"WideString", "String"},
            {"String", "String"},
            {"string", "String"},
            {"TObject", "Object"},
            {"OleVariant", "DataTable"},
            {"TFDQuery", "ClientDataSet"},
            {"Exception", "Exception"},
            {"exception", "Exception"},
            {"TStringList", "List<String>"},
            {"List<String>", "List<String>"}
        };

        public static String getMappedType(String delphiType)
        {
            if (!typeMap.ContainsKey(delphiType))
            {
                throw new Exception("Unsupported delphiType: " + delphiType);
            }

            return typeMap[delphiType];
        }

        public static String getTypeOfValue(String value)
        {
            if (value.Contains("'"))
            {
                return "String";
            }
            else
            {
                return "int";
            }
        }

        public static String formatValue(String value, String type = "String")
        {
            if (type != null && type.Equals("String"))
            {
                int len = value.Length;
                if (value.StartsWith("\"") && value.EndsWith("\""))
                {
                    return value;
                }
                else
                {
                    return '"' + value.Substring(1, len - 2) + '"';
                }
            }
            else
            {
                return value;
            }
        }
    }
}
