using CookieHuntApp.Models;
using CookieHuntApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookieHuntApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignInPage : ContentPage
	{
        public ObservableCollection<UserHasRole> UserHasRoles;

        public SignInPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void BtnLoginAdmin_OnClicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.LoginUser(EntEmail.Text, EntPassword.Text);
            if (!response)
            {
                await DisplayAlert("Alert", "Something wrong...", "Cancel");
            }
            else
            {

                var resultRoles = await apiServices.GetRolesFromEmail(EntEmail.Text);

                foreach (var item in resultRoles)
                {
                    if (item.Role.Equals("admin") == true)
                    {
                        Navigation.InsertPageBefore(new AdminMenuPage(), this);
                        await Navigation.PopAsync();
                        break;
                    }
                }
               // await DisplayAlert("Alert", "You are not registered as an admin", "Cancel");
            }
        }

        private void TapSignUp_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }

        private void TapSignUp_Tapped(object sender, EventArgs e)
        {

        }

        private async void BtnLoginEmployee_Clicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.LoginUser(EntEmail.Text, EntPassword.Text);
            if (!response)
            {
                await DisplayAlert("Alert", "Something wrong...", "Cancel");
            }
            else
            {

                    var resultRoles = await apiServices.GetRolesFromEmail(EntEmail.Text);

                    foreach (var role in resultRoles)
                    {
                        if(role.Role.Equals("employee")==true)
                        {
                        Navigation.InsertPageBefore(new EmployeeMenuPage(role), this);
                        await Navigation.PopAsync();
                        break;
                    }
            }
               // await DisplayAlert("Alert", "You are not registered as an employee", "Cancel");


            }


        }
        

        private async void BtnLoginCustomer_Clicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.LoginUser(EntEmail.Text, EntPassword.Text);
            if (!response)
            {
                await DisplayAlert("Alert", "Something wrong...", "Cancel");
            }
            else
            {
                Navigation.InsertPageBefore(new CustomerMenuPage(), this);
                await Navigation.PopAsync();

            }
        }
    }
}