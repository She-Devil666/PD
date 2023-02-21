using PD.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private CustomersDataAccess dataAccess = new CustomersDataAccess();
        byte[] image;

        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void reg_Clicked(object sender, EventArgs e)
        {
            if (login.Text == null || pass.Text == null || pass_retry.Text == null || name.Text == null || phone.Text.Length != 11 || city.Text == null)
                await DisplayAlert("Ошибка!", "Заполните все пустые поля!", "Ок");
            else if (image == null)
                await DisplayAlert("Ошибка!", "Выберите фото!", "Ок");
            else if (pass.Text.Length < 8)
                await DisplayAlert("Ошибка!", "Пароль должен состоять минимум из 8 символов!", "Ок");
            else if (pass.Text != pass_retry.Text)
                await DisplayAlert("Ошибка!", "Пароли не совпадают!", "Ок");
            else
            {
                User check = this.dataAccess.GetUser(login.Text);
                if (check == null)
                {
                    check = this.dataAccess.GetPhone(phone.Text);
                    if (check == null)
                    {
                        User user = new User
                        {
                            Login = login.Text,
                            Password = pass.Text,
                            Name = name.Text,
                            City = city.Text,
                            Phone = phone.Text,
                            Description = description.Text,
                            Image= image
                        };
                        this.dataAccess.SaveUser(user);
                        await DisplayAlert("Успешно!", "Вы успешно зарегистрировались!", "Ок");
                        await Navigation.PushModalAsync(new AccountPage(user));
                    }
                    else
                        await DisplayAlert("Ошибка!", "Пользователь с такой почтой уже существует!", "Ок");
                }
                else
                    await DisplayAlert("Ошибка!", "Пользователь с таким логином уже существует!", "Ок");
            }
        }

        private async void log_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }

        private async void photo_Clicked(object sender, EventArgs e)
        {
            try
            {
                Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
                if (stream != null)
                {
                    image = GetImageBytes(stream);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        private byte[] GetImageBytes(Stream stream)
        {
            byte[] ImageBytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                stream.CopyTo(memoryStream);
                ImageBytes = memoryStream.ToArray();
            }
            return ImageBytes;
        }
    }
}