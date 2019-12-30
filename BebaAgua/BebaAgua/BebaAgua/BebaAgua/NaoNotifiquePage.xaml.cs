using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BebaAgua.Images.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;


namespace BebaAgua
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NaoNotifiquePage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public NaoNotifiquePage()
        {
            InitializeComponent();

        }
        protected override async void OnAppearing()
        {

            listView.ItemsSource = await App.Banco().alarme.Listar();
            listView2.IsVisible = false;

        }

        async void btnSelecionar_Clicked(object sender, EventArgs e)
        {
            if (btnSelecionar.Text == "Selecionar")
            {
                listView.IsVisible = false;
                listView2.IsVisible = true;
                btnSelecionar.Text = "Cancelar";
                listView2.ItemsSource = await App.Banco().alarme.Listar();
                btnExcluir.IsVisible = true;
                btnAdicionar.IsVisible = false;

            }
            else if (btnSelecionar.Text == "Cancelar")
            {
                listView2.IsVisible = false;
                btnExcluir.IsVisible = false;
                btnSelecionar.Text = "Selecionar";
                listView.ItemsSource = await App.Banco().alarme.Listar();
                listView.IsVisible = true;
                btnAdicionar.IsVisible = true;

            }
            else
            {
                listView.IsVisible = true;
                PickerAdd.IsVisible = false;
                lblPicker.IsVisible = false;
                lblPicker2.IsVisible = false;
                PickerAdd2.IsVisible = false;
                btnConcluir.IsVisible = false;
                btnAdicionar.IsVisible = true;
                btnSelecionar.Text = "Selecionar";
            }
        }
        void btnAdicionar_Clicked(object sender, EventArgs e)
        {
            listView.IsVisible = false;
            PickerAdd.IsVisible = true;
            lblPicker.IsVisible = true;
            lblPicker2.IsVisible = true;
            PickerAdd2.IsVisible = true;
            btnConcluir.IsVisible = true;
            btnAdicionar.IsVisible = false;
            btnSelecionar.Text = "Voltar";

        }
        async void btnExcluir_Clicked(object sender, EventArgs e)
        {
            bool resultado = await DisplayAlert("Excluir Horário", "Deseja excluir o horário Não Pertube?", "Sim", "Não");

            if (resultado)
            {
                List<Alarme> alarme = listView2.ItemsSource as List<Alarme>;

                foreach (Alarme item in alarme.Where(x => x.SwitchEnable))
                    await App.Banco().alarme.Excluir(item);

                listView2.ItemsSource = await App.Banco().alarme.Listar();
            }



            }

        async private void btnConcluir_Clicked(object sender, PropertyChangedEventArgs e)
        {
            bool resultado = await DisplayAlert("Adicionar Não Pertube", "Deseja adicionar o horário Não Pertube?", "Sim", "Não");

            if (resultado)
            {
                Alarme alarme = new Alarme();
                alarme.HorarioInicio = Convert.ToString((DateTime.Today + PickerAdd.Time).TimeOfDay);
                alarme.HorarioFim = Convert.ToString((DateTime.Today + PickerAdd2.Time).TimeOfDay);           
                await App.Banco().alarme.Salvar(alarme);
                listView.ItemsSource = await App.Banco().alarme.Listar();
                listView.IsVisible = true;
                PickerAdd.IsVisible = false;
                lblPicker.IsVisible = false;
                lblPicker2.IsVisible = false;
                PickerAdd2.IsVisible = false;
                btnConcluir.IsVisible = false;
                btnAdicionar.IsVisible = true;
                btnSelecionar.Text = "Selecionar";
                
            }


        }

    }
}
