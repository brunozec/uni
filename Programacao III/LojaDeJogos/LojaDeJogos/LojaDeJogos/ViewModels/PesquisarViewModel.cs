using LojaDeJogos.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Input;
using LojaDeJogos.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LojaDeJogos.ViewModels
{
    public class PesquisarViewModel : BaseViewModel
    {
        private string _textoPesquisa;
        private string _url;
        private string _name;
        private int? _yearPublished;
        private string _imageUrl;

        public string TextoPesquisa
        {
            get => _textoPesquisa;
            set => SetProperty(ref _textoPesquisa, value);
        }

        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public int? YearPublished
        {
            get => _yearPublished;
            set => SetProperty(ref _yearPublished, value);
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        public PesquisarViewModel()
        {
            Title = "Pesquisar jogos";
            //resolve a dependencia pela interface do repositorio
            var httpClient = DependencyService.Resolve<HttpClient>();

            PesquisarCommand = new Command(async () =>
            {
                //https://learn.microsoft.com/en-us/xamarin/essentials/geolocation?tabs=android
                try
                {
                    //faz a chamada na api para carrega uma lista de jogos com o nome pesquisado
                    var retorno = await httpClient.GetAsync($"https://api.boardgameatlas.com/api/search?name={TextoPesquisa}&client_id=JLBr5npPhV");

                    if (retorno.StatusCode == HttpStatusCode.OK)
                    {
                        var games = JsonConvert.DeserializeObject<GamesAPI>(await retorno.Content.ReadAsStringAsync());

                        if (games.Games.Any())
                        {
                            var primeiro = games.Games.First();
                            Name = primeiro.Name;
                            YearPublished = primeiro.YearPublished;
                            Url = primeiro.Url;
                            ImageUrl = primeiro.ImageUrl;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //ocorreu uma falha
                }
            });
        }

        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public ICommand PesquisarCommand { get; }
    }
}