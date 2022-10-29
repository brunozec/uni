using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using LojaDeJogos.Models;
using LojaDeJogos.Repositories;
using LojaDeJogos.Views.Vendas;
using Xamarin.Forms;

namespace LojaDeJogos.ViewModels.Vendas
{
    public class VendasListaViewModel : BaseViewModel
    {
        private readonly IVendaRepository _vendaRepository;
        private Venda _selectedVenda;

        public ObservableCollection<Venda> Vendas { get; }
        public Command LoadVendasCommand { get; }
        public Command AddVendaCommand { get; }
        public Command<Venda> VendaTapped { get; }

        public VendasListaViewModel()
        {
            //resolve a dependencia pela interface do repositorio
            _vendaRepository = DependencyService.Resolve<IVendaRepository>();
            Title = "Vendas";

            //Inicializa a lista de vendas
            Vendas = new ObservableCollection<Venda>();

            //Inicializa o commando para carregamento dos vendas
            LoadVendasCommand = new Command(async () => await ExecuteLoadVendasCommand());

            //Inicializa o commando para o tap o tem selecionado
            VendaTapped = new Command<Venda>(OnVendaSelected);

            //inicializa o comando para a ação de adicionar venda
            AddVendaCommand = new Command(OnAddVenda);
        }

        async Task ExecuteLoadVendasCommand()
        {
            IsBusy = true;

            try
            {
                Vendas.Clear();
                var items = await _vendaRepository.GetAllAsync();
                foreach (var item in items)
                {
                    Vendas.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedVenda = null;
        }

        public Venda SelectedVenda
        {
            get => _selectedVenda;
            set
            {
                SetProperty(ref _selectedVenda, value);
                OnVendaSelected(value);
            }
        }

        private async void OnAddVenda(object obj)
        {
            //navega até a pagina para adicionar um novo venda
            await Shell.Current.GoToAsync(nameof(VendaPage));
        }

        async void OnVendaSelected(Venda item)
        {
            if (item == null)
                return;

            //navega até a pagina de detalhes do venda
            await Shell.Current.GoToAsync($"{nameof(VendaDetailPage)}?{nameof(VendaDetailViewModel.VendaId)}={item.Id}");
        }
    }
}