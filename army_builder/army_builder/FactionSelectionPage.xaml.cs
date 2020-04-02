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
        }

        public void PopulateFactionCategories(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            string selected = (string)picker.SelectedItem;

            switch (selected)
            {
                case "Soup":
                    List<string> temp = new List<string>();
                    temp.Add("N/A");
                    factionCategory.ItemsSource = temp;
                    break;
                case "Main":
                    factionCategory.ItemsSource = new List<string>(soupMainFactions.Keys);
                    break;
                case "Named":
                    break;
                default:
                    Console.WriteLine("Error with faction types: " + selected);
                    break;
            }
        }
    }
}