using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace army_builder
{
    public partial class App : Application
    {
        public static string dbPath;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string filePath)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            dbPath = filePath;

            using (var source = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("army_builder.database.army_40k.db"))
            {
                using (FileStream destination = File.Create(dbPath))
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
