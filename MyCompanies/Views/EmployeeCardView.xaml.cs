using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace MyCompanies.Views
{
    public partial class EmployeeCardView : Window
    {
        private readonly Regex _regex = new("[^0-9]+");

        public EmployeeCardView()
        {
            InitializeComponent();
        }

        private void OnTextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }
    }
}
