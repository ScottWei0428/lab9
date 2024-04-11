using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace lab9
{
    public partial class MainPage : ContentPage
    {
        private double? _currentValue = null;
        private double? _storedValue = null;
        private string _currentOperation;
        private bool _equalPressed;

        public MainPage()
        {
            InitializeComponent();
            ResetCalculator();
        }

        private void ResetCalculator()
        {
            _currentValue = null;
            _storedValue = null;
            _currentOperation = string.Empty;
            _equalPressed = false;
            CalculatorDisplay.Text = "0";
        }

        private async void OnNumberClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await BlinkButton(button);

            if (button.Text == "C")
            {
                ResetCalculator();
                return;
            }

            if (_equalPressed)
            {
                
                ResetCalculator();
            }

            string pressedNumber = button.Text;

            if (button.Text == ".")
            {
                
                if (!CalculatorDisplay.Text.Contains("."))
                {
                    CalculatorDisplay.Text = _currentValue == null ? "0." : CalculatorDisplay.Text + ".";
                }
            }
            else
            {
                
                if (CalculatorDisplay.Text == "0" || _currentValue == null)
                {
                    CalculatorDisplay.Text = button.Text;
                }
                else
                {
                    CalculatorDisplay.Text += button.Text;
                }
            }

            
            if (double.TryParse(CalculatorDisplay.Text, out double newValue))
            {
                _currentValue = newValue;
            }



            _currentValue = double.Parse(CalculatorDisplay.Text);
        }

        private async void OnOperationClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await BlinkButton(button);

            if (_storedValue != null && _currentValue != null)
            {
                PerformCalculation();
                _storedValue = _currentValue;
                _currentValue = null;
            }
            else if (_currentValue != null)
            {
                _storedValue = _currentValue;
                _currentValue = null;
            }

            _currentOperation = button.Text;
            _equalPressed = false; 
        }

        private async void OnEqualClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await BlinkButton(button);

            PerformCalculation();
            _currentOperation = null;
            _equalPressed = true; 
        }

        private void PerformCalculation()
        {
            if (_currentOperation == null || _currentValue == null || _storedValue == null) return;

            switch (_currentOperation)
            {
                case "+":
                    _currentValue = _storedValue + _currentValue;
                    break;
                case "-":
                    _currentValue = _storedValue - _currentValue;
                    break;
                case "×":
                    _currentValue = _storedValue * _currentValue;
                    break;
                case "÷":
                    if (_currentValue == 0)
                    {
                        CalculatorDisplay.Text = "Error";
                        return;
                    }
                    _currentValue = _storedValue / _currentValue;
                    break;
            }

            CalculatorDisplay.Text = _currentValue.ToString();
            _storedValue = null;
           
        }

        private async Task BlinkButton(Button button)
        {
            var originalColor = button.BackgroundColor;
            button.BackgroundColor = Colors.Green;
            await Task.Delay(100);
            button.BackgroundColor = originalColor;
        }
    }
}
