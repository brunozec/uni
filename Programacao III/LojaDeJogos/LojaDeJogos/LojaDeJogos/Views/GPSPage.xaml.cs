using LojaDeJogos.ViewModels;
using Xamarin.Forms;

namespace LojaDeJogos.Views
{
    public partial class GPSPage : ContentPage
    {
        private readonly GPSViewModel _viewModel;

        public GPSPage()
        {
            InitializeComponent();
            BindingContext =
                _viewModel = new GPSViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}