using System.Diagnostics;
using System.Windows;

namespace Chat.ClientWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Trace.Listeners.Add(new ClientTraceListener());
            base.OnStartup(e);
        }
    }
}
