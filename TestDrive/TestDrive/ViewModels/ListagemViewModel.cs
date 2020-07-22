using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class ListagemViewModel : BaseViewModel
    {
        const string UrlVeiculos = "https://aluracar.herokuapp.com/";
        public ObservableCollection<Veiculo> Veiculos { get; set; }

        private bool aguarde;

        public bool Aguarde
        {
            get { return aguarde; }
            set
            {
                aguarde = value;
                OnPropertyChanged("");
            }
        }


        Veiculo veiculoSelecionado;
        public Veiculo VeiculoSelecionado
        {
            get
            {
                return veiculoSelecionado;
            }
            set
            {
                veiculoSelecionado = value;
                if (value != null)
                    MessagingCenter.Send(veiculoSelecionado, "VeiculoSelecionado");
            }
        }

        public ListagemViewModel()
        {
            Veiculos = new ObservableCollection<Veiculo>();
        }

        public async Task GetVeiculos()
        {
            Aguarde = true;
            var httpCliente = new HttpClient();
            var response = await httpCliente.GetStringAsync(new Uri(UrlVeiculos));
            var veiculoJson = JsonConvert.DeserializeObject<VeiculoJson[]>(response);
            veiculoJson.ToList().ForEach(v=> Veiculos.Add(new Veiculo
            {
                Nome = v.nome,
                Preco = v.preco
            }));
            Aguarde = false;
        }
    }

    class VeiculoJson
    {
        public string nome { get; set; }
        public int preco { get; set; }
    }
}
