using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculate
{
    public partial class MainPage : ContentPage
    {
        private string currentInput = string.Empty;
        private string leftOperand = string.Empty;
        private string rightOperand = string.Empty;
        private string currentOperator = string.Empty;
        private ObservableCollection<Operation> operations;
        public ObservableCollection<Operation> Operations { get { return operations; } }

        public MainPage()
        {
            operations = new ObservableCollection<Operation>();

            InitializeComponent();
            HistoryView.ItemsSource = Operations;

        }

        void OnNumberButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            currentInput += button.Text;
            ResultText.Text = currentInput;
        }

        void OnOperatorButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;

            if (!string.IsNullOrEmpty(currentInput))
            {
                if (!string.IsNullOrEmpty(leftOperand))
                {
                    rightOperand = currentInput;
                    double left = double.Parse(leftOperand);
                    double right = double.Parse(rightOperand);
                    Calculate(left, right, currentOperator);
                    leftOperand = ResultText.Text;
                }
                else
                {
                    leftOperand = currentInput;
                }
            }
            PrevResult.Text = leftOperand + button.Text;
            currentInput = string.Empty;
            currentOperator = button.Text;
        }

        void OnEqualButtonClicked(object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(leftOperand) && !string.IsNullOrEmpty(currentInput))
            {
                double left = double.Parse(leftOperand);
                double right = double.Parse(currentInput);
                Calculate(left, right, currentOperator);
                leftOperand = ResultText.Text;
                currentInput = string.Empty;
                currentOperator = string.Empty;
            }
        }

        void Calculate(double left, double right, string operatorSymbol)
        {
            switch (operatorSymbol)
            {
                case "+":
                    ResultText.Text = (left + right).ToString();
                    
                    break;
                case "-":
                    ResultText.Text = (left - right).ToString();
                    break;
                case "*":
                    ResultText.Text = (left * right).ToString();
                    break;
                case "/":
                    if (right == 0)
                    {
                        ResultText.Text = "Cannot divide by zero";
                    }
                    else
                    {
                        ResultText.Text = (left / right).ToString();
                    }
                    break;
            }
            PrevResult.Text = String.Format("{0} {1} {2}", left, operatorSymbol, right);
            operations.Add(new Operation { OperationBody = PrevResult.Text+" = ", Result = ResultText.Text });
        }

        void OnClearButtonClicked(object sender, EventArgs args)
        {
            currentInput = string.Empty;
            leftOperand = string.Empty;
            rightOperand = string.Empty;
            currentOperator = string.Empty;
            ResultText.Text = "0";
            PrevResult.Text = "";
        }

        void ShowOperationHistory(object sender, EventArgs args) 
        {
            HistoryLayout.IsVisible = true;
            MainLayout.IsVisible = false;
        }

        void HideOperationHistory(object sender, EventArgs args)
        {
            HistoryLayout.IsVisible = false;
            MainLayout.IsVisible = true;
        }
    }
}
