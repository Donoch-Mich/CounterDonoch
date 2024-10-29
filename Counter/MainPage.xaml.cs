using System;
using System.IO;
using System.Xml.Linq;
using Microsoft.Maui.Controls;

namespace Counter
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadCounters();
        }

        private void addCounterName_Clicked(object sender, EventArgs e)
        {
            string counterName = string.IsNullOrWhiteSpace(CounterNameEntry.Text) ? "Domyślna nazwa" : CounterNameEntry.Text;

            int initialValue = 0;
            if (!string.IsNullOrEmpty(CounterValueEntry.Text) && int.TryParse(CounterValueEntry.Text, out int parsedValue))
            {
                initialValue = parsedValue;
            }
            else
            {
                DisplayAlert("Invalid Input", "Please enter a valid initial value.", "OK");
                return;
            }

            var customElement = new Counter.Controls.element
            {
                CounterName = counterName,
                InitialValue = initialValue
            };

            Controls.Children.Add(customElement);

            CounterNameEntry.Text = string.Empty;
            CounterValueEntry.Text = string.Empty;
        }

        private void LoadCounters()
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "counterData.xml");

            if (File.Exists(filePath))
            {
                try
                {
                    var doc = XDocument.Load(filePath);

                    foreach (var counterElement in doc.Descendants("Counter"))
                    {
                        string counterName = counterElement.Element("CounterName")?.Value ?? "Default Name";
                        int initialValue = int.TryParse(counterElement.Element("InitialValue")?.Value, out int parsedInitialValue) ? parsedInitialValue : 0;
                        int counterValue = int.TryParse(counterElement.Element("CounterValue")?.Value, out int parsedCounterValue) ? parsedCounterValue : initialValue;

                        var customElement = new Counter.Controls.element
                        {
                            CounterName = counterName,
                            InitialValue = initialValue
                        };

                        // Set _counterValue directly using SetCounterValue
                        customElement.SetCounterValue(counterValue);

                        Controls.Children.Add(customElement);
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", "Failed to load counters: " + ex.Message, "OK");
                }
            }
        }
    
    }
}
