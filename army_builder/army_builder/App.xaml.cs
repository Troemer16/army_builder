using army_builder.database;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace army_builder
{
    public partial class App : Application
    {
        public static Database Db { get; private set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string filePath)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            using (var source = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("army_builder.database.army_40k.db"))
            {
                using (FileStream destination = File.Create(filePath))
                {
                    try
                    {
                        source.CopyTo(destination);
                    }
                    catch (NullReferenceException e)
                    {
                        Console.Write(e.StackTrace);
                    }

                }
            }

            Db = new Database(filePath);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
