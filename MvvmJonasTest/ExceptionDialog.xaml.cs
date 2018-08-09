using System.Windows;

namespace MvvmJonasTest
{
    /// <summary>
    /// Interaction logic for ExceptionDialog.xaml
    /// </summary>
    public partial class ExceptionDialog : Window
    {
        public ExceptionDialog(string exceptionText)
        {
            InitializeComponent();
            _exceptionText.Text = exceptionText;
        }
    }
}
