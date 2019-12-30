using BebaAgua.Classes;
using BebaAgua.Images.Models;
using BebaAgua.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;

namespace BebaAgua
{
    public partial class App : Application
    {
        private static BancoDeDados banco;

        public static BancoDeDados Banco()
        {
            if (banco == null)
            {
                //diretorio local da aplicacao, o mesmo nao é compartilhado
                string diretorio = Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);

                //nome do banco de dados que será salvo no celular
                string nome = "BDBebaAgua.db3";

                //instancia o BD
                banco = new BancoDeDados(Path.Combine(diretorio, nome));
                if(Banco().usuario.Consultar(1).Result == null)
                Banco().usuario.Salvar(Classes.PadraoUsuario.usuario());


            }
            return banco;
        }
        public App()
        {
            InitializeComponent();

            MainPage = new MasterDetailPage1();
        }

        protected override void OnStart()
        {
            




        }

        Usuario usuario = App.Banco().usuario.Consultar(1).Result;

     

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
