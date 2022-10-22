using LojaDeJogos.Models;
using LojaDeJogos.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LojaDeJogos.Views
{
    public partial class JogoPage : ContentPage
    {
        private readonly JogoViewModel _viewModel;
        public Jogo Jogo { get; set; }

        public JogoPage()
        {
            InitializeComponent();
            BindingContext =
                _viewModel = new JogoViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}