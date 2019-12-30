using BebaAgua.Images.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BebaAgua.Repository;

namespace BebaAgua
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificacaoPage : ContentPage
    {
        public NotificacaoPage()
        {
            InitializeComponent();
        }

        Usuario usuario = App.Banco().usuario.Consultar(1).Result;

        protected override async void OnAppearing()
        {
            usuario = App.Banco().usuario.Consultar(1).Result;
            switch_notificar.IsToggled = usuario.Notificacao;
            switch_led.IsToggled = usuario.Led;
            switch_vibrar.IsToggled = usuario.Vibracao;
            slider_lembrete.Value = usuario.IntervaloNotificacao;

        }

        protected async override void OnDisappearing()
        {
            usuario.Notificacao = switch_notificar.IsToggled;
            usuario.Led = switch_led.IsToggled;
            usuario.Vibracao = switch_vibrar.IsToggled;
            usuario.IntervaloNotificacao = slider_lembrete.Value;

            await App.Banco().usuario.Salvar(usuario);
        }

        async void notificar(object sender, EventArgs e)
        {
            if (switch_notificar.IsToggled == false)
            {
                slider_lembrete.IsEnabled = false;
                valor_minutos.IsEnabled = false;
                switch_vibrar.IsEnabled = false;
                switch_led.IsEnabled = false;
            }
            else
            {
                slider_lembrete.IsEnabled = true;
                valor_minutos.IsEnabled = true;
                switch_vibrar.IsEnabled = true;
                switch_led.IsEnabled = true;
            }

        }



        async void altera_valor(object sender, EventArgs e)
        {

            int valor = Convert.ToInt32(slider_lembrete.Value);
            int valor2;
            if (valor == 0)
                valor_minutos.Text = "";


            if (valor > 59)
            {
                valor2 = valor / 60;
                valor = valor - (valor2 * 60);
                string valortext = "";
                string valor2text = "";

                if (valor > 1)
                    valortext = " minutos";
                else
                    valortext = " minuto";

                if (valor2 > 1)
                    valor2text = " horas e ";
                else
                    valor2text = " hora e ";
                valor_minutos.Text = valor2 + valor2text + valor + valortext;
            }
            else
            {
                if (valor > 1)
                    valor_minutos.Text = valor + " minutos";
                else
                    valor_minutos.Text = valor + " minuto";
            }


        }
        async void btnNaoPerturbe_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NaoNotifiquePage());


        }
    }
}

       