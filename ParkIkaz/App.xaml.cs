using MauiApp1;

namespace ParkIkaz
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new BarcodeOkuyucu();
        }
    }
}
