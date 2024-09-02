using System.Data;
using System.Reflection;

namespace HelperKit;

public static partial class Extensions
{
    /// <summary>
    /// Verifies if the collection contains duplicate values
    /// </summary>
    /// <param name="items">The enumeration to validate for duplicates</param>
    /// <typeparam name="T">Element type</typeparam>
    /// <returns>true if any element in the sequence is duplicated</returns>
    public static bool ContainsDuplicates<T>(this IEnumerable<T> items)
    {
        HashSet<T> set = [];
        return !items.All(set.Add);
    }

    /// <summary>
    /// Verifies if the collection contains duplicate values
    /// </summary>
    /// <param name="items">The enumeration to validate for duplicates</param>
    /// <param name="predicate">A function for each element to compare</param>
    /// <typeparam name="T">Element type</typeparam>
    /// <typeparam name="TKey">Element Selector type</typeparam>
    /// <returns>true if any element in the sequence is duplicated</returns>
    public static bool ContainsDuplicates<T, TKey>(this IEnumerable<T> items, Func<T, TKey> predicate)
    {
        HashSet<TKey> set = [];
        return items.Any(x => !set.Add(predicate(x)));
    }

    /// <summary>
    /// Distinct by specific property.
    /// </summary>
    /// <param name="items"></param>
    /// <param name="predicate">Function to pass object for distinct to work.</param>
    public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> predicate)
    {
        HashSet<TKey> set = [];
        foreach (var item in items)
        {
            if (set.Add(predicate(item)))
                yield return item;
        }
    }

    /// <summary>
    /// Converts an enumeration of groupings into a Dictionary of those groupings.
    /// </summary>
    /// <typeparam name="TKey">Key type of the grouping and dictionary.</typeparam>
    /// <typeparam name="T">Element type of the grouping and dictionary list.</typeparam>
    /// <param name="grouping">The enumeration of groupings from a GroupBy() clause.</param>
    /// <returns>A dictionary of groupings such that the key of the dictionary is TKey type and the value is List of TValue type.</returns>
    public static Dictionary<TKey, List<T>> ToDictionary<TKey, T>(this IEnumerable<IGrouping<TKey, T>> grouping)
    {
        return grouping.ToDictionary(x => x.Key, x => x.ToList());
    }

    /// <summary>
    /// Converts an IEnumerable object to Datatable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    /// <exception cref="MissingFieldException"></exception>
    public static DataTable ToDataTable<T>(this IEnumerable<T> items) where T : class
    {
        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        if (props is {Length: 0})
            throw new MissingFieldException("The implemented type doesn't have valid fields");

        var dataTable = new DataTable(typeof(T).Name);
        foreach (var prop in props)
            dataTable.Columns.Add(prop.Name, prop.PropertyType);

        if (items is null)
            return dataTable;

        foreach (var item in items)
        {
            if (item is null)
                continue;

            var values = new object[props.Length];
            for (var i = 0; i < props.Length; i++)
                values[i] = props[i].GetValue(item, null);

            dataTable.Rows.Add(values);
        }

        return dataTable;
    }
}
