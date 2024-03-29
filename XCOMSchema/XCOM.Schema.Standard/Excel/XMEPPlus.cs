﻿using OfficeOpenXml;
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
        /// <summary>
        /// 读取excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="hasHeader"></param>
        /// <param name="headerMapp"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T LoadExcel<T>(string filePath, bool hasHeader = true, Dictionary<string, string> headerMapp = null)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
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
        /// <summary>
        /// 读取excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <param name="hasHeader"></param>
        /// <param name="headerMapp"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T LoadExcelByStream<T>(Stream stream, bool hasHeader = true, Dictionary<string, string> headerMapp = null)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excelPack = new ExcelPackage())
            {
                //Load excel stream
                excelPack.Load(stream);
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
                                throw new Exception($"导入数据【{titleName}】非法");
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
        /// <summary>
        /// 生成excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <param name="data"></param>
        /// <param name="hasHeader"></param>
        /// <param name="headerMapp"></param>
        public static byte[] SaveExcel<T>(string filePath, string sheetName, List<T> data, bool hasHeader = true, Dictionary<string, string> headerMapp = null)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                var headerIndexList = new List<EPPlusExportModel>();
                var worksheet = package.Workbook.Worksheets.Add(sheetName);
                var rowIndex = 1;
                if (hasHeader && headerMapp != null && headerMapp.Count > 0)
                {
                    var index = 1;
                    foreach (KeyValuePair<string, string> item in headerMapp)
                    {
                        worksheet.Cells[rowIndex, index].Value = item.Key;
                        headerIndexList.Add(new EPPlusExportModel
                        {
                            CellName = item.Value,
                            ColumnIndex = index
                        });
                        index++;
                    }
                }
                else
                {
                    var index = 1;
                    var properties = typeof(T).GetProperties();
                    foreach (var item in properties)
                    {
                        worksheet.Cells[rowIndex, index].Value = item.Name;
                        headerIndexList.Add(new EPPlusExportModel
                        {
                            CellName = item.Name,
                            ColumnIndex = index
                        });
                        index++;
                    }
                }
                rowIndex++;
                data.ForEach(data =>
                {
                    var properties = typeof(T).GetProperties();
                    foreach (var item in properties)
                    {
                        if (headerIndexList.Exists(e => e.CellName == item.Name))
                        {
                            var cell = headerIndexList.FirstOrDefault(e => e.CellName == item.Name);
                            worksheet.Cells[rowIndex, cell.ColumnIndex].Value = item.GetValue(data);
                        }
                    }
                    rowIndex++;
                });
                package.Save();
                return package.GetAsByteArray();

            }
        }

    }

}

