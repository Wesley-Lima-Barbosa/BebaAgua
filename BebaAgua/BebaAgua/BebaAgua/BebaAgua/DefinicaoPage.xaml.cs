using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BebaAgua.Images.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BebaAgua
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DefinicaoPage : ContentPage
    {
        public DefinicaoPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            Usuario user = App.Banco().usuario.Consultar(1).Result;

            if (user.PrefLiquido == 1)
                PickerCopo.SelectedItem = "ml";
            else if (user.PrefLiquido == 2)
                PickerCopo.SelectedItem = "oz";

            if (user.PrefPeso == 1)
                PickerPeso.SelectedItem = "kg";
            else if (user.PrefPeso == 2)
                PickerPeso.SelectedItem = "lb";


            if (user.PrefPeso == 2)
                EntryPeso.Text = Convert.ToString(user.Peso * 2.205);
            else
                EntryPeso.Text = Convert.ToString(user.Peso);
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        protected async override void OnDisappearing()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            //EntryPeso_Unfocused(null, null);

            if ((EntryPeso.Text == null) || (EntryPeso.Text == ""))
            {
                Usuario user = App.Banco().usuario.Consultar(1).Result;
                EntryPeso.Text = Convert.ToString(user.Peso);
            }
            else
            {
                EntryPeso_Unfocused(null, null);

            }
        }
        async void btnRestaurar_Clicked(object sender, EventArgs e)
        {
            bool resultado = await DisplayAlert("Confirmação", "Realmente deseja restaurar o usuário (Não poderá ser recuperado)?", "Sim", "Não");

            if (resultado)

            {
				

                Usuario user = App.Banco().usuario.Consultar(1).Result;
                user = Classes.PadraoUsuario.usuario();
                user.ID = 1;
                await App.Banco().usuario.Salvar(user);
                await DisplayAlert("Concluído", "Restauração de Usuário completa!", "Ok");
            }
        }

        async void btnRestaurarHistorico_Clicked(object sender, EventArgs e)
        {
            bool resultado = await DisplayAlert("Confirmação", "Realmente deseja restaurar o Histórico? (Não poderá ser recuperado)", "Sim", "Não");
            if (resultado)
            {
                List<Historico> Historico = App.Banco().historico.Listar().Result;
                App.Banco().historico.ExcluirMultiplos();
                await DisplayAlert("Concluído", "Restauração de Histórico completa!", "Ok");
            }
        }

        async void PickerCopoChanged(object sender, EventArgs e)
        {
            Usuario user = App.Banco().usuario.Consultar(1).Result;
            string picker = Convert.ToString(PickerCopo.SelectedItem);
            if (picker == "ml")
            {
                user.PrefLiquido = 1;
            }
            else if (picker == "oz")
            {
                user.PrefLiquido = 2;
            }
            await App.Banco().usuario.Salvar(user);
        }
        async void PickerPesoChanged(object sender, EventArgs e)
        {
            Usuario user = App.Banco().usuario.Consultar(1).Result;
            string picker = Convert.ToString(PickerPeso.SelectedItem);
            if (picker == "kg")
            {
                user.PrefPeso = 1;
                if (user.Peso != Convert.ToDouble(EntryPeso.Text))
                    EntryPeso.Text = Convert.ToString(Convert.ToDouble(EntryPeso.Text) / 2.205);
            }
            else if (picker == "lb")
            {
                user.PrefPeso = 2;
                EntryPeso.Text = Convert.ToString(user.Peso * 2.205);
            }
            await App.Banco().usuario.Salvar(user);
        }
        async void EntryPeso_Unfocused(object sender, EventArgs e)
        {
            Usuario user = App.Banco().usuario.Consultar(1).Result;
            if (user.PrefPeso == 2)
                user.Peso = Convert.ToDouble(EntryPeso.Text) / 2.205;
            else if (user.PrefPeso == 1)
                user.Peso = Convert.ToDouble(EntryPeso.Text);
             await App.Banco().usuario.Salvar(user);

        }
    }
}