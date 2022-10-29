using LojaDeJogos.Repositories;
using System;
using System.Diagnostics;
using LojaDeJogos.Views.Jogos;
using Xamarin.Forms;

namespace LojaDeJogos.ViewModels.Jogos
{
    [QueryProperty(nameof(JogoId), nameof(JogoId))]
    public class JogoDetailViewModel : BaseViewModel
    {
        public Command EditJogoCommand { get; }

        private readonly IJogoRepository _jogoRepository;
        private string _descricao;
        private decimal _valor;
        private int _estoque;
        private string _genero;
        private string _classificacao;
        private string _tags;
        private int? _id;
        private int _jogoId;

        public int? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Descricao
        {
            get => _descricao;
            set => SetProperty(ref _descricao, value);
        }

        public decimal Valor
        {
            get => _valor;
            set => SetProperty(ref _valor, value);
        }

        public int Estoque
        {
            get => _estoque;
            set => SetProperty(ref _estoque, value);
        }

        public string Genero
        {
            get => _genero;
            set => SetProperty(ref _genero, value);
        }

        public string Classificacao
        {
            get => _classificacao;
            set => SetProperty(ref _classificacao, value);
        }

        public string Tags
        {
            get => _tags;
            set => SetProperty(ref _tags, value);
        }

        public int JogoId
        {
            get => _jogoId;
            set
            {
                _jogoId = value;
                LoadJogoId(value);
            }
        }

        public JogoDetailViewModel()
        {
            //resolve a dependencia pela interface do repositorio
            _jogoRepository = DependencyService.Resolve<IJogoRepository>();

            //inicializa o comando para a ação de editar jogo
            EditJogoCommand = new Command(OnEditJogo);
        }

        private async void OnEditJogo(object item)
        {
            if (Id == null)
                return;

            //navega até a pagina de detalhes do jogo
            await Shell.Current.GoToAsync($"{nameof(JogoPage)}?{nameof(JogoViewModel.JogoId)}={Id}");
        }

        public async void LoadJogoId(int jogoId)
        {
            try
            {
                //carrega o item pelo código identificador passado por parametro
                var jogo = await _jogoRepository.GetByIdAsync(jogoId);
                //popula a propriedades da viewmodel, com os valores do item carregado
                Id = jogo.Id;
                Descricao = jogo.Descricao;
                Classificacao = jogo.Classificacao;
                Estoque = jogo.Estoque;
                Valor = jogo.Valor;
                Genero = jogo.Genero;
                Tags = jogo.Tags;
            }
            catch (Exception)
            {
                Debug.WriteLine("Falha ao carrega o item");
            }
        }
    }
}
