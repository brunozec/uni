using LojaDeJogos.ViewModels.Jogos;
using LojaDeJogos.ViewModels.Vendas;
using Xamarin.Forms;

namespace LojaDeJogos.Views.Vendas
{
    public partial class VendasListaPage : ContentPage
    {
        VendasListaViewModel _viewModel;

        public VendasListaPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new VendasListaViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}