using System;
using System.Collections.Generic;
using System.Text;

namespace DGrok.Converter
{
    class Mapper
    {
        public static String getMappedType(String delphiType)
        {
            if (delphiType.Equals("Integer"))
            {
                return "int";
            }

            if (delphiType.Equals("String"))
            {
                return "String";
            }

            if (delphiType.Equals("OleVariant"))
            {
                return "DataTable";
            }

            if (delphiType.Equals("WideString"))
            {
                return "String";
            }

            throw new Exception("Unsupported delphiType: " + delphiType);
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
