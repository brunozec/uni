using System;
using LojaDeJogos.Views;
using LojaDeJogos.Views.Jogos;
using Xamarin.Forms;

namespace LojaDeJogos
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(JogoDetailPage), typeof(JogoDetailPage));
            Routing.RegisterRoute(nameof(JogoPage), typeof(JogoPage));
            Routing.RegisterRoute(nameof(PesquisarPage), typeof(PesquisarPage));
            Routing.RegisterRoute(nameof(GPSPage), typeof(GPSPage));
        }
    }
}
