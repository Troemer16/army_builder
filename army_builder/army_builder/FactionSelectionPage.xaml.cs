using army_builder.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace army_builder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FactionSelectionPage : ContentPage
    {
        private IDictionary<string, List<string>> soupMainFactions;
        private IDictionary<string, List<string>> mainNamedFactions;
        private string facType;
        private string decision;

        public FactionSelectionPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            soupMainFactions = App.Db.GetSoupFactions();
            mainNamedFactions = App.Db.GetMainFactions();
        }

        public void PopulateFactionCategories(object sender, EventArgs e)
        {
            //clear current options as new options are being selected
            factionOptions.ItemsSource = new List<string>();

            Picker picker = (Picker)sender;
            facType = ((string)picker.SelectedItem).ToLower();
            List<string> items = new List<string>();

            switch (facType)
            {
                case "soup":
                    items.Add("N/A");
                    break;
                case "main":
                    items = new List<string>(soupMainFactions.Keys);
                    break;
                case "named":
                    items = new List<string>(mainNamedFactions.Keys);
                    break;
                default:
                    Console.WriteLine("Error with faction types: " + facType);
                    break;
            }

            items.Sort();
            factionCategory.ItemsSource = items;
        }

        public void PopulateOptions(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            string facCategory = (string)picker.SelectedItem;
            List<string> items = new List<string>();

            if (facCategory != null)
            {
                switch (facType)
                {
                    case "soup":
                        items = new List<string>(soupMainFactions.Keys);
                        break;
                    case "main":
                        soupMainFactions.TryGetValue(facCategory, out items);
                        break;
                    case "named":
                        mainNamedFactions.TryGetValue(facCategory, out items); ;
                        break;
                    default:
                        Console.WriteLine("Error with faction types: " + facType);
                        break;
                }

                items.Sort();
                factionOptions.ItemsSource = items;
            }
        }

        public void SetDecision(object sender, SelectedItemChangedEventArgs e)
        {
            decision = e.SelectedItem.ToString();
            continueButton.IsEnabled = true;
        }

        public void BuildRedirect(object sender, EventArgs e)
        {
            
            string table = facType + "_factions";
            int id = App.Db.GetId(decision, table);

            Faction faction = new Faction(id, decision, facType);
            Navigation.PushAsync(new BuildPage(faction));
        }
    }
}