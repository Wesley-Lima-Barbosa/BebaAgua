using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts.Forms;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using Entry = Microcharts.Entry;

namespace BebaAgua
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        DateTime Data = DateTime.Now;


        public MainPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {


            base.OnAppearing();
            Chart1.Chart = new DonutChart() { Entries = Atualizar(0) };
           Images.Models.Usuario user = App.Banco().usuario.Consultar(1).Result;
            if (user.PrefLiquido == 1)
            {
                CopoImage.Source = "Copoml.png";
                CanecaImage.Source = "Canecaml.png";
                GarrafaImage.Source = "Garrafaml.png";
                XicaraImage.Source = "Xicaraml.png";
            }
            else
            {
                CopoImage.Source = "Copooz.png";
                CanecaImage.Source = "Canecaoz.png";
                GarrafaImage.Source = "Garrafaoz.png";
                XicaraImage.Source = "Xicaraoz.png";
            }



        }

        async void CopoTap(object sender, EventArgs e)
        {
            CopoImage.IsEnabled = false;
            var imagesender = (Image)sender;
            await CopoImage.ScaleTo(1.5, 50);
            Images.Models.Usuario user = App.Banco().usuario.Consultar(1).Result;
            if (user.PrefLiquido == 1)
                Atualizar(200);
            else
                Atualizar(6.5 * 30);
            await CopoImage.ScaleTo(1, 50);
            CopoImage.IsEnabled = true;
        }

        async void GarrafaTap(object sender, EventArgs e)
        {
            GarrafaImage.IsEnabled = false;
            var imagesender = (Image)sender;
            await GarrafaImage.ScaleTo(1.5, 50);
            Images.Models.Usuario user = App.Banco().usuario.Consultar(1).Result;
            if (user.PrefLiquido == 1)
                Atualizar(510);
            else
                Atualizar(17 * 30);
            await GarrafaImage.ScaleTo(1, 50);
            GarrafaImage.IsEnabled = true;
        }

        async void CanecaTap(object sender, EventArgs e)
        {
            CanecaImage.IsEnabled = false;
            var imagesender = (Image)sender;
            await CanecaImage.ScaleTo(1.5, 50);
            Images.Models.Usuario user = App.Banco().usuario.Consultar(1).Result;
            if (user.PrefLiquido == 1)
                Atualizar(350);
            else
                Atualizar(12 * 30);
            await CanecaImage.ScaleTo(1, 50);
            CanecaImage.IsEnabled = true;
        }


        async void XicaraTap(object sender, EventArgs e)
        {
            XicaraImage.IsEnabled = false;
            var imagesender = (Image)sender;
            await XicaraImage.ScaleTo(1.5, 50);
            Images.Models.Usuario user = App.Banco().usuario.Consultar(1).Result;
            if (user.PrefLiquido == 1)
                Atualizar(240);
            else
                Atualizar(8 * 30);
            await XicaraImage.ScaleTo(1, 50);
            XicaraImage.IsEnabled = true;
        }
        async void JarraTap(object sender, EventArgs e)
        {
            JarraImage.IsEnabled = false;
            var imagesender = (Image)sender;
            await JarraImage.ScaleTo(1.5, 50);

            double valor = Convert.ToDouble(EntryQuant.Text);
            Images.Models.Usuario user = App.Banco().usuario.Consultar(1).Result;
            if (user.PrefLiquido == 1)
                Atualizar(valor);
            else
                Atualizar(valor * 30);
            await JarraImage.ScaleTo(1, 50);
            JarraImage.IsEnabled = true;
        }


        public List<Entry> Atualizar(double quant)
        {
            Images.Models.Historico model = new Images.Models.Historico();
            Images.Models.Usuario user = App.Banco().usuario.Consultar(1).Result;
            if (quant > 0)
            {
                model.Horario = Data;
                model.Quantidade = quant;
                App.Banco().historico.Salvar(model);
            }

            List<Images.Models.Historico> AguaBebida = App.Banco().historico.Listar().Result.Where(x => x.Horario.Date == DateTime.Now.Date).ToList();
            int valor = Convert.ToInt32(AguaBebida.Sum(x => x.Quantidade));
            double meta;

            if (user.PrefLiquido == 2)
            {
                meta = (user.Meta - valor) / 30;
                valor = valor / 30;
            }
            else
                meta = user.Meta - valor;

            string labelentry;
            string metaformat;



            if (meta <= 0)
            {
                if (user.PrefLiquido == 2)
                    metaformat = "+" + (meta * -1).ToString("0.00");
                else
                    metaformat = "+" + (meta * -1).ToString("0");
               
                labelentry = "Meta concluída!";
                meta = 0;


            }
            else
            {
                if (user.PrefLiquido == 2)
                    metaformat = meta.ToString("0.00");
                else
                    metaformat = meta.ToString("0");
                
                labelentry = "Restante da Meta";

            }


            List<Entry> entries = new List<Entry>
        {
            new Entry(valor)
            {
                Color=SKColor.Parse("#FF1943")

            },
            new Entry(Convert.ToInt32(meta))
            {
                Color =  SKColor.Parse("#00CED1")

            },
            };
            lblmeta.Text = labelentry;
            LabelMeta.Text = metaformat;
            if (user.PrefLiquido == 2)
                LabelBebido.Text = valor.ToString("0.00");
            else
                LabelBebido.Text = valor.ToString("0");
            Chart1.Chart = new DonutChart() { Entries = entries };
            Chart1.Chart.LabelTextSize = 30;
            Chart1.Chart = new DonutChart() { Entries = entries };



            return entries;
        }


    }
}