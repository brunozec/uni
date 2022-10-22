using LojaDeJogos.ViewModels;
using LojaDeJogos.Views;
using System;
using System.Collections.Generic;
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
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
