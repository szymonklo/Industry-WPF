using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModelLibrary.Models;
using Industry_WPF.ViewModels;

namespace Industry_WPF.Views
{
    /// <summary>
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();
            ProductsView productView = this;
        }

        private void createNewWorldButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"Creating New World - started...");
            World world = new World();

            //MessageBox.Show($"Creating New World - finished succesfully");
            
            //ProductViewModel.Load();
        }

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void OrderColumns_Click(object sender, RoutedEventArgs e)
        {
            ProductDataGrid.Columns.Where(x => x.Header.ToString() == "Name").First().DisplayIndex = 0;
        }
    }
}
