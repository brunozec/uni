using System;
using LojaDeJogos.Views.Jogos;
using Xamarin.Forms;
using LojaDeJogos.Views.Vendas;

namespace LojaDeJogos
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(JogoDetailPage), typeof(JogoDetailPage));
            Routing.RegisterRoute(nameof(JogoPage), typeof(JogoPage));
            Routing.RegisterRoute(nameof(VendaPage), typeof(VendaPage));
            Routing.RegisterRoute(nameof(VendaDetailPage), typeof(VendaDetailPage));
        }
    }
}
