using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _usuario;

        public string Usuario
        {
            get => _usuario;
            set
            {
                _usuario = value;
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        private string _senha;

        public string Senha
        {
            get => _senha;
            set
            {
                _senha = value;
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        public ICommand EntrarCommand { get; private set; }

        public LoginViewModel()
        {
            EntrarCommand = new Command(() =>
            {
                MessagingCenter.Send(new Usuario(), "Sucesso Login");
            }, () => !string.IsNullOrEmpty(_usuario) && !string.IsNullOrEmpty(_senha));
        }

    }
}
