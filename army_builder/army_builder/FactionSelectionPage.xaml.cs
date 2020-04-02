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
            string selected = (string)picker.SelectedItem;
            List<string> items = new List<string>();

            switch (selected)
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
                    Console.WriteLine("Error with faction types: " + selected);
                    break;
            }

            items.Sort();
            factionCategory.ItemsSource = items;
        }
    }
}