using Newtonsoft.Json;
using ParkIkaz;

namespace MauiApp1;

public partial class BarcodeOkuyucu : ContentPage
{
    public BarcodeOkuyucu()
    {
        InitializeComponent();
        this.Loaded += BarcodeOkuyucu_Loaded;

    }

    private async void BarcodeOkuyucu_Loaded(object? sender, EventArgs e)
    {
        //var page = new IkazBilgisiGonder("zerenaisenur@gmail.com");
        //await this.Navigation.PushModalAsync(page, false);
    }

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (cView.Cameras.Count > 0)
        {
            cView.Camera = cView.Cameras.First();
            MainThread.BeginInvokeOnMainThread(async () =>
            {

                await cView.StopCameraAsync();
                await cView.StartCameraAsync();

            });

        }
    }
    private bool oku = false;
    private void cameraView_BarcodeDedected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            if (oku) return;
            oku = true;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://192.168.2.133:80/arac-sahibi/get?Id=" + args.Result[0].Text);
            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            var kullanici = JsonConvert.DeserializeObject<TblAracSahibi>(result);
            var isPageOpen = Application.Current.MainPage.Navigation.ModalStack.Any(p => p is IkazBilgisiGonder);

            if (!isPageOpen)
            {
                // Týklama olayý gerçekleþtiðinde yeni sayfayý modal olarak açýn
                await Application.Current.MainPage.Navigation.PushModalAsync(new IkazBilgisiGonder(kullanici));
            }
            //var page = new IkazBilgisiGonder(kullanici);
            //await this.Navigation.PushModalAsync(page, false);
            await Task.Delay(1000);
            oku = false;
        });
    }
}