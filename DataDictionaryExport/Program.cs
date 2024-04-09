using DataDictionaryExport;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Data;
using System.Text;


// 创建工作簿
//建立空白工作簿
XSSFWorkbook book = new XSSFWorkbook();

#region Excel样式

IFont font = book.CreateFont();
font.FontHeightInPoints = 11;
font.FontName = "宋体";

IFont fontLink = book.CreateFont();
fontLink.IsBold = true;
fontLink.Color = HSSFColor.Blue.Index;
fontLink.FontName = "宋体";
fontLink.Underline = FontUnderlineType.Single;

//设置链接的样式：水平垂直对齐居中
ICellStyle cellLinkStyle = book.CreateCellStyle();
cellLinkStyle.Alignment = HorizontalAlignment.Left;
cellLinkStyle.VerticalAlignment = VerticalAlignment.Center;
//cellLinkStyle.BottomBorderColor = HSSFColor.Black.Index;
//cellLinkStyle.LeftBorderColor = HSSFColor.Black.Index;
//cellLinkStyle.RightBorderColor = HSSFColor.Black.Index;
//cellLinkStyle.TopBorderColor = HSSFColor.Black.Index;
cellLinkStyle.SetFont(fontLink);
//cellLinkStyle.FillPattern = FillPattern.SolidForeground;

//设置表头的样式：水平垂直对齐居中，加粗
ICellStyle titleCellStyle = book.CreateCellStyle();
titleCellStyle.Alignment = HorizontalAlignment.Center;
titleCellStyle.GetFont(book).IsBold = true;
titleCellStyle.BorderBottom = BorderStyle.Thick;
titleCellStyle.BorderLeft = BorderStyle.Thick;
titleCellStyle.BorderRight = BorderStyle.Thick;
titleCellStyle.BorderTop = BorderStyle.Thick;
titleCellStyle.VerticalAlignment = VerticalAlignment.Center;

titleCellStyle.FillForegroundColor = HSSFColor.LightOrange.Index;   //图案颜色
titleCellStyle.FillBackgroundColor = HSSFColor.LightOrange.Index;   //背景颜色
titleCellStyle.FillPattern = FillPattern.SolidForeground;


// 数据单元格样式
ICellStyle dataCellStyle = book.CreateCellStyle();
dataCellStyle.SetFont(font);
dataCellStyle.BorderBottom = BorderStyle.Thin;
dataCellStyle.BorderLeft = BorderStyle.Thin;
dataCellStyle.BorderRight = BorderStyle.Thin;
dataCellStyle.BorderTop = BorderStyle.Thin;

ICellStyle focusDataCellStyle = book.CreateCellStyle();
focusDataCellStyle.SetFont(font);
focusDataCellStyle.BorderBottom = BorderStyle.Thin;
focusDataCellStyle.BorderLeft = BorderStyle.Thin;
focusDataCellStyle.BorderRight = BorderStyle.Thin;
focusDataCellStyle.BorderTop = BorderStyle.Thin;
focusDataCellStyle.FillForegroundColor = HSSFColor.LightGreen.Index;   //图案颜色
focusDataCellStyle.FillBackgroundColor = HSSFColor.LightGreen.Index;   //背景颜色
focusDataCellStyle.FillPattern = FillPattern.SolidForeground;

ICellStyle focusFirstPageDataCellStyle = book.CreateCellStyle();
focusFirstPageDataCellStyle.SetFont(fontLink);
focusFirstPageDataCellStyle.BorderBottom = BorderStyle.Thin;
focusFirstPageDataCellStyle.BorderLeft = BorderStyle.Thin;
focusFirstPageDataCellStyle.BorderRight = BorderStyle.Thin;
focusFirstPageDataCellStyle.BorderTop = BorderStyle.Thin;
focusFirstPageDataCellStyle.FillForegroundColor = HSSFColor.LightGreen.Index;   //图案颜色
focusFirstPageDataCellStyle.FillBackgroundColor = HSSFColor.LightGreen.Index;   //背景颜色
focusFirstPageDataCellStyle.FillPattern = FillPattern.SolidForeground;
#endregion


//在工作簿中：建立空白工作表
ISheet firstSheet = book.CreateSheet("表名列表");
IRow firstRow = firstSheet.CreateRow(0);//（第一行写标题)
firstRow.CreateCell(0).SetCellValue("序号");//第一列标题，以此类推
firstRow.CreateCell(1).SetCellValue("表名");
firstRow.CreateCell(2).SetCellValue("说明");
firstRow.Cells[0].CellStyle = titleCellStyle;
firstRow.Cells[1].CellStyle = titleCellStyle;
firstRow.Cells[2].CellStyle = titleCellStyle;
firstRow.HeightInPoints = 25;
SetColumnWidthAutoFixed(firstSheet);

