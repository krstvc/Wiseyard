using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseyard.Helpers;

namespace Wiseyard.ViewModels
{
    public class LoginViewModel : Observable
    {
        private string _username;
        private string _password;
        private bool _buttonEnabled;

        public string Username
        {
            get => _username;
            set
            {
                Set(ref _username, value);
                if (String.IsNullOrEmpty(value))
                {
                    ButtonEnabled = false;
                }
                else
                {
                    ButtonEnabled = !String.IsNullOrEmpty(Password);
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                Set(ref _password, value);
                if (String.IsNullOrEmpty(value))
                {
                    ButtonEnabled = false;
                }
                else
                {
                    ButtonEnabled = !String.IsNullOrEmpty(Username);
                }
            }
        }

        public bool ButtonEnabled
        {
            get => _buttonEnabled;
            set { Set(ref _buttonEnabled, value); }
        }

        public LoginViewModel()
        {
        }

    }
}
