using EmployeeComponent;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace EmployeeUWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<EmployeeViewModel> _employeesOC = null;
        public XamlUICommand ChangeFirstNameCommand = null;
        public MainPage()
        {
            InitializeComponent();

            Employees employees = new Employees() ;

            _employeesOC = employees.GetEmployees();

            EmployeesList.ItemsSource = _employeesOC;
            ChangeFirstNameCommand = new XamlUICommand();
            ChangeFirstNameCommand.ExecuteRequested += ChangeFirstNameCommand_ExecuteRequested;

        }

        private async void ChangeFirstNameCommand_ExecuteRequested(XamlUICommand sender, ExecuteRequestedEventArgs args)
        {
            ListView lv = (ListView)args.Parameter;

            if (lv.SelectedIndex != -1)
            {
                await SpeakAsync($"Changing First name, from {_employeesOC[lv.SelectedIndex].FirstName}, to, {txtFirstName.Text}");
                _employeesOC[lv.SelectedIndex].FirstName = txtFirstName.Text;
            }
        }

        private async Task SpeakAsync(string text)
        {

            MediaElement mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(text);
            mediaElement.SetSource(stream, stream.ContentType);

        }
    }
}
