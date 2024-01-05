using System.Net.Mail;
using System.Net;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ParkIkaz;

public partial class IkazBilgisiGonder : ContentPage
{
    private string AliciMailAdresi {  get; set; }

    public IkazBilgisiGonder(string AliciMailAdresi)
	{
        this.AliciMailAdresi = AliciMailAdresi;

        InitializeComponent();
    }
    private async void OnSendButtonClicked(object sender, EventArgs e)
    {
        if (IsBusy) return; IsBusy = true;
        sendButton.IsEnabled = false;
        pageLoader.IsVisible = true;
        await Task.Delay(1000);
        try
        {
            MailMessage mail = new MailMessage();

            mail.To.Add(this.AliciMailAdresi);
            mail.From = new MailAddress("parkikazuygulamasi@gmail.com");
            mail.Subject = subjectEntry.Text;
            string Body = messageEditor.Text;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Credentials = new System.Net.NetworkCredential("parkikazuygulamasi@gmail.com", "rcey musw ewuo rbmz");
            // smtp.Port = 587;
            //Or your Smtp Email ID and Password
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            // smtp.EnableSsl = true;
            smtp.Send(mail);
            await DisplayAlert("Hata", $"Ýkaz bilgisi gönderildi.", "Tamam");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Hata oluþtu: {ex.Message}", "Tamam");
        }
        pageLoader.IsVisible = false;
        sendButton.IsEnabled = true;
        IsBusy = false;
    }
}