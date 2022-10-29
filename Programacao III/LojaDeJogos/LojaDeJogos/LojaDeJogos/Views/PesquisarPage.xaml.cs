using LojaDeJogos.ViewModels;
using Xamarin.Forms;

namespace LojaDeJogos.Views
{
    public partial class PesquisarPage : ContentPage
    {
        private readonly PesquisarViewModel _viewModel;

        public PesquisarPage()
        {
            InitializeComponent();
            BindingContext =
                _viewModel = new PesquisarViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}