DataTable tabelNameDt = Db.GetDataTable(Const.Sql_QueryTableInfo);
// 添加表名及创建对应sheet页
for (int i = 0; i < tabelNameDt.Rows.Count; i++)
{
    int count = i + 1;
    string tableName = tabelNameDt.Rows[i][0].ToString();
    string tableComment = tabelNameDt.Rows[i][1].ToString();

    IRow tmpRow = firstSheet.CreateRow(count);//（第一行写标题)
    tmpRow.HeightInPoints = 25;
    tmpRow.CreateCell(0).SetCellValue(count);//第一列标题，以此类推
    tmpRow.Cells[0].CellStyle = dataCellStyle;

    ICell nameCell = tmpRow.CreateCell(1);
    nameCell.SetCellValue(tableName);

    tmpRow.CreateCell(2).SetCellValue(tableComment);

    ISheet tmpSheet = book.CreateSheet(tableComment);

    SetHyperLinkToSheet(nameCell, tableComment);
    nameCell.CellStyle = focusFirstPageDataCellStyle;

    string sql = Const.Sql_GetTableFieldInfo(tableName);


    DataTable dt = Db.GetDataTable(sql);
    ProcessSheetData(tmpSheet, dt);
}

//导出
using MemoryStream ms = new();
book.Write(ms);

string databaseName = Db.GetString(Const.Sql_QueryDatabaseName);
using FileStream file = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{databaseName}数据字典{DateTime.Now:yyyyMMddHHmmss}.xlsx"), FileMode.OpenOrCreate);
book.Write(file);
//file.Flush();



void ProcessSheetData(ISheet sheet, DataTable dt)
{
    // 第一行写返回连接
    IRow row1 = sheet.CreateRow(0);
    row1.HeightInPoints = 30;
    ICell returnCell = row1.CreateCell(0);
    returnCell.SetCellValue("返回表名列表");
    SetHyperLinkToSheet(returnCell, "表名列表");
    sheet.AddMergedRegion(new CellRangeAddress(0, 0, 1, 5));//标题合并单元格操作，6为总列数
    row1.CreateCell(1).SetCellValue(sheet.SheetName);
    row1.Cells[1].CellStyle= titleCellStyle;

    //第二行写标题
    IRow row2 = sheet.CreateRow(1);
    row2.HeightInPoints = 25;  //设置字体大小

    ICell titleCell1 = row2.CreateCell(0);
    titleCell1.CellStyle = titleCellStyle;
    titleCell1.SetCellValue("序号");
    titleCell1.CellStyle.Alignment = HorizontalAlignment.Left;
    for (int i = 0; i < dt.Columns.Count; i++)
    {
        ICell titleCell = row2.CreateCell(i + 1);
        titleCell.CellStyle = titleCellStyle;
        titleCell.SetCellValue(dt.Columns[i].ColumnName);
    }

    // 开始写数据
    int startIndex = 2;
    for (int i = 0; i < dt.Rows.Count; i++)
    {
        // 创建行
        int count = i + startIndex;
        IRow tmpRows = sheet.CreateRow(count);
        tmpRows.HeightInPoints = 18;  //设置字体大小
                                      // 首列序号
        ICell dataCell1 = tmpRows.CreateCell(0);
        dataCell1.CellStyle = dataCellStyle;
        dataCell1.SetCellValue(i + 1);
        // 数据列
        for (int j = 0; j < dt.Columns.Count; j++)
        {
            ICell dataCell = tmpRows.CreateCell(j + 1);

            if (dt.Columns[j].ColumnName == "字段名称" || dt.Columns[j].ColumnName == "备注")
            {
                dataCell.CellStyle = focusDataCellStyle;
            }
            else
            {
                dataCell.CellStyle = dataCellStyle;
            }
            dataCell.SetCellValue(dt.Rows[i][j].ToString());
        }
    }
    SetColumnWidthAutoFixed(sheet);
}

//设置sheet页内单元格自适应宽度
void SetColumnWidthAutoFixed(ISheet sheet)
{
    for (int columnNum = 0; columnNum <= 10; columnNum++)
    {
        int columnWidth = (int)(sheet.GetColumnWidth(columnNum) / 256);
        for (int rowNum = 0; rowNum <= sheet.LastRowNum; rowNum++)
        {
            IRow currentRow = sheet.GetRow(rowNum);
            if (currentRow.GetCell(columnNum) != null)
            {
                ICell currentCell = currentRow.GetCell(columnNum);
                int length = Encoding.Default.GetBytes(currentCell.ToString()).Length + 5;
                if (columnWidth < length)
                {
                    columnWidth = length;
                }
            }
        }
        sheet.SetColumnWidth(columnNum, columnWidth * 256);
    }
}

// 设置单元格链接至当前工作簿指定Sheet页
void SetHyperLinkToSheet(ICell cell, string sheetName)
{
    XSSFHyperlink link = new(HyperlinkType.Document);
    link.Address = $"#{sheetName}!A1";
    cell.Hyperlink = link;
    cell.CellStyle = cellLinkStyle;
}
