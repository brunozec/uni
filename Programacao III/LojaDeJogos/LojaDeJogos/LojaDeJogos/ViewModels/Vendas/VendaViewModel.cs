using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LojaDeJogos.Models;
using LojaDeJogos.Repositories;
using Xamarin.Forms;

namespace LojaDeJogos.ViewModels.Vendas
{
    [QueryProperty(nameof(VendaId), nameof(VendaId))]
    public class VendaViewModel : BaseViewModel
    {
        private readonly IVendaRepository _vendaRepository;
        private int _vendaId;
        private int? _id;
        private string _clienteCpfCnpj;
        private string _nomeCliente;
        private IEnumerable<VendaItem> _itens;
        private bool _mostrarBotaoDeletar;

        public VendaViewModel()
        {
            _vendaRepository = DependencyService.Resolve<IVendaRepository>();

            SaveCommand = new Command(OnSave, ValidateSave);

            CancelCommand = new Command(OnCancel);

            DeleteCommand = new Command(OnDelete);

            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(_clienteCpfCnpj)
             && Itens.Count() > 0;
        }

        public int VendaId
        {
            get => _vendaId;
            set
            {
                _vendaId = value;
                LoadVendaId(value);
            }
        }

        public int? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string ClienteCpfCnpj
        {
            get => _clienteCpfCnpj;
            set => SetProperty(ref _clienteCpfCnpj, value);
        }

        public IEnumerable<VendaItem> Itens
        {
            get => _itens;
            set => SetProperty(ref _itens, value);
        }

        public string NomeCliente
        {
            get => _nomeCliente;
            set => SetProperty(ref _nomeCliente, value);
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
            await _vendaRepository.DeleteByIdAsync(VendaId);

            //navega ate a lista de vendas
            await Shell.Current.GoToAsync("//VendasListaPage");
        }

        private async void OnSave()
        {
            Venda novoVenda = new Venda()
            {
                Id = Id, ClienteCpfCnpj = ClienteCpfCnpj, NomeCliente = NomeCliente, Itens = Itens
            };

            await _vendaRepository.SaveAsync(novoVenda);

            //remove a pagina atual da lista de navegação
            await Shell.Current.GoToAsync("..");
        }

        public async void LoadVendaId(int vendaId)
        {
            try
            {
                //carrega o item pelo código identificador passado por parametro
                var venda = await _vendaRepository.GetByIdAsync(vendaId);

                //mostra o botão de deletar
                MostrarBotaoDeletar = true;

                //popula a propriedades da viewmodel, com os valores do item carregado
                Id = venda.Id;
                ClienteCpfCnpj = venda.ClienteCpfCnpj;
                NomeCliente = venda.NomeCliente;
                Itens = venda.Itens;
            }
            catch (Exception)
            {
                Debug.WriteLine("Falha ao carrega o item");
            }
        }

        public void OnAppearing()
        {
            //caso esteja sendo editado um venda, recarrega as possiveis alterações
            if (Id != null)
            {
                LoadVendaId((int)Id);
            }
        }
    }
}