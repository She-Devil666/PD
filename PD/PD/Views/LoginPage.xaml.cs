using PD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private CustomersDataAccess dataAccess = new CustomersDataAccess();
        public Home home;
        public AccountPage account = new AccountPage();

        public LoginPage()
        {
            InitializeComponent();
        }

        public LoginPage(string Login, string Password)
        {
            InitializeComponent();
            login.Text = Login;
            pass.Text = Password;
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            if (login.Text == null && pass.Text == null)
                await DisplayAlert("Ошибка!", "Заполните поля логина и пароля!", "Ок");
            else if (login.Text == null)
                await DisplayAlert("Ошибка!", "Заполните поле логина!", "Ок");
            else if (pass.Text == null)
                await DisplayAlert("Ошибка!", "Заполните поле пароля!", "Ок");
            else
            {
                User user = this.dataAccess.GetUser(login.Text);
                if (user == null)
                    await DisplayAlert("Ошибка!", "Пользователя с таким логином не существует!", "Ок");
                else if (user.Password == pass.Text)
                {
                    await DisplayAlert("Успешно!", "Вы успешно авторизовались!", "Ок");
                    await Navigation.PushModalAsync(new AccountPage(user));
                    account.currentUser = user;
                }
                else
                    await DisplayAlert("Ошибка!", "Проверьте введенные логин и пароль", "Ок");
            }
        }

        private async void reg_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterPage());
        }
    }
}