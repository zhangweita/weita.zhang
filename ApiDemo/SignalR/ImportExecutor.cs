using ApiDemo.Common;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace ApiDemo.SignalR;

public class ImportExecutor(ILogger<ImportExecutor> logger, IHubContext<ImportDictHub> hubContext, IOptionsSnapshot<ConnStringOptions> connOptions)
{
    private readonly ILogger<ImportExecutor> logger = logger;
    private readonly IHubContext<ImportDictHub> hubContext = hubContext;
    private readonly IOptionsSnapshot<ConnStringOptions> connOptions = connOptions;

    public async Task ExecuteAsync(string connectionId)
    {
        try
        {
            await DoExecuteAsync(connectionId);
        }
        catch (Exception ex)
        {
            await hubContext.Clients.Client(connectionId).SendAsync("Failed");

            logger.LogError(ex, "ImportExecutor出现异常");
        }
    }

    public async Task DoExecuteAsync(string connectionId)
    {
        string[] lines = await File.ReadAllLinesAsync(@"D:\ecdict.csv");

        var client = hubContext.Clients.Client(connectionId);
        await client.SendAsync("Started");

        using SqlConnection conn = new(connOptions.Value.Default);
        await conn.OpenAsync();

        using SqlBulkCopy bulkCopy = new(conn);
        bulkCopy.DestinationTableName = "T_WordItems";
        bulkCopy.ColumnMappings.Add("Word", "Word");
        bulkCopy.ColumnMappings.Add("Phonetic", "Phonetic");
        bulkCopy.ColumnMappings.Add("Definition", "Definition");
        bulkCopy.ColumnMappings.Add("Translation", "Translation");

        DataTable dataTable = new();
        dataTable.Columns.Add("Word");
        dataTable.Columns.Add("Phonetic");
        dataTable.Columns.Add("Definition");
        dataTable.Columns.Add("Translation");
        int count = lines.Length;
        for (int i = 1; i < count; i++)
        {
            string line = lines[i];
            var dataRow = dataTable.NewRow();
            string[] segments = line.Split(',');
            dataRow["Word"] = segments[0];
            dataRow["Phonetic"] = segments[1];
            dataRow["Definition"] = segments[2];
            dataRow["Translation"] = segments[3];
            dataTable.Rows.Add(dataRow);

            if (dataTable.Rows.Count == 1000)
            {
                await bulkCopy.WriteToServerAsync(dataTable);
                dataTable.Clear();
                await client.SendAsync("ImportProgress", i, count);
            }
        }
        // 最后一组数据
        await client.SendAsync("ImportProgress", count, count);
        await bulkCopy.WriteToServerAsync(dataTable);
        await client.SendAsync("Completed");
    }
}
