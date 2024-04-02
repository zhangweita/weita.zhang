using System;
using System.Linq;

namespace DataDictionaryExport
{
    public static class Const
    {

        public static string Sql_QueryDatabaseName = "select name from V$database";
        public static string Sql_QueryTableInfo = @"SELECT t.TABLE_NAME,c.COMMENTS FROM USER_TABLES t
                                            JOIN USER_TAB_COMMENTS c
                                            ON t.TABLE_NAME=c.TABLE_NAME";

        public static string Sql_GetTableFieldInfo(string tableName) => $@"SELECT A.table_name                 AS ""表名"",
                                       A.column_name                AS ""字段名称"",
                                       Decode(A.char_length, 0, Decode(A.data_scale, NULL, A.data_type,
                                                                                     A.data_type
                                                                                     ||'('
                                                                                     ||A.data_precision
                                                                                     ||','
                                                                                     ||A.data_scale
                                                                                     ||')'),
                                                             A.data_type
                                                             ||'('
                                                             ||A.char_length
                                                             ||')') AS ""字段类型"",
                                --       A.data_type                  AS ""字段类型"",
                                --       A.data_precision             AS ""有效位"",
                                --       A.data_scale                 AS ""精度值"",
                                --       A.char_length                AS ""字段长度"",
                                       A.nullable                   AS ""能否为空"",
	                                   B.COMMENTS 					AS ""备注""
                                FROM   sys.user_tab_columns A
                                LEFT JOIN user_col_comments B ON A.TABLE_NAME = B.TABLE_NAME AND A.COLUMN_NAME = B.COLUMN_NAME
                                WHERE  A.table_name = '{tableName}'";
    }
}
