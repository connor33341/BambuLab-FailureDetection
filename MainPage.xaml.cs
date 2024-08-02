using adminlib;

namespace BambuLab_FailureDetection
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }
        private async void SetupButton_Clicked(object sender, EventArgs e)
        {
            CWAdminLib adminLib = new CWAdminLib();
            bool Admin = adminLib.IsAdmin();
            if (Admin)
            {
                await Navigation.PushAsync(new Views.NewSettings());
            }
            else
            {
                string ApplicationPath = AppContext.BaseDirectory;
                ApplicationPath += "BambuLab-FailureDetection.exe";
                adminLib.RestartAsAdmin(ApplicationPath);
                Application.Current.Quit();
            }
        }

        private void ViewButton_Clicked(object sender, EventArgs e)
        {

        }
    }

}
