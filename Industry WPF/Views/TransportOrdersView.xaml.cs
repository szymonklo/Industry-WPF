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
using System.Data;

namespace Industry_WPF.Views
{
    /// <summary>
    /// Interaction logic for TransportOrdersView.xaml
    /// </summary>
    public partial class TransportOrdersView : UserControl
    {
        public TransportOrdersView()
        {
            InitializeComponent();
        }

        //private void Details3(object sender, RoutedEventArgs e)
        //{
        //    //TODO - w innny sposów (Icommand?)
        //    TransprtOrder transportOrder=  ((Button)e.Source).DataContext;
        //    DataRow dataRow = (DataRow) ((Button)e.Source).DataContext;
        //    MessageBox.Show($"sender: {sender.ToString()} e: {e.ToString()}");
        //}
    }
}
