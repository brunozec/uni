using LojaDeJogos.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace LojaDeJogos.Views
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