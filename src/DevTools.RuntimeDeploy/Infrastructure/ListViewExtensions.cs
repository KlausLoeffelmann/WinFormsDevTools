// TODO(warp#93): Migrate to a WARP-provided source generator for
// type-safe ListView column/row binding. The reflection-based helpers
// below were carried over from the original DevTools.Libs project as a
// temporary stop-gap so that DevTools.RuntimeDeploy can drop its
// project reference on DevTools.Libs.
//
// Once the WARP-side generator ships (tracking issue:
// https://github.com/KlausLoeffelmannOrg/WARP/issues/93), the three
// helpers in this file should be deleted and the call sites in
// DeployRuntimeView / OverView re-pointed at the generator-produced
// strongly-typed equivalents (no PropertyInfo.GetValue, no boxing,
// no nameof strings).

namespace DevTools.RuntimeDeploy.Infrastructure;

internal static class ListViewExtensions
{
    public static void ConfigureDetailsView(
        this ListView listView,
        bool fullRowSelect = true,
        bool gridLines = false,
        bool checkBoxes = false)
    {
        listView.View = View.Details;
        listView.FullRowSelect = fullRowSelect;
        listView.GridLines = gridLines;
        listView.CheckBoxes = checkBoxes;
    }

    public static void CheckItemsInFirstColumn(this ListView listView, string[] itemsToCheck)
    {
        foreach (ListViewItem item in listView.Items)
        {
            if (itemsToCheck.Contains(item.SubItems[0].Text))
            {
                item.Checked = true;
            }
        }
    }

    public static void AddItemsWithColumnHeadersFromType<T>(
        this ListView listView,
        IEnumerable<T> dataItems,
        bool addSourceDataToTag,
        params (string PropertyName, string DisplayName)[] typePropertyColumnHeaderNames)
    {
        var properties = typeof(T).GetProperties();

        var columnProperties = typePropertyColumnHeaderNames
            .Join(properties,
                outerItem => outerItem.PropertyName,
                innerItem => innerItem.Name,
                (outer, inner) => new
                {
                    PropertyInfo = inner,
                    outer.DisplayName
                });

        listView.Items.Clear();
        listView.Columns.Clear();

        listView.BeginUpdate();

        // Build-up the Column Header
        foreach (var columnHeaderItem in columnProperties)
        {
            listView.Columns.Add(columnHeaderItem.DisplayName);
        }

        // Build up the actual list
        foreach (var dataItem in dataItems)
        {
            var first = false;
            ListViewItem? listViewItem = null;

            foreach (var columnItem in columnProperties)
            {
                if (!first)
                {
                    first = true;
                    listViewItem = listView.Items.Add(
                        columnItem
                            .PropertyInfo
                            .GetValue(dataItem)!
                            .ToString());

                    if (addSourceDataToTag)
                        listViewItem.Tag = dataItem;

                    continue;
                }

                listViewItem!.SubItems.Add(
                            columnItem
                                .PropertyInfo
                                .GetValue(dataItem)!
                                .ToString());
            }
        }

        foreach (ColumnHeader columnItem in listView.Columns)
        {
            // We're setting the column width to content width.
            columnItem.Width = -2;
        }

        listView.EndUpdate();
    }
}
