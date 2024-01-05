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
        var page = new IkazBilgisiGonder("zerenaisenur@gmail.com");
        await this.Navigation.PushModalAsync(page, false);
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
            var page = new IkazBilgisiGonder(args.Result[0].Text);
            await this.Navigation.PushModalAsync(page, false);
            await Task.Delay(1000);
            oku = false;
        });
    }
}