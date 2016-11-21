using antestapp1.View_Layer;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace antestapp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UCSize();
            ucListView.ShowAddViewArgs += new EventHandler(ShowAddView);
            ucListView.ShowDetailViewFirstTimeArgs += new EventHandler(ShowDetailsFirstTime);
            ucAddView.HideAddViewArgs += new EventHandler(HideAddView);
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            //ShowHidePanel("showPanel");
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            //ShowHidePanel("hidePanel");
        }

        protected void ShowDetailsFirstTime(object sender, EventArgs e)
        {
            Storyboard sb = Resources["showDetailsView"] as Storyboard;
            sb.Begin(_ucDetailsView); 
        }

        protected void ShowAddView(object sender, EventArgs e)
        {
            Storyboard sb = Resources["showAddView"] as Storyboard;
            sb.Begin(_ucAddView);
        }

        protected void HideAddView(object sender, EventArgs e)
        {
            Storyboard sb = Resources["hideAddView"] as Storyboard;
            sb.Begin(_ucAddView); 
        }

        private void UCSize()
        {
            Thickness t = new Thickness();
            t.Bottom = 350;
            t.Left = 0;
            t.Top = 1500;
            t.Right = 0;
            _ucAddView.Margin = t;
        }
    }
}
