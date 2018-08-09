using System;
using System.Windows;
using System.Windows.Threading;

namespace MvvmJonasTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //   AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //  Dispatcher.CurrentDispatcher.UnhandledException += CurrentDispatcher_UnhandledException;



            InitializeComponent();

            //var data = ModelGenerator.GenerateUserModels();
            //DataContext = 
        }

        private void CurrentDispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            new ExceptionDialog(e.Exception.ToString()).ShowDialog();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

            //   new ExceptionDialog(e.ExceptionObject.ToString()).ShowDialog();

            //try
            //{
            //    //CommandManager.RequerySuggested CommandManager.RequerySuggested = null;
            //    //var SomeEvent = (EventHandler)Delegate.RemoveAll(CommandManager.RequerySuggested, CommandManager.RequerySuggested);
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //    //throw;
            //}
        }
    }
}
