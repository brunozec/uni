using LojaDeJogos.ViewModels.Jogos;
using Xamarin.Forms;

namespace LojaDeJogos.Views.Jogos
{
    public partial class JogoDetailPage : ContentPage
    {
        public JogoDetailPage()
        {
            InitializeComponent();
            BindingContext = new JogoDetailViewModel();
        }
    }
}