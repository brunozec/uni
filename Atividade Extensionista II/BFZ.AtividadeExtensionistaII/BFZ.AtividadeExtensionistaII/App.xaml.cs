using BFZ.AtividadeExtensionistaII.Repositories;

namespace BFZ.AtividadeExtensionistaII;

public partial class App : Application
{
	public App(RepositoryBase repositoryBase)
	{
		InitializeComponent();

		MainPage = new NavigationPage(new MainPage());
	}
}
