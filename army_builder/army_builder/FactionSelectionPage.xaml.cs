using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.Data.Sqlite;
using System;
using army_builder.model;

namespace army_builder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FactionSelectionPage : ContentPage
    {
        private IDictionary<string, List<string>> soupMainFactions;
        private IDictionary<string, List<string>> mainNamedFactions;
        private string facType;
        private CheckBox current;
        private string selectedFaction;

        public FactionSelectionPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            soupMainFactions = App.Db.getSoupFactions();
            mainNamedFactions = App.Db.getMainFactions();
        }

        public void PopulateFactionCategories(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            facType = (string)picker.SelectedItem;
            List<string> items = new List<string>();

            switch (facType)
            {
                case "Soup":
                    items.Add("N/A");
                    break;
                case "Main":
                    items = new List<string>(soupMainFactions.Keys);
                    break;
                case "Named":
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

            if(facCategory != null)
            {
                switch (facType)
                {
                    case "Soup":
                        items = new List<string>(soupMainFactions.Keys);
                        break;
                    case "Main":
                        soupMainFactions.TryGetValue(facCategory, out items);
                        break;
                    case "Named":
                        mainNamedFactions.TryGetValue(facCategory, out items); ;
                        break;
                    default:
                        Console.WriteLine("Error with faction types: " + facType);
                        break;
                }

                items.Sort();
            }
     
            factionOptions.ItemsSource = items;
        }
    }
}