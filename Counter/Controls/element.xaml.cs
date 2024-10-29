using Microsoft.Maui.Controls;
using System.Xml.Linq;

namespace Counter.Controls
{
    public partial class element : ContentView
    {
        private int _counterValue;

        public static readonly BindableProperty CounterNameProperty =
            BindableProperty.Create(nameof(CounterName), typeof(string), typeof(element), default(string), propertyChanged: OnCounterNameChanged);

        public static readonly BindableProperty InitialValueProperty =
            BindableProperty.Create(nameof(InitialValue), typeof(int), typeof(element), 0, propertyChanged: OnInitialValueChanged);

        public string CounterName
        {
            get => (string)GetValue(CounterNameProperty);
            set => SetValue(CounterNameProperty, value);
        }

        public int InitialValue
        {
            get => (int)GetValue(InitialValueProperty);
            set => SetValue(InitialValueProperty, value);
        }

        public element()
        {
            InitializeComponent();
            UpdateCounterDisplay();
        }

        private static void OnCounterNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (element)bindable;
            control.Counter_Name.Text = (string)newValue;
        }

        private static void OnInitialValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (element)bindable;
            control._counterValue = (int)newValue;
            control.UpdateCounterDisplay();
        }

        private void OnPlusCounterClicked(object sender, EventArgs e)
        {
            _counterValue++;
            UpdateCounterDisplay();
            SaveCounter(sender, e);
        }

        private void OnMinusCounterClicked(object sender, EventArgs e)
        {
            _counterValue--;
            UpdateCounterDisplay();
            SaveCounter(sender, e);
        }

        private void ResetCounter(object sender, EventArgs e)
        {
            _counterValue = InitialValue;
            UpdateCounterDisplay();
            SaveCounter(sender, e);
        }

        private void UpdateCounterDisplay()
        {
            Result.Text = _counterValue.ToString();
            
        }
        private void SaveCounter(object sender, EventArgs e)
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "counterData.xml");
            XDocument saveFile;

            
            if (File.Exists(filePath))
            {
                saveFile = XDocument.Load(filePath);
            }
            else
            {
                saveFile = new XDocument(new XElement("Counters"));
            }

            
            var existingCounter = saveFile.Root.Elements("Counter")
                .FirstOrDefault(x => (string)x.Element("CounterName") == CounterName);

            if (existingCounter != null)
            {
                // Update the existing counter's values
                existingCounter.Element("InitialValue").Value = InitialValue.ToString();
                existingCounter.Element("CounterValue").Value = _counterValue.ToString();
            }
            else
            {
                var newCounter = new XElement("Counter",
                    new XElement("CounterName", CounterName),
                    new XElement("InitialValue", InitialValue),
                    new XElement("CounterValue", _counterValue)
                );

                saveFile.Root.Add(newCounter);
            }

            saveFile.Save(filePath);
        }
        public void SetCounterValue(int value)
        {
            _counterValue = value;
            UpdateCounterDisplay();
        }




    }
}