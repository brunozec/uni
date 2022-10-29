using LojaDeJogos.ViewModels.Jogos;
using Xamarin.Forms;

namespace LojaDeJogos.Views.Jogos
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