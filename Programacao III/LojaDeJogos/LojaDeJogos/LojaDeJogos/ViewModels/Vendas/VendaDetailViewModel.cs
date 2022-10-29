using System;
using System.Collections.Generic;
using System.Diagnostics;
using LojaDeJogos.Models;
using LojaDeJogos.Repositories;
using LojaDeJogos.Views;
using LojaDeJogos.Views.Vendas;
using Xamarin.Forms;

namespace LojaDeJogos.ViewModels.Vendas
{
    [QueryProperty(nameof(VendaId), nameof(VendaId))]
    public class VendaDetailViewModel : BaseViewModel
    {
        public Command EditVendaCommand { get; }

        private readonly IVendaRepository _vendaRepository;
        private string _clienteCpfCnpj;
        private IEnumerable<VendaItem> _itens;
        private string _nomeCliente;
        private int _vendaId;
        private int? _id;

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
        
        public int VendaId
        {
            get => _vendaId;
            set
            {
                _vendaId = value;
                LoadVendaId(value);
            }
        }

        public VendaDetailViewModel()
        {
            //resolve a dependencia pela interface do repositorio
            _vendaRepository = DependencyService.Resolve<IVendaRepository>();

            //inicializa o comando para a ação de editar venda
            EditVendaCommand = new Command(OnEditVenda);
        }

        private async void OnEditVenda(object item)
        {
            if (Id == null)
                return;

            //navega até a pagina de detalhes do venda
            await Shell.Current.GoToAsync($"{nameof(VendaPage)}?{nameof(VendaViewModel.VendaId)}={Id}");
        }

        public async void LoadVendaId(int vendaId)
        {
            try
            {
                //carrega o item pelo código identificador passado por parametro
                var venda = await _vendaRepository.GetByIdAsync(vendaId);
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
    }
}