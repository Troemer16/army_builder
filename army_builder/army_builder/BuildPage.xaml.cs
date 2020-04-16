using army_builder.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace army_builder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuildPage : ContentPage
    {
        public BuildPage()
        {
            InitializeComponent();
        }

        public BuildPage(Faction faction)
        {
            InitializeComponent();
        }
    }
}