using DataCollectionService.Entities;
using DataCollectionService.Helpers;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionService.Services
{
   public class ExcelPackageService
    {
        public static ExcelPackage CreateExcelPackage(IList<ClientCard> clientCards)
        {
            var package = new ExcelPackage();
            var clientCardsExcel = ClientCardForExcel.ConvertToListClientCardForExcel(clientCards);
            package.Workbook.Properties.Title = "Clients Report";
            package.Workbook.Properties.Author = "System";
            package.Workbook.Properties.Subject = "ClientCards Report";
            package.Workbook.Properties.Keywords = "ClientCard";
            var worksheet = package.Workbook.Worksheets.Add("ClientCards");
            worksheet.Cells[1, 1].Value = "CardId";
            worksheet.Cells[1, 2].Value = "ClientName";
            worksheet.Cells[1, 3].Value = "ClientAdress";
            worksheet.Cells[1, 4].Value = "Phone";
            worksheet.Cells[1, 5].Value = "E-mail";
            worksheet.Cells[1, 6].Value = "Equips";
            worksheet.Cells[1, 7].Value = "Breackage";
            worksheet.Cells[1, 8].Value = "MasterName";
            worksheet.Cells[1, 9].Value = "MasterNumber";
            worksheet.Cells[1, 10].Value = "PutDate";
            worksheet.Cells[1, 11].Value = "PerformDate";
            worksheet.Cells[1, 12].Value = "Work";
            worksheet.Cells[1, 13].Value = "RepairEquips";

            //Add values
            var numberformat = "#,##0";
            var dataCellStyleName = "TableNumber";
            var numStyle = package.Workbook.Styles.CreateNamedStyle(dataCellStyleName);
            numStyle.Style.Numberformat.Format = numberformat;
            worksheet.Cells["A2"].LoadFromCollection(clientCardsExcel);
            worksheet.Cells[2, 10, 2 + clientCardsExcel.Count, 10].Style.Numberformat.Format = "dd-mm-yyyy";
            worksheet.Cells[2, 11, 2 + clientCardsExcel.Count, 11].Style.Numberformat.Format = "dd-mm-yyyy";

            // Add to table / Add summary row
            var tbl = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: clientCardsExcel.Count + 1, toColumn: 13), "Data");
            tbl.ShowHeader = true;
            tbl.TableStyle = TableStyles.Dark9;


            // AutoFitColumns
            worksheet.Cells[1, 1, clientCardsExcel.Count, 13].AutoFitColumns();
            worksheet.Cells[2, 12, 2 + clientCardsExcel.Count, 12].Style.WrapText = true;
            worksheet.Cells[2, 13, 2 + clientCardsExcel.Count, 13].Style.WrapText = true;
            return package;
        }
    }
}
