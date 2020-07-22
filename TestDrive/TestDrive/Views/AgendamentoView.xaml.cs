using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using TestDrive.ViewModels;
using Xamarin.Forms;

namespace TestDrive.Views
{
    public partial class AgendamentoView : ContentPage
    {
        public AgendamentoViewModel ViewModel { get; set; }

        public AgendamentoView(Veiculo veiculo)
        {
            InitializeComponent();
            this.ViewModel = new AgendamentoViewModel(veiculo);
            this.BindingContext = this.ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Agendamento>(this, "Agendamento",
                async (msg) =>
                {
                    var confirma = await DisplayAlert("Salvar Agendamento", "Deseja mesmo enviar o agendamento ?", "Sim", "Não");

                    if (confirma)
                    {
                        ViewModel.SalvarAgendamento();
                    }
                });

            MessagingCenter.Subscribe<Agendamento>(this, "Sucesso Agendamento", (msg) =>
            {
                DisplayAlert("Agendamento", "Agendamento Salvo Com Sucesso", "Ok");
            });
            
            MessagingCenter.Subscribe<ArgumentException>(this, "Falha Agendamento", (msg) =>
            {
                DisplayAlert("Agendamento", "Falha ao Agendar o Test Drive, tente mais tarde!", "Ok");
            });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Agendamento>(this, "Agendamento");
            MessagingCenter.Unsubscribe<Agendamento>(this, "Sucesso Agendamento");
            MessagingCenter.Unsubscribe<ArgumentException>(this, "Falha Agendamento");
        }
    }
}
