// Author:      Li Leo Wang
// Start Date:  2019-07-30
// Description:
//      - C# tutorial 01: WPF application with dispatcher 
//        to update GUI controls.
// Notes:
//      - (none)
//
// Change history:
//      - Refer to GitHub comments related to each source file.
//

using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Cs_tutorial_01_WPF_dispatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _count = 0;
        private delegate void Update_label_background_callback(int state);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _count++;
            lblMessage.Content = "Click: " + _count.ToString();
        }

        private void btnContinous_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Long_running();
            }).Start();

            btnContinous.IsEnabled = false;
        }

        private void Long_running()
        {
            try
            {

                while (true)
                {
                    int delay_ms = 500;

                    //Update_label_background(1);
                    //lblMessage.Dispatcher.BeginInvoke(new Update_label_background_callback(Update_label_background), new object[] { 1 });
                    Dispatcher.BeginInvoke(new Update_label_background_callback(Update_label_background), DispatcherPriority.Render, new object[] { 1 });
                    Thread.Sleep(delay_ms);

                    //Update_label_background(2);
                    //lblMessage.Dispatcher.BeginInvoke(new Update_label_background_callback(Update_label_background), new object[] { 2 });
                    Dispatcher.BeginInvoke(new Update_label_background_callback(Update_label_background), DispatcherPriority.Render, new object[] { 2 });
                    Thread.Sleep(delay_ms);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Update_label_background(int state)
        {
            switch (state)
            {
                case 1:
                    lblMessage.Background = Brushes.Red;
                    break;

                case 2:
                    lblMessage.Background = Brushes.Yellow;
                    break;

                default:
                    break;
            }

        }
    }
}
