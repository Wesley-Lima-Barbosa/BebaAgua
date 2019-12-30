using BebaAgua.Images.Models;
using BebaAgua.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BebaAgua
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoricoPage : ContentPage
	{
		public HistoricoPage ()
		{
			InitializeComponent (); 
		}
       protected override async void OnAppearing()
        {
          
            List<Historico> list = await App.Banco().historico.Listar();
            
            listView.ItemsSource = list.OrderByDescending(x => x.Horario).ToList(); 
            
            
        }
        async void btnSelecionar_Clicked(object sender, EventArgs e)
        {
            if (btnSelecionar.Text == "Selecionar")
            {
                listView.IsVisible = false;
                listView2.IsVisible = true;
                btnSelecionar.Text = "Cancelar";
                listView2.ItemsSource = await App.Banco().historico.Listar();
                ExcluirStackLayout.IsVisible = true;

            }
            else
            {
                listView2.IsVisible = false;
                ExcluirStackLayout.IsVisible = false;
                btnSelecionar.Text = "Selecionar";
                listView.ItemsSource = await App.Banco().historico.Listar();
                listView.IsVisible = true;


            }

        }
       async void btnExcluir_Clicked(object sender, EventArgs e)
        {
            List<Historico> historic = listView2.ItemsSource as List<Historico>;

            foreach(Historico item in historic.Where(x => x.SwitchEnable))
              await App.Banco().historico.Excluir(item);

            listView2.ItemsSource = await App.Banco().historico.Listar();




        }



    }

}