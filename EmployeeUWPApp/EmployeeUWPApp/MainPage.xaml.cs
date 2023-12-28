using EmployeeComponent;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EmployeeUWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<EmployeeViewModel> _employeesOC = null;
        public XamlUICommand ChangeFirstNameCommand = null;
        public MainPage()
        {
            this.InitializeComponent();

            Employees employees = new Employees();
            _employeesOC = employees.GetEmployees();
            //bind the OC to the list view
            EmployeesList.ItemsSource = _employeesOC;

            ChangeFirstNameCommand = new XamlUICommand();
            ChangeFirstNameCommand.ExecuteRequested += ChangeFirstNameCommand_ExecuteRequested;

            btnThankYou.Click += BtnThankYou_Click;
        }

        private void BtnThankYou_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        //method will be invoked when but first name changed is pressed
        private async void ChangeFirstNameCommand_ExecuteRequested(XamlUICommand sender, ExecuteRequestedEventArgs args)
        {

            ListView lv = (ListView)args.Parameter;
            //if an item in the listview is selected
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
