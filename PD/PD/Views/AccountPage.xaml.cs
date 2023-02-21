using PD.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.App.Assist.AssistStructure;

namespace PD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        private CustomersDataAccess dataAccess = new CustomersDataAccess();
        public User currentUser;
        byte[] image;

        public AccountPage() 
        {
            InitializeComponent();
            try
            {
                this.BindingContext = dataAccess.GetUser(currentUser.Login);
                DisplayAlert(currentUser.Login, currentUser.City, currentUser.Name);
            }
            catch { }
        }

        public AccountPage(User user)
        {
            InitializeComponent();
            this.BindingContext = dataAccess.GetUser(user.Login);
            currentUser = user;
            var titleView = new Label { Text = currentUser.Login };
            NavigationPage.SetTitleView(this, titleView);
            var stream1 = new MemoryStream(currentUser.Image);
            photo.Source = ImageSource.FromStream(() => stream1);
        }

        private void edit_Clicked(object sender, EventArgs e)
        {
            name.IsReadOnly = false;
            login.IsReadOnly = false;
            phone.IsReadOnly = false;
            city.IsReadOnly = false;
            description.IsReadOnly = false;
            edit.IsVisible = false;
            save.IsVisible = true;
        }

        private async void save_Clicked(object sender, EventArgs e)
        {
            if (login.Text == null || name.Text == null || phone.Text.Length != 11 || city.Text == null)
                await DisplayAlert("Ошибка!", "Заполните все пустые поля!", "Ок");
            else
            {
                currentUser.City = city.Text;
                currentUser.Description = description.Text;
                currentUser.Name = name.Text;
                currentUser.Phone = phone.Text;
                currentUser.Login = login.Text;
                this.dataAccess.SaveUser(currentUser);
                name.IsReadOnly = true;
                login.IsReadOnly = true;
                phone.IsReadOnly = true;
                city.IsReadOnly = true;
                description.IsReadOnly = true;
                save.IsVisible = false;
                edit.IsVisible = true;
                await DisplayAlert("Успешно!", "Данные успешно сохранены!", "Ок");
            }
        }

        private async void logout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }

        private async void new_photo_Clicked(object sender, EventArgs e)
        {
            try
            {
                Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
                if (stream != null)
                {
                    image = GetImageBytes(stream);
                    currentUser.Image = image;
                    this.dataAccess.SaveUser(currentUser);
                    await DisplayAlert("Успешно!", "Фото успешно обновлено!", "Ок");
                    var stream1 = new MemoryStream(currentUser.Image);
                    photo.Source = ImageSource.FromStream(() => stream1);
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