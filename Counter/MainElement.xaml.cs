using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counter
{
    public partial class MainElement
    {
        int count = 0;

        public MainElement()
        {
            InitializeComponent();
        }

        private void OnPlusCounterClicked(object sender, EventArgs e)
        {
            count++;

            ShowResult();
        }

        private void OnMinusCounterClicked(object sender, EventArgs e)
        {
            count--;

            ShowResult();
        }

        private void ShowResult()
        {

            Result.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(Result.Text);
        }
    }
}

