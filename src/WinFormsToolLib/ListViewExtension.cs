namespace WinFormsToolLib
{
    public static class ListViewExtension
    {
        public static void ConfigureDetailsView(
            this ListView listView,
            bool fullRowSelect = true,
            bool gridLines = true,
            bool checkBoxes = false)
        {
            listView.View = View.Details;
            listView.FullRowSelect = fullRowSelect;
            listView.GridLines = gridLines;
            listView.CheckBoxes = checkBoxes;
        }

        /// <summary>
        /// Adds items for a list of certain types where the columns are selected by property names, 
        /// and the display names for columns can be configured.
        /// </summary>
        /// <typeparam name="T">Type of the items in the List.</typeparam>
        /// <param name="listView">The ListView control displaying headers and Items. 
        /// Should be in Details View for this to work correctly.</param>
        /// <param name="dataItems">The data elements.</param>
        /// <param name="typePropertyColumnHeaderNames">Tuple with Property/Displayname.</param>
        public static void AddItemsWithColumnHeadersFromType<T>(
            this ListView listView,
            IEnumerable<T> dataItems,
            bool addSourceDataToTag,
            params (string PropertyName,string DisplayName)[] typePropertyColumnHeaderNames)
        {
            // We're joining the columns we want to have with the type's properties.
            var columnProperties = typePropertyColumnHeaderNames
                .Join(
                    typeof(T).GetProperties(),
                    outerItem => outerItem.PropertyName,    // where outerTable.Name == inner 
                    innerItem => innerItem.Name,            // (the latter being string already).
                    (outer, inner) => new
                    {
                        PropertyInfo = inner,
                        DisplayName = outer.DisplayName
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
}
