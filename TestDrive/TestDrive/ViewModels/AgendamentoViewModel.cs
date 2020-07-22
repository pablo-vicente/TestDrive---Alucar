using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class AgendamentoViewModel : BaseViewModel
    {
        const string URL_POST_AGENDAMENTO = "https://aluracar.herokuapp.com/salvaragendamento";
        public Agendamento Agendamento { get; set; }

        public Veiculo Veiculo
        {
            get => Agendamento.Veiculo;
            set => Agendamento.Veiculo = value;
        }

        public string Nome
        {
            get => Agendamento.Nome;

            set
            {
                Agendamento.Nome = value;
                OnPropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }
        }

        public string Fone
        {
            get => Agendamento.Fone;

            set
            {
                Agendamento.Fone = value;
                OnPropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }
        }

        public void SalvarAgendamento()
        {
            using (var httpCliente = new HttpClient())
            {
                var dataHoraAgendamento = new DateTime(DataAgendamento.Year, DataAgendamento.Month, DataAgendamento.Day, HoraAgendamento.Hours, HoraAgendamento.Minutes, HoraAgendamento.Seconds);

                var json = JsonConvert.SerializeObject(new
                {
                    nome = Nome,
                    fone = Fone,
                    email = Email,
                    carro = Veiculo.Nome,
                    preco = Veiculo.Preco,
                    dataAgendamento = dataHoraAgendamento
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = httpCliente.PostAsync(URL_POST_AGENDAMENTO, content).Result;
                if (response.IsSuccessStatusCode)
                    MessagingCenter.Send<Agendamento>(this.Agendamento, "Sucesso Agendamento");
                else
                    MessagingCenter.Send<ArgumentException>(new ArgumentException(), "Falha Agendamento");
            }
        }

        public string Email
        {
            get => Agendamento.Email;

            set
            {
                Agendamento.Email = value;
                OnPropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }
        }

        public DateTime DataAgendamento
        {
            get => Agendamento.DataAgendamento;
            set => Agendamento.DataAgendamento = value;
        }

        public TimeSpan HoraAgendamento
        {
            get => Agendamento.HoraAgendamento;
            set => Agendamento.HoraAgendamento = value;
        }


        public AgendamentoViewModel(Veiculo veiculo)
        {
            this.Agendamento = new Agendamento();
            this.Agendamento.Veiculo = veiculo;

            AgendarCommand = new Command(() =>
            {
                MessagingCenter.Send(Agendamento, "Agendamento");
            }, () => !string.IsNullOrEmpty(Nome) && !string.IsNullOrEmpty(Fone) && !string.IsNullOrEmpty(Email));
        }

        public ICommand AgendarCommand { get; set; }
    }
}
