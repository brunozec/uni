using LojaDeJogos.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using LojaDeJogos.Repositories;
using LojaDeJogos.Views.Jogos;
using Xamarin.Forms;

namespace LojaDeJogos.ViewModels.Jogos
{
    public class JogosListaViewModel : BaseViewModel
    {
        private readonly IJogoRepository _jogoRepository;
        private Jogo _selectedJogo;

        public ObservableCollection<Jogo> Jogos { get; }
        public Command LoadJogosCommand { get; }
        public Command AddJogoCommand { get; }
        public Command<Jogo> JogoTapped { get; }

        public JogosListaViewModel()
        {
            //resolve a dependencia pela interface do repositorio
            _jogoRepository = DependencyService.Resolve<IJogoRepository>();
            Title = "Jogos";

            //Inicializa a lista de jogos
            Jogos = new ObservableCollection<Jogo>();

            //Inicializa o commando para carregamento dos jogos
            LoadJogosCommand = new Command(async () => await ExecuteLoadJogosCommand());

            //Inicializa o commando para o tap o tem selecionado
            JogoTapped = new Command<Jogo>(OnJogoSelected);

            //inicializa o comando para a ação de adicionar jogo
            AddJogoCommand = new Command(OnAddJogo);
        }

        async Task ExecuteLoadJogosCommand()
        {
            IsBusy = true;

            try
            {
                Jogos.Clear();
                var items = await _jogoRepository.GetAllAsync();
                foreach (var item in items)
                {
                    Jogos.Add(item);
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
            SelectedJogo = null;
        }

        public Jogo SelectedJogo
        {
            get => _selectedJogo;
            set
            {
                SetProperty(ref _selectedJogo, value);
                OnJogoSelected(value);
            }
        }

        private async void OnAddJogo(object obj)
        {
            //navega até a pagina para adicionar um novo jogo
            await Shell.Current.GoToAsync(nameof(JogoPage));
        }

        async void OnJogoSelected(Jogo item)
        {
            if (item == null)
                return;

            //navega até a pagina de detalhes do jogo
            await Shell.Current.GoToAsync($"{nameof(JogoDetailPage)}?{nameof(JogoDetailViewModel.JogoId)}={item.Id}");
        }
    }
}