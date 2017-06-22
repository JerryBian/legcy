using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace Chat.ClientWpf
{
    public class ClientTraceListener : TraceListener
    {
        private static readonly object LockObject = new object();

        public override void Write(string message)
        {

        }

        public override void WriteLine(string message)
        {
            UpdateUi(() =>
            {
                var mainWindows = System.Windows.Application.Current.MainWindow as MainWindow;
                mainWindows?.AppendToOutput(message);
            });
        }

        private void UpdateUi(Action action)
        {
            if (System.Windows.Application.Current.CheckAccess())
            {
                action();
                return;
            }

            System.Windows.Application.Current.Dispatcher.Invoke(action);
        }
    }
}
