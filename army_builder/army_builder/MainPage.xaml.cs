using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace army_builder
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void CreateRedirect(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Battle-Forged?", "Cancel", null, "Yes", "No");
            bool battleForged = "Yes".Equals(action);

            if (battleForged) {
                await Navigation.PushAsync(new FactionSelectionPage());
            } else {

            }
            
        }
    }
}
