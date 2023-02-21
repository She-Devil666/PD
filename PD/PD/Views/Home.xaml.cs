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
    public partial class Home : TabbedPage
    {
        public User current_user;

        public Home(User user)
        {
            InitializeComponent();
            current_user = user;
            CurrentPage = Children[2];
            var titleView = new Label { Text = current_user.Login };
            NavigationPage.SetTitleView(this, titleView);
        }
    }
}