using LojaDeJogos.Models;
using LojaDeJogos.ViewModels.Vendas;
using Xamarin.Forms;

namespace LojaDeJogos.Views.Vendas
{
    public partial class VendaPage : ContentPage
    {
        private readonly VendaViewModel _viewModel;
        public Venda Venda { get; set; }

        public VendaPage()
        {
            InitializeComponent();
            BindingContext =
                _viewModel = new VendaViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}