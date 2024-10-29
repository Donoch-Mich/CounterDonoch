using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counter
{
    public partial class MainPage
    {
        int basicValue = 0;

        int value = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnPlusCounterClicked(object sender, EventArgs e)
        {
            value++;

            ShowResult();
        }

        private void OnMinusCounterClicked(object sender, EventArgs e)
        {
            value--;

            ShowResult();
        }

        private void ShowResult()
        {

            Result.Text =  $"{value}";
                
            SemanticScreenReader.Announce(Result.Text);
        }

        private void addCounter(object sender, EventArgs e)
        {
            
        }

        private void ReserCounter(object sender, EventArgs e)
        {
            value = basicValue;
            ShowResult();
        }
        

    }
}

