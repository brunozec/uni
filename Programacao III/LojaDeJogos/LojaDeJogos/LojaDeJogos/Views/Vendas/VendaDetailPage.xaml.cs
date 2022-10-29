using LojaDeJogos.ViewModels.Vendas;
using Xamarin.Forms;

namespace LojaDeJogos.Views.Vendas
{
    public partial class VendaDetailPage : ContentPage
    {
        public VendaDetailPage()
        {
            InitializeComponent();
            BindingContext = new VendaDetailViewModel();
        }
    }
}