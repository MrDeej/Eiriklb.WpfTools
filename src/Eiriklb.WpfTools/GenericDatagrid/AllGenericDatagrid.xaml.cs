using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Eiriklb.WpfTools.GenericDatagrid
{
    /// <summary>
    /// Interaction logic for AllGenericDatagrid.xaml
    /// </summary>
    public partial class AllGenericDatagrid : Window
    {
        public AllGenericDatagrid()
        {
            InitializeComponent();
        }


        public static AllGenericDatagrid CreateInstance<T>(ObservableCollection<T> items, [CallerArgumentExpression("items")] string? title2 = null)
        {
            var title = typeof(T).Name;

            if (title2 != null)
                title = title2 + " - " + title;

            var window = new AllGenericDatagrid
            {
                Dg =
                {
                    ItemsSource = items
                },
                Title = title
            };


            return window;
        }
        public static AllGenericDatagrid CreateInstanceFromList<T>(List<T> items, [CallerArgumentExpression("items")] string? title2 = null)
        {
            var title = typeof(T).Name;

            if (title2 != null)
                title = title2 + " - " + title;

            var window = new AllGenericDatagrid
            {
                Dg =
                {
                    ItemsSource = items
                },
                Title = title
            };


            return window;
        }

        private void Dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.RowsSelected = this.Dg.SelectedItems is { Count: > 0 } || this.Dg.SelectedItem != null;
        }

        private void BtnJson_Click(object sender, RoutedEventArgs e)
        {
            string? json = null;
            string typeName = "";

            object? selectedItem = null;
            if (this.Dg.SelectedItems is { Count: > 0 })
            {
                selectedItem = this.Dg.SelectedItems[0];
            }
            else if (this.Dg.SelectedItem != null)
            {
                selectedItem = this.Dg.SelectedItem;
            }

            throw new NotImplementedException();
            if (selectedItem != null)
            {
                typeName = selectedItem.GetType().Name;

                //// Try to get the ToProto extension method for the type
                //var toProtoMethod = typeof(ExtensionMethods).GetMethod("ToProto", new[] { selectedItem.GetType() });

                //if (toProtoMethod != null)
                //{
                //    // If the method exists, invoke it
                //    var protoObject = toProtoMethod.Invoke(null, new[] { selectedItem });
                //    json = JsonConvert.SerializeObject(protoObject, Formatting.Indented);
                //}
                //else
                //{
                //    json = JsonConvert.SerializeObject(selectedItem, Formatting.Indented);
                //}
            }

            //if (json != null)
            //    new JsonViewer.JsonViewerSmall(json, typeName).Show();
        }
    }
}
