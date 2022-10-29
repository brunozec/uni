using LojaDeJogos.Services;
using LojaDeJogos.Views;
using System;
using System.IO;
using System.Net.Http;
using LojaDeJogos.Repositories;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LojaDeJogos
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            var sql = DependencyService.Resolve<ISql>();

            Database.CreateDatabase(sql.GetConnectionAsync());

            DependencyService.Register<IJogoRepository, JogoRepository>();
            DependencyService.Register<HttpClient>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
