using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookieHuntApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdminEmployeeAtStore : ContentPage
	{
		public AdminEmployeeAtStore ()
		{
			InitializeComponent ();
		}

        private void LvEmployee_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void LvStore_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}