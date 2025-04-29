using System;
namespace CalculatorApp
{
    public partial class MainPage : ContentPage
    {
        string currentInput = "";
        List<string> enters = new List<string>();
        string Operator = "";

        public MainPage()
        {
            InitializeComponent();


        }
        private async Task AnimateButton(Button button)
        {
            button.BorderWidth = 5;
            button.BorderColor = Colors.Black;
            await Task.Delay(120);
            button.BorderWidth = 0;
        }
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            if (Display.Text != "0")
            {
                Display.Text = "0";
                enters.Clear();
                Operator = "";
            }
            await AnimateButton(Delete);
            currentInput = "";
        }
        private async void PosNeg_Clicked(object sender, EventArgs e)
        {
            if (Display.Text != "0")
            {
                double lastNumber;
                string[] currentNums = currentInput.Split(' ');
                var currentNumsList = currentNums.ToList();
                if (double.TryParse(currentInput, out lastNumber))
                {
                    currentNumsList.RemoveAt(currentNumsList.Count - 1); // Remove the last number
                    lastNumber = -lastNumber; // Flip the sign

                    currentInput = lastNumber.ToString();

                    currentNumsList.Add(currentInput);
                    Display.Text = ""; // Clear the display
                    for (int i = 0; i < currentNumsList.Count; i++) // Update the display
                    {
                        Display.Text += currentNumsList[i];
                    }

                    //Fix result logic
                }
            }
            await AnimateButton(PosNeg);
        }


        private async void Percent_Clicked(object sender, EventArgs e)
        {
            //ToDo
            await AnimateButton(Percent);
        }
        private async void Division_Clicked(object sender, EventArgs e)
        {
            string last = Display.Text.Last().ToString();
            if (last != "÷")
            {
                Display.Text += "÷";
                enters.Add("÷");
                Operator += "÷";
                await AnimateButton(Division);
            }
            currentInput = "";
            await AnimateButton(Division);
        }

        private async void Number_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string number = button.Text;

       
            if (Display.Text == "0")
            {
                Display.Text = number;
                enters.Clear();
                enters.Add(number);
            }
            else
            {
                Display.Text += number;
                enters.Add(number);
            }
            currentInput += number;
            await AnimateButton(button);
        }

       

        private async void Multiply_Clicked(object sender, EventArgs e)
        {
            string last = Display.Text.Last().ToString();
            if (last != "-" && last != "+" && last != "x" && last != "÷")
            {
                Display.Text += "x";
                enters.Add("x");
                Operator += "x";
                await AnimateButton(Multiply);
            }
            await AnimateButton(Multiply);
            currentInput = "";
        }


        private async void Subtracked_Clicked(object sender, EventArgs e)
        {
            
            string last = Display.Text.Last().ToString();
            if (last != "-" && last != "+" && last != "x" && last != "÷")
            {
                Display.Text += "-";
                enters.Add("-");
                Operator += "- ";
                await AnimateButton(Subtracked);
            }
            await AnimateButton(Subtracked);
            currentInput = "";
        }


        private async void Add_Clicked(object sender, EventArgs e)
        {
            string last = Display.Text.Last().ToString();
            if (last != "-" && last != "+" && last != "x" && last != "÷")
            {

                Display.Text += "+";
                enters.Add("+");
                Operator += "+ ";
                await AnimateButton(Add);
                currentInput = "";
            }
            await AnimateButton(Add);
        }

        private async void Coma_Clicked(object sender, EventArgs e)
        {
            if (!Display.Text.EndsWith(","))
            {
                enters.Add(",");
                
                Display.Text += ",";
            }
            await AnimateButton(Coma);
        }

        private void Equal_Clicked(object sender, EventArgs e)
        {   
            if(Display.Text.Contains("+") || Display.Text.Contains("-") || Display.Text.Contains("x") || Display.Text.Contains("÷"))
            {
                double[] numbers = Display.Text.Split(new char[] { '+', '-', 'x', '÷' }, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
                string[] operators = Operator.Split(" ").ToArray();
                double result = 0;
                int i = 0;
                foreach (double number in numbers)
                {

                    if (result == 0)
                    {
                        result = number;
                    }
                    else
                    {
                        switch (operators[i])
                        {
                            case "+":
                                result += number;
                                break;
                            case "-":
                                result -= number;
                                break;
                            case "x":
                                result *= number;
                                break;
                            case "÷":
                                result /= number;
                                break;
                        }
                        i++;
                    }
                }
                
                Operator = "";
                result = Math.Round(result, 2);
                Display.Text = result.ToString();
                enters.Clear();
                enters.Add(result.ToString());
            }
        }

    }
}
