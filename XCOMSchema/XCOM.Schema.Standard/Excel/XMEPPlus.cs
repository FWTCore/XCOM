using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Standard.Utility;

namespace XCOM.Schema.Standard.Excel
{
    public class XMEPPlus
    {

        public static T LoadExcel<T>(string filePath, bool hasHeader = true, Dictionary<string, string> headerMapp = null)
        {
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excelPack = new ExcelPackage())
            {
                //Load excel stream
                using (var stream = File.OpenRead(filePath))
                {
                    excelPack.Load(stream);
                }
                //处理第一个工作表。(如果处理多个表格，可以在此用for循环处理)
                var ws = excelPack.Workbook.Worksheets[0];
                DataTable excelasTable = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    if (!string.IsNullOrEmpty(firstRowCell.Text))
                    {
                        string firstColumn = string.Format("Column {0}", firstRowCell.Start.Column);
                        var titleName = hasHeader ? firstRowCell.Text : firstColumn;
                        if (headerMapp != null && headerMapp.Count > 0)
                        {
                            if (!headerMapp.ContainsKey(titleName))
                            {
                                throw new Exception("导入数据格式不正");
                            }
                            titleName = headerMapp[titleName];
                        }
                        excelasTable.Columns.Add(titleName);
                    }
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, excelasTable.Columns.Count];
                    DataRow row = excelasTable.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                //将所有内容作为泛型获取，最终定是否强制转换为所需类型
                var generatedType = XMJson.Deserialize<T>(XMJson.Serailze(excelasTable));
                return (T)Convert.ChangeType(generatedType, typeof(T));
            }

        }
    }
}
