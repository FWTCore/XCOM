using CodeGeneration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Utility
{
    public class DataBaseHelper
    {
        /// <summary>
        /// 数据库字段映射
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isNull"></param>
        /// <param name="numericPrecision"></param>
        /// <returns></returns>
        public static string GetFieldType(string type, bool isNull, int? numericPrecision)
        {
            string newType = "string";
            switch (type.ToUpper())
            {
                case nameof(DBFieldTypeEnum.VARCHAR):
                case nameof(DBFieldTypeEnum.VARCHAR2):
                case nameof(DBFieldTypeEnum.NVARCHAR):
                case nameof(DBFieldTypeEnum.CHAR):
                    newType = "string";
                    break;
                case nameof(DBFieldTypeEnum.TINYINT):
                    if (numericPrecision.HasValue && numericPrecision == 1)
                        newType = "bool";
                    else
                        newType = "byte";
                    if (isNull) newType = newType + "?";
                    break;
                case nameof(DBFieldTypeEnum.INT):
                case nameof(DBFieldTypeEnum.INTEGER):
                case nameof(DBFieldTypeEnum.SMALLINT):
                    newType = "int";
                    if (isNull) newType = newType + "?";
                    break;
                case nameof(DBFieldTypeEnum.LONG):
                case nameof(DBFieldTypeEnum.BIGINT):
                    newType = "long";
                    if (isNull) newType = newType + "?";
                    break;
                case nameof(DBFieldTypeEnum.DATE):
                case nameof(DBFieldTypeEnum.DATETIME):
                case nameof(DBFieldTypeEnum.DATETIME2):
                case nameof(DBFieldTypeEnum.DATETIMEOFFSET):
                    newType = "DateTime";
                    if (isNull) newType = newType + "?";
                    break;
                case nameof(DBFieldTypeEnum.DECIMAL):
                case nameof(DBFieldTypeEnum.NUMBER):
                case nameof(DBFieldTypeEnum.MONEY):
                case nameof(DBFieldTypeEnum.NUMERIC):
                    newType = "Decimal";
                    if (isNull) newType = newType + "?";
                    break;
                case nameof(DBFieldTypeEnum.DOUBLE):
                    newType = "double";
                    if (isNull) newType = newType + "?";
                    break;
                case nameof(DBFieldTypeEnum.FLOAT):
                    newType = "float";
                    if (isNull) newType = newType + "?";
                    break;
                case nameof(DBFieldTypeEnum.UNIQUEIDENTIFIER):
                    newType = "Guid";
                    if (isNull) newType = newType + "?";
                    break;
                case nameof(DBFieldTypeEnum.BIT):
                    newType = "bool";
                    if (isNull) newType = newType + "?";
                    break;

            }

            return newType;


        }
    }
}
