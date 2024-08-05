#define USEPYD
//#define Beta

using adminlib;

namespace BambuLab_FailureDetection
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
#if WINDOWS
            CWAdminLib adminLib = new CWAdminLib();
            bool Admin = adminLib.IsAdmin();
            if (!Admin)
            {
                string ApplicationPath = AppContext.BaseDirectory;
                ApplicationPath += "BambuLab-FailureDetection.exe";
                adminLib.RestartAsAdmin(ApplicationPath);
                Application.Current.Quit();
            }
#endif
        }
        private async void SetupButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.NewSettings());
            /*
            CWAdminLib adminLib = new CWAdminLib();
            bool Admin = adminLib.IsAdmin();
            if (Admin)
            {
                
            }
            else
            {
                string ApplicationPath = AppContext.BaseDirectory;
                ApplicationPath += "BambuLab-FailureDetection.exe";
                adminLib.RestartAsAdmin(ApplicationPath);
                Application.Current.Quit();
            }
            */
        }

        private async void ViewButton_Clicked(object sender, EventArgs e)
        {
#if Beta
            await Navigation.PushAsync(new Views.ViewPrinter());
#endif
        }
    }

}
