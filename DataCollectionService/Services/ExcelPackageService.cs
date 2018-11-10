using System.Collections.Generic;
using System.Linq;
using DataCollectionService.Entities;
using DataCollectionService.Helpers;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace DataCollectionService.Services
{
    public class ExcelPackageService
    {
        public static ExcelPackage CreateExcelPackage(IList<ClientCard> clientCards)
        {
            var package = new ExcelPackage();
            var cardsForOutput = ClientCardForOutput.ConvertToListClientCardForOutput(clientCards);
            package.Workbook.Properties.Title = "Clients Report";
            package.Workbook.Properties.Author = "System";
            package.Workbook.Properties.Subject = "ClientCards Report";
            package.Workbook.Properties.Keywords = "ClientCard";
            var worksheet = package.Workbook.Worksheets.Add("ClientCards");
            worksheet.Cells[1, 1].Value = "CardId";
            worksheet.Cells[1, 2].Value = "ContractId";
            worksheet.Cells[1, 3].Value = "ClientName";
            worksheet.Cells[1, 4].Value = "ClientAdress";
            worksheet.Cells[1, 5].Value = "Phone";
            worksheet.Cells[1, 6].Value = "E-mail";
            worksheet.Cells[1, 7].Value = "Equips";
            worksheet.Cells[1, 8].Value = "Breackage";
            worksheet.Cells[1, 9].Value = "MasterName";
            worksheet.Cells[1, 10].Value = "MasterNumber";
            worksheet.Cells[1, 11].Value = "PutDate";
            worksheet.Cells[1, 12].Value = "PerformDate";
            worksheet.Cells[1, 13].Value = "Work";
            worksheet.Cells[1, 14].Value = "RepairEquips";

            // Add values
            const string numberformat = "#,##0";
            const string dataCellStyleName = "TableNumber";
            var numStyle = package.Workbook.Styles.CreateNamedStyle(dataCellStyleName);
            numStyle.Style.Numberformat.Format = numberformat;
            worksheet.Cells["A2"].LoadFromCollection(cardsForOutput);
            worksheet.Cells[2, 10, 2 + cardsForOutput.Count, 11].Style.Numberformat.Format = "dd-mm-yyyy";
            worksheet.Cells[2, 11, 2 + cardsForOutput.Count, 12].Style.Numberformat.Format = "dd-mm-yyyy";

            // Add to table style
            var tbl = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: cardsForOutput.Count + 1, toColumn: 14), "Data");
            tbl.ShowHeader = true;
            tbl.TableStyle = TableStyles.Dark9;

            // AutoFitColumns and Wraptext
            worksheet.Cells[1, 1, cardsForOutput.Count, 14].AutoFitColumns();
            worksheet.Cells[2, 13, 2 + cardsForOutput.Count, 13].Style.WrapText = true;
            worksheet.Cells[2, 14, 2 + cardsForOutput.Count, 14].Style.WrapText = true;
            return package;
        }
    }
}
