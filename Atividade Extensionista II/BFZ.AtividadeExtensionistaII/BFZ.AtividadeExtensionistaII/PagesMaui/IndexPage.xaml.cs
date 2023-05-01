using BFZ.AtividadeExtensionistaII.Viewmodels.Implementations.Auth;
using Microsoft.AspNetCore.Components;
using Syncfusion.Maui.TabView;

namespace BFZ.AtividadeExtensionistaII.PagesMaui;

public partial class IndexPage : ContentPage
{
    [Inject] public AuthenticationViewModel AuthenticationViewModel { get; set; }
    
    public IndexPage()
    {
        InitializeComponent();
        
    }
}