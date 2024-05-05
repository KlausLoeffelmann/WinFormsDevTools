namespace WinFormsToolLib
{
    public static class ControlExtension
    {
        public async static Task ShowAsync(this Form form)
        {
            TaskCompletionSource taskCompletionSource = new TaskCompletionSource();

            if (!form.IsHandleCreated)
            {
                form.Load += Form_Load;
                form.Show();
                await taskCompletionSource.Task;
            }

            void Form_Load(object? sender, EventArgs e)
            {
                if (sender is Form form)
                {
                    form.Load -= Form_Load;
                    taskCompletionSource.SetResult();
                }
            }
        }


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
