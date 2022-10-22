using LojaDeJogos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using LojaDeJogos.Repositories;
using Xamarin.Forms;
using System.Diagnostics;
using LojaDeJogos.Views;

namespace LojaDeJogos.ViewModels
{
    [QueryProperty(nameof(JogoId), nameof(JogoId))]
    public class JogoViewModel : BaseViewModel
    {
        private readonly IJogoRepository _jogoRepository;
        private int _jogoId;
        private int? _id;
        private string _descricao;
        private decimal _valor;
        private int _estoque;
        private string _genero;
        private string _classificacao;
        private string _tags;
        private bool _mostrarBotaoDeletar;

        public JogoViewModel()
        {
            _jogoRepository = DependencyService.Resolve<IJogoRepository>();

            SaveCommand = new Command(OnSave, ValidateSave);

            CancelCommand = new Command(OnCancel);

            DeleteCommand = new Command(OnDelete);

            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_descricao)
             && _valor > 0
             && !String.IsNullOrWhiteSpace(_genero)
             && !String.IsNullOrWhiteSpace(_classificacao)
             && !String.IsNullOrWhiteSpace(_tags);
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

        public bool MostrarBotaoDeletar
        {
            get => _mostrarBotaoDeletar;
            set => SetProperty(ref _mostrarBotaoDeletar, value);
        }

        public Command DeleteCommand { get; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            //remove a pagina atual da lista de navegação
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDelete()
        {
            //deleta o item
            await _jogoRepository.DeleteByIdAsync(JogoId);

            //navega ate a lista de jogos
            await Shell.Current.GoToAsync("//JogosListaPage");
        }

        private async void OnSave()
        {
            Jogo novoJogo = new Jogo()
            {
                Id = Id,
                Descricao = Descricao,
                Classificacao = Classificacao,
                Estoque = Estoque,
                Valor = Valor,
                Genero = Genero,
                Tags = Tags
            };

            await _jogoRepository.SaveAsync(novoJogo);

            //remove a pagina atual da lista de navegação
            await Shell.Current.GoToAsync("..");
        }

        public async void LoadJogoId(int jogoId)
        {
            try
            {
                //carrega o item pelo código identificador passado por parametro
                var jogo = await _jogoRepository.GetByIdAsync(jogoId);

                //mostra o botão de deletar
                MostrarBotaoDeletar = true;

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

        public void OnAppearing()
        {
            //caso esteja sendo editado um jogo, recarrega as possiveis alterações
            if (Id != null)
            {
                LoadJogoId((int)Id);
            }
        }
    }
}