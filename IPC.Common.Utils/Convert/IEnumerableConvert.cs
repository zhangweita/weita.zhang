using System.Data;
using System.Reflection;

namespace IPC.Common.Utils.Convert;

public static class IEnumerableConvertExtension
{
    public static DataTable ToTable<T>(this IEnumerable<T> collection)
    {
        PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        DataTable dt = new();
        dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());

        for (int i = 0; i < collection.Count(); i++)
        {
            object[] array = props.Select(p => p.GetValue(collection.ElementAt(i)!, null)!).ToArray();
            dt.LoadDataRow(array, true);
        }
        return dt;
    }

    public static async Task<DataTable> ToTableAsync<T>(this IAsyncEnumerable<T> collection)
    {
        PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        DataTable dt = new();
        dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());

        await foreach (T item in collection)
        {
            object[] array = props.Select(p => p.GetValue(item!, null)!).ToArray();
            dt.LoadDataRow(array, true);
        }
        return dt;
    }
}
