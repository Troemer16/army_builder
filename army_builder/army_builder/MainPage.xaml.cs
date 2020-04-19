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
        private bool battleForged;
        private string armyCapType;
        private int armyCap;

        public MainPage()
        {
            InitializeComponent();
        }

        void Popup(object sender, EventArgs e)
        {
            popup.IsVisible = true;
            popupBlur.IsVisible = true;
        }

        void SetBattleForged(object sender, EventArgs e)
        {
            string decision = ((Picker)sender).SelectedItem.ToString();
            battleForged = "Yes".Equals(decision);
            checkContinue();
        }

        void SetCapType(object sender, EventArgs e)
        {
            armyCapType = ((Picker)sender).SelectedItem.ToString();

            if(armyCapType.Equals("No Limit"))
            {
                armyLimit.IsEnabled = false;
            } else
            {
                armyLimit.IsEnabled = true;
            }

            checkContinue();
        }

        void SetArmyCap(object sender, EventArgs e)
        {
            Entry entry = (Entry)sender;
            string amount = entry.Text;

            if (!Int32.TryParse(amount, out armyCap))
            {
                entry.Text = amount.Substring(0, amount.Length - 1);
            }

            checkContinue();
        }

        private void checkContinue()
        {
            if((armyCap > 0 || !armyLimit.IsEnabled) && armyCapType != null)
            {
                @continue.IsEnabled = true;
            } else
            {
                @continue.IsEnabled = false;
            }
        }

        void Cancel(object sender, EventArgs e)
        {
            popup.IsVisible = false;
            popupBlur.IsVisible = false;
        }

        async void CreateRedirect(object sender, EventArgs e)
        {
            if (battleForged) {
                if(armyCapType.Equals("No Limit"))
                {
                    await Navigation.PushAsync(new FactionSelectionPage());
                } else
                {
                    await Navigation.PushAsync(new FactionSelectionPage(armyCapType, armyCap));
                }
                
            } else {

            }
            
        }
    }
}
