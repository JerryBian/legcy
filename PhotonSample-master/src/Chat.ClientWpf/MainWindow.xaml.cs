using System;
using System.Threading.Tasks;
using System.Windows;

namespace Chat.ClientWpf
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

        public void AppendToOutput(string message)
        {
            UpdateUi(() =>
            {
                Output.AppendText(Environment.NewLine + message);
                Output.SelectionStart = Output.Text.Length;
                Output.ScrollToEnd();
            });
        }

        private void NewPlayerButton(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                var obj = new ChatClient();
                obj.Execute();
            });
        }

        private void UpdateUi(Action action)
        {
            if (Dispatcher.CheckAccess())
            {
                action();
                return;
            }

            Dispatcher.Invoke(action);
        }
    }
}
