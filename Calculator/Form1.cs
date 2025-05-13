using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private String toCalculate;
        private List<int> nums;
        private List<char> symbols;
        private bool negative = false;
        private int? result; // Question mark indicates the value for result can be an integer or null
        private readonly char[] SYMBOLS = new char[] { 'x', '/', '+', '-'};
        public Form1()
        {
            InitializeComponent();
            textBox1.AppendText("0");
        }

        private void button1_Click(object sender, EventArgs e) 
        { 
            // Clear the textbox if there is only a 0 in the calculator or in case of an error before adding the digit

            if (textBox1.Text.Equals("0") || textBox1.Text.Equals("Error"))
            {
                textBox1.Clear();
            }


            textBox1.AppendText("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("0") || textBox1.Text.Equals("Error"))
            {
                textBox1.Clear();
            }
            textBox1.AppendText("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("0") || textBox1.Text.Equals("Error"))
            {
                textBox1.Clear();
            }
            textBox1.AppendText("3");
        }

        // Button 4
        private void button8_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("0") || textBox1.Text.Equals("Error"))
            {
                textBox1.Clear();
            }

            textBox1.AppendText("4");
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("0") || textBox1.Text.Equals("Error"))
            {
                textBox1.Clear();
            }

            textBox1.AppendText("6");
        }

        // Add button
        private void button4_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("Error"))
            {
                textBox1.Clear();
            }

            // If there is only one entry, or the last entry in the calculator wasn't a symbol, then add the new symbol
            if (textBox1.Text.Length == 1 || !SYMBOLS.Contains(textBox1.Text.ElementAt(textBox1.Text.Length - 1)))
            {
                textBox1.AppendText("+");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("0") || textBox1.Text.Equals("Error"))
            {
                textBox1.Clear();
            }


            textBox1.AppendText("9");
        }

        // Equals button
        private void button9_Click_1(object sender, EventArgs e)
        {
            toCalculate = textBox1.Text;
            result = calculate(toCalculate);

            textBox1.Clear();

            if (result == null)
            {
                textBox1.AppendText("Error");
            }

            else
            {
                textBox1.AppendText(result.ToString());
            }
                

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // Multiply button
        private void button13_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("Error"))
            {
                textBox1.Clear();
            }

            if (textBox1.Text.Length == 1 || !SYMBOLS.Contains(textBox1.Text.ElementAt(textBox1.Text.Length - 1)))
            {
                textBox1.AppendText("x");
            }
        }

        // Divide button
        private void button16_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("Error"))
            {
                textBox1.Clear();
            }

            if (textBox1.Text.Length == 1 || !SYMBOLS.Contains(textBox1.Text.ElementAt(textBox1.Text.Length - 1)))
            {
                textBox1.AppendText("/");
            }
        }

        // Button 5
        private void button7_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("0") || textBox1.Text.Equals("Error"))
            {
                textBox1.Clear();
            }

            textBox1.AppendText("5");
        }

        // Button 7
        private void button12_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("0") || textBox1.Text.Equals("Error"))
            {
                textBox1.Clear();
            }

            textBox1.AppendText("7");

        }

        // Button 8
        private void button11_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("0") || textBox1.Text.Equals("Error"))
            {
                textBox1.Clear();
            }

            textBox1.AppendText("8");
        }

        // Button 0
        private void button14_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("0") || textBox1.Text.Equals("Error"))
            {
                textBox1.Clear();
            }

            textBox1.AppendText("0");
        }

        // Button 9
        private void button10_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("0") || textBox1.Text.Equals("Error"))
            {
                textBox1.Clear();
            }

            textBox1.AppendText("9");
        }

        // Subtract button
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("Error") || textBox1.Text.Equals("0"))
            {
                textBox1.Clear();
            }
            

            if (textBox1.Text.Length == 0 || !SYMBOLS.Contains(textBox1.Text.ElementAt(textBox1.Text.Length - 1)))
            {
                textBox1.AppendText("-");
            }
        }

        
        // Clear button
        private void button15_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.AppendText("0");
        }

        private void parseCalculations(String input)
        {
            Boolean parsed = false ;
            int num;
            String numStr = "";
            nums = new List<int>();
            symbols = new List<char>();

            foreach (char c in input) {
                parsed = int.TryParse(char.ToString(c), out num);

                // If c can be parsed as an integer
                if (parsed)
                {
                    numStr += c;
                }

                // If first number is negative or we are past the first number and the c is a symbol
                else if ((numStr.Length == 0 && c == '-') || numStr.Length != 0 && SYMBOLS.Contains(c))
                {

                    if (numStr.Length != 0)
                    {
                        nums.Add(int.Parse(numStr));
                    }

                    // Keep all other symbols as is
                    if (c != '-')
                    {

                        symbols.Add(c);
                        numStr = "";
                    }

                    // Treat subtraction as addition with a negative number (5 - 5 = 5 + -5)
                    else
                    {
                        if (numStr.Length == 0)
                        {
                            nums.Add(0); // If first number in calculator is negative, perform 0 + -firstNum to avoid error
                        }

                        symbols.Add('+');

                        numStr = "-";

                    }

                    

                }

            }

            if (numStr.Length != 0) // Check if there is any remaining integers in the string
            {

                parsed = int.TryParse(numStr, out num);


                if (parsed)
                {
                    nums.Add(num);
                }
            }

        }

        

        private int? calculate(String input)
        {

            parseCalculations(input);

            foreach (int i in nums)
            {
                Console.WriteLine(i);
            }
            foreach (char c in symbols)
            {
                Console.WriteLine(c);
            }


            if (nums.Count == 2)
            {
                return performArithmeticTwoNums(nums[0], nums[1], symbols[0]);
            }

            int nums_index = 0;
            int num;
            int? result = 0;
            char symbol;

            int arithmetic_index = 0; // Index in SYMBOLS, keeps track of the index of the priority operator according to BEDMAS
            int current_arithmetic_index = 0; // Index in symbols list

            while (nums.Count != 1)
            {

                symbol = symbols[current_arithmetic_index];
                num = nums[nums_index];

                if (symbol == SYMBOLS[arithmetic_index]) // If current operator is the priority
                {

                    result = performArithmeticTwoNums(num, nums[nums_index + 1], symbol);


                    if (result == null || result > Int32.MaxValue || result < Int32.MinValue)
                    {
                        return null;
                    }

                    nums.RemoveAt(nums_index);
                    nums.RemoveAt(nums_index);
                    symbols.Remove(symbol);
                    
                    nums.Insert(0, (int) result);

                    current_arithmetic_index = 0;
                    nums_index = 0;

                }

                else
                {
                    current_arithmetic_index++;

                    if (current_arithmetic_index > symbols.Count - 1) // If the search for the prioritized symbol fails, then move on to the next highest operator
                    {
                        arithmetic_index++;
                        current_arithmetic_index = 0;
                        
                    }

                    else
                    {
                        nums_index++;

                        if (nums_index > nums.Count - 2) // If the nums_index doesn't allow for itself and the next numbered to be used, then reset the index
                        {
                            nums_index = 0;
                        }
                    }
                }

                if (nums.Count == 2)
                {

                    result = performArithmeticTwoNums(nums[0], nums[1], symbols[0]);
                    if (result == null || result > Int32.MaxValue || result < Int32.MinValue)
                    {
                        return null;
                    }

                    nums.Clear();
                    nums.Add((int)result);


                }

            }


            //foreach (char c in symbols)
            //{
            //    num = nums[index];

            //    if (index == 0)
            //    {
            //        result += performArithmeticTwoNums(num, nums[index + 1], c);
            //        index += 2;
            //    }

            //    else
            //    {
            //        result = performArithmeticTwoNums(result, num, c);
            //        index++;
            //    }



            //    if (result == null || result > Int32.MaxValue || result < Int32.MinValue)
            //    {
            //        return null;
            //    }


            //}

            return result;
        }


        private int? performArithmeticTwoNums(int? num1, int? num2, char symbol)
        {
            if (symbol == '+')
            {
                Console.WriteLine(num1 + "+" + num2 + "=" + (num1 + num2));
                return num1 + num2;
            }


            else if (symbol == 'x')
            {
                Console.WriteLine(num1 + "x" + num2 + "=" + num1 * num2);
                return num1 * num2;
            }

            else if (symbol == '/') 
            { 

                if (num2 == 0) // Zero division error
                {
                    return null;
                }

                Console.WriteLine(num1 + "/" + num2 + "=" + num1 / num2);

                return num1 / num2;
            }

            return null;
        }


    }
}
