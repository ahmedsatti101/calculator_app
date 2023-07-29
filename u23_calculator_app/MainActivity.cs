using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Sql;
using Javax.Security.Auth;

namespace u23_calculator_app
{
    [Activity(Label = "Calculator", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.FullUser)]
    public class MainActivity : Activity
    {
        private TextView mTextView;
        private Button buttonEquals;
        private Button buttonClear;
        private Button button0;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button modulus_button;
        private Button brackets_button;
        private Button brackets_button2;
        private bool isBracketOpen;
        private ImageButton delete_button;
        private Button exponentiation_button;
        private Button divide_button;
        private Button multiply_button;
        private Button add_button;
        private Button decimal_button;
        private Button minus_button;
        private Button square_button;
        private Button SquareRootButton;
        Button[] buttons;

        public Button? button1 { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            mTextView = FindViewById<TextView>(Resource.Id.mTextView);
            buttonEquals = FindViewById<Button>(Resource.Id.buttonEquals);
            buttonClear = FindViewById<Button>(Resource.Id.buttonClear);
            button0 = FindViewById<Button>(Resource.Id.button0);
            button1 = FindViewById<Button>(Resource.Id.button1);
            button2 = FindViewById<Button>(Resource.Id.button2);
            button3 = FindViewById<Button>(Resource.Id.button3);
            button4 = FindViewById<Button>(Resource.Id.button4);
            button5 = FindViewById<Button>(Resource.Id.button5);
            button6 = FindViewById<Button>(Resource.Id.button6);
            button7 = FindViewById<Button>(Resource.Id.button7);
            button8 = FindViewById<Button>(Resource.Id.button8);
            button9 = FindViewById<Button>(Resource.Id.button9);
            modulus_button = FindViewById<Button>(Resource.Id.modulus_button);
            divide_button = FindViewById<Button>(Resource.Id.divide_button);
            delete_button = FindViewById<ImageButton>(Resource.Id.delete_button);
            brackets_button = FindViewById<Button>(Resource.Id.brackets_button);
            brackets_button2 = FindViewById<Button>(Resource.Id.brackets_button2);
            exponentiation_button = FindViewById<Button>(Resource.Id.exponentiation_button);
            multiply_button = FindViewById<Button>(Resource.Id.multiply_button);
            add_button = FindViewById<Button>(Resource.Id.add_button);
            decimal_button = FindViewById<Button>(Resource.Id.decimal_button);
            minus_button = FindViewById<Button>(Resource.Id.minus_button);
            square_button = FindViewById<Button>(Resource.Id.square_button);
            SquareRootButton = FindViewById<Button>(Resource.Id.SquareRootButton);

            buttonEquals.Click += ButtonEquals_Click;
            buttonClear.Click += ButtonClear_Click;
            delete_button.Click += DeleteButton_Click;
            square_button.Click += SquareButton_Click;
            exponentiation_button.Click += ExponentButton_Click;
            SquareRootButton.Click += SquareRootButton_Click;


            buttons = new Button[]
        {
           FindViewById<Button>(Resource.Id.button0),
           FindViewById<Button>(Resource.Id.button1),
           FindViewById<Button>(Resource.Id.button2),
           FindViewById<Button>(Resource.Id.button3),
           FindViewById<Button>(Resource.Id.button4),
           FindViewById<Button>(Resource.Id.button5),
           FindViewById<Button>(Resource.Id.button6),
           FindViewById<Button>(Resource.Id.button7),
           FindViewById<Button>(Resource.Id.button8),
           FindViewById<Button>(Resource.Id.button9),
           FindViewById<Button>(Resource.Id.modulus_button),
           FindViewById<Button>(Resource.Id.divide_button),
           FindViewById<Button>(Resource.Id.brackets_button),
           FindViewById<Button>(Resource.Id.brackets_button2),
           FindViewById<Button>(Resource.Id.exponentiation_button),
           FindViewById<Button>(Resource.Id.multiply_button),
           FindViewById<Button>(Resource.Id.add_button),
           FindViewById<Button>(Resource.Id.decimal_button),
           FindViewById<Button>(Resource.Id.minus_button),
           FindViewById<Button>(Resource.Id.square_button),
           FindViewById<Button>(Resource.Id.SquareRootButton),

         };
            
            
        foreach (Button button in buttons)
            {
                button.Click += Button_Click;
            }

        void Button_Click(object sender, EventArgs e)
            {
                string buttonText = ((Button)sender).Text;

                if (buttonText == "=")
                {
                    try
                    {
                        string expression = mTextView.Text;
                        double result = EvaluateExpression(expression);
                        mTextView.Text = result.ToString();
                    }
                    catch (System.Exception ex)
                    {
                        mTextView.Text = "Error";
                    }
                }
                else if (buttonText == "C")
                {
                    mTextView.Text = string.Empty;
                }
                else
                {
                    mTextView.Text += buttonText;
                }
            }    
            
        
        void DeleteButton_Click(object sender, EventArgs e)

                {
                    if (mTextView.Text.Length > 0)
                    {
                        mTextView.Text = mTextView.Text.Substring(0, mTextView.Text.Length - 1);
                    }
                }
            
        void ButtonEquals_Click(object sender, System.EventArgs e)
            {
                // Perform the calculation
                string input = mTextView.Text;
                if (!string.IsNullOrEmpty(input))
                {
                    double result = EvaluateExpression(input);
                    mTextView.Text = result.ToString();
                }
                else
                {
                    mTextView.Text = "Invalid input";
                }
            }

        void ButtonClear_Click(object sender, System.EventArgs e)
            {
                mTextView.Text = string.Empty;
            }

        void ModulusButton_Click(object sender, EventArgs e)
            {
                string input = mTextView.Text;

                if (!string.IsNullOrEmpty(input))
                {
                    string[] operands = input.Split('%');

                    if (operands.Length == 2)
                    {
                        if (double.TryParse(operands[0], out double operand1) && double.TryParse(operands[1], out double operand2))
                        {
                            double result = operand1 % operand2;
                            mTextView.Text = result.ToString();
                        }
                        else
                        {
                            mTextView.Text = "Invaild input";
                        }
                    }
                    else
                    {
                        mTextView.Text = "Invalid input";
                    }
                }
                else
                {
                    mTextView.Text = "Invalid input";
                }
            }

        void BracketsButton_Click(object sender, EventArgs e)
            {
                string bracket = isBracketOpen ? "(" : ")";
                mTextView.Text += bracket;
                isBracketOpen = !isBracketOpen;
            }

        void SquareButton_Click(object sender, EventArgs e)
            {
                if (double.TryParse(mTextView.Text, out double value))
                {
                    double result = value * value;
                    mTextView.Text = result.ToString();
                }
                else
                {
                    mTextView.Text = "Invalid input";
                }
            }
            
        void ExponentButton_Click(object sender, EventArgs e)
            {
                if (double.TryParse(mTextView.Text, out double baseValue))
                {
                    // Prompt the user for the exponent
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetTitle("Enter Exponent");
                    EditText input = new EditText(this);
                    input.InputType = Android.Text.InputTypes.ClassNumber;
                    builder.SetView(input);
                    builder.SetPositiveButton("OK", (dialog, which) =>
                    {
                        if (double.TryParse(input.Text, out double exponent))
                        {
                            double result = System.Math.Pow(baseValue, exponent);
                            mTextView.Text = $"{baseValue}^{exponent} = {result}";
                        }
                        else
                        {
                            mTextView.Text = "Invalid exponent";
                        }
                    });
                    builder.SetNegativeButton("Cancel", (dialog, which) =>
                    {
                        // Do nothing if the user cancels
                    });
                    builder.Show();
                }
                else
                {
                    mTextView.Text = "Invalid input";
                }
            }


        void SquareRootButton_Click(object sender, EventArgs e)
            {
                if (double.TryParse(mTextView.Text, out double value))
                {
                    if (value >= 0)
                    {
                        double result = System.Math.Sqrt(value);
                        mTextView.Text = $"√{value} = {result}";
                    }
                    else
                    {
                        mTextView.Text = "Invalid input";
                    }
                }
                else
                {
                    mTextView.Text = "Invalid input";
                }
            }

        double EvaluateExpression(string expression)
            {
                try
                {
                    expression = expression.Replace("%", "% ");

                    System.Data.DataTable table = new System.Data.DataTable();
                    table.Columns.Add("expression", typeof(string), expression);
                    System.Data.DataRow row = table.NewRow();
                    table.Rows.Add(row);
                    double result = double.Parse((string)row["expression"]);
                    return result;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Error occured during expression evaluation: " + ex.Message);
                    return 0;
                }
            }
        }
    }
}