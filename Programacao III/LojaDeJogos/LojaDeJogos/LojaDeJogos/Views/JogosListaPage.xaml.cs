using LojaDeJogos.Models;
using LojaDeJogos.ViewModels;
using LojaDeJogos.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LojaDeJogos.Views
{
    public partial class JogosListaPage : ContentPage
    {
        JogosListaViewModel _viewModel;

        public JogosListaPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new JogosListaViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}