using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TableReader;
namespace TaskList_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            MongoManager man = new MongoManager();   
            TaskUser user1 = new TaskUser("alex","1234");
            try
            {
                user1.Login();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
         }
    }
}
