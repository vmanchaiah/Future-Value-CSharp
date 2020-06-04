using System;
using System.Windows.Forms;

namespace FutureValue
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            IsValidData();
            try
            {
                decimal monthlyInvestment = Convert.ToDecimal(txtMonthlyInvestment.Text);
                decimal yearlyInterestRate = Convert.ToDecimal(txtInterestRate.Text);
                int years = Convert.ToInt32(txtYears.Text);

                int months = years * 12;
                decimal monthlyInterestRate = yearlyInterestRate / 12 / 100;

                decimal futureValue = this.CalculateFutureValue(
                    monthlyInvestment, monthlyInterestRate, months);
                txtFutureValue.Text = futureValue.ToString("c");
                txtMonthlyInvestment.Focus();
            }
            //try-catch statement for FormatException and Overflow Exception

            /* catch (FormatException ex)
             {
                 MessageBox.Show("Format Error: Please enter a valid number for the field");
             }
             catch (OverflowException ex)
             {
                 MessageBox.Show("Overflow Error: Please enter a valid number for the field");
             }*/

            //try-catch statement that will catch any other exception

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.GetType().ToString() + "\n\n" + ex.StackTrace, "Exception");
            }
        }

        private decimal CalculateFutureValue(decimal monthlyInvestment,
            decimal monthlyInterestRate, int months)
        {
            decimal futureValue = 0m;
            for (int i = 0; i < months; i++)
            {
                futureValue = (futureValue + monthlyInvestment)
                                * (1 + monthlyInterestRate);

                //throw new Exception("Unknown Error: Please try again");

            }
            return futureValue;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool IsPresent(TextBox textBox, string name)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(name + " is a required field. ", "Entry Error");
                textBox.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsDecimal(TextBox textBox, string name)
        {
            if (Decimal.TryParse(textBox.Text, out _))
            {
                return true;
            }
            else
            {
                MessageBox.Show(name + " must be a decimal value.", "Entry Error");
                textBox.Focus();
                return false;
            }
        }

        public bool IsInt32(TextBox textBox, string name)
        {
            if (Int32.TryParse(textBox.Text, out _))
            {
                return true;
            }
            else
            {
                MessageBox.Show(name + " must be 32. ", "Entry Error");
                textBox.Focus();
                return false;
            }
        }

        public bool IsWithinRange(TextBox textBox, string name, decimal min, decimal max)

        {
            decimal number = Convert.ToDecimal(textBox.Text);
            if (number < min || number > max)
            {
                MessageBox.Show(name + " must be between " + min + " and " + max + ".", "Entry Error");
                textBox.Focus();
                return false;

            }
            else
            {
                return true;
            }
        }

        public bool IsValidData()
        {
            return

            IsPresent(txtMonthlyInvestment, "Monthly Investment") &&
            IsDecimal(txtMonthlyInvestment, "Monthly Investment") &&
            IsWithinRange(txtMonthlyInvestment, "Monthly Investment", 1, 1000) &&

            IsPresent(txtInterestRate, "Interest Rate") &&
            IsDecimal(txtInterestRate, "Interest Rate") &&
            IsWithinRange(txtInterestRate, "Interest Rate", 1, 15) &&

            IsPresent(txtYears, "Years") &&
            IsDecimal(txtYears, "Years") &&
            IsWithinRange(txtYears, "Years", 1, 20);
        }
    }
}

