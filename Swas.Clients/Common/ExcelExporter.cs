using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using MSExcel = OfficeOpenXml;


namespace Swas.Clients.Common
{
    public class ExcelExporter
    {
        #region Delegates

        delegate string ExcelColumnNames(int columnIndex);

        #endregion

        #region Methods

        public static string ExportData<T>(IList<T> Source, string sheetName)
        {
            ExcelColumnNames ecn = (param) =>
            {
                return (param <= 25
                        ? Convert.ToString((char)(65 + param))
                        : Convert.ToString((char)(64 + param / 26)) + Convert.ToString((char)(65 + param % 26)));
            };

            var fileName = string.Format("{0}.xlsx", Guid.NewGuid());
            var exportFileName = String.Format("{0}Temp\\{1}", HttpContext.Current.Request.PhysicalApplicationPath, fileName);
            try
            {
                if (Source.Any())
                {
                    if (File.Exists(exportFileName))
                        File.Delete(exportFileName);

                    using (MSExcel.ExcelPackage ep = new MSExcel.ExcelPackage(new FileInfo(exportFileName)))
                    {
                        ep.Workbook.Worksheets.Add(sheetName);
                        ep.Workbook.CalcMode = MSExcel.ExcelCalcMode.AutomaticNoTable;

                        MSExcel.ExcelWorksheet ws = ep.Workbook.Worksheets[sheetName];
                        ws.Cells.Style.Font.Name = "Sylfaen";
                        ws.Cells.Style.Font.Size = 10;
                        ws.View.ZoomScale = 100;

                        ws.Cells["A2"].LoadFromCollection(Source, false, OfficeOpenXml.Table.TableStyles.None);

                        PropertyInfo[] pList = Source[0].GetType().GetProperties();
                        for (int i = 0; i < pList.Length; i++)
                        {
                            object[] attributes = pList[i].GetCustomAttributes(typeof(DisplayAttribute), false);
                            if (attributes.Any())
                            {
                                ws.Cells[String.Format("{0}1", ecn(i))].Style.Font.Bold = true;
                                ws.Cells[String.Format("{0}1", ecn(i))].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                ws.Cells[String.Format("{0}1", ecn(i))].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                                ws.Cells[String.Format("{0}1", ecn(i))].Style.Border.BorderAround(MSExcel.Style.ExcelBorderStyle.Medium, Color.Black);
                                ws.Cells[String.Format("{0}1", ecn(i))].Value = (attributes[0] as DisplayAttribute).Name;
                            }

                            if (pList[i].PropertyType == typeof(DateTime))
                                ws.Column(i + 1).Style.Numberformat.Format = "dd/MM/yyyy";
                            else if (pList[i].PropertyType == typeof(Int16) ||
                                     pList[i].PropertyType == typeof(Int32) ||
                                     pList[i].PropertyType == typeof(Int64))
                                ws.Column(i + 1).Style.Numberformat.Format = "#0";
                            else if (pList[i].PropertyType == typeof(decimal) ||
                                     pList[i].PropertyType == typeof(float) ||
                                     pList[i].PropertyType == typeof(double))
                                ws.Column(i + 1).Style.Numberformat.Format = "#0.00";

                            ws.Column(i + 1).AutoFit(25.5, 105.5);
                            ws.Column(i + 1).Style.WrapText = true;
                            ws.Column(i + 1).Style.ReadingOrder = OfficeOpenXml.Style.ExcelReadingOrder.LeftToRight;
                            ws.Column(i + 1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            ws.Column(i + 1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        }

                        ep.Save();
                    }
                }
            }
            catch (Exception ex) { throw ex; }

            return fileName;
        }

        #endregion
    }
}
