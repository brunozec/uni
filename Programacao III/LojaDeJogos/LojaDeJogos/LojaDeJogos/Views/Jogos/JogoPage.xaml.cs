using LojaDeJogos.Models;
using LojaDeJogos.ViewModels.Jogos;
using Xamarin.Forms;

namespace LojaDeJogos.Views.Jogos
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