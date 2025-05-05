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
        private int? result; // Question mark indicates it can be null has returns false when result.HasValue
        private readonly char[] SYMBOLS = new char[] { 'x', '/', '+', '-'};
        public Form1()
        {
            InitializeComponent();
            textBox1.AppendText("0");
        }

        private void button1_Click(object sender, EventArgs e) 
        { 

            if (textBox1.Text.Length == 1 && textBox1.Text.Equals("0"))
            {
                textBox1.Clear();
            }
        
            textBox1.AppendText("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length == 1 && textBox1.Text.Equals("0"))
            {
                textBox1.Clear();
            }
            textBox1.AppendText("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length == 1 && textBox1.Text.Equals("0"))
            {
                textBox1.Clear();
            }
            textBox1.AppendText("3");
        }

        // Button 4
        private void button8_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length == 1 && textBox1.Text.Equals("0"))
            {
                textBox1.Clear();
            }

            textBox1.AppendText("4");
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length == 1 && textBox1.Text.Equals("0"))
            {
                textBox1.Clear();
            }

            textBox1.AppendText("6");
        }

        // Add button
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || !SYMBOLS.Contains(textBox1.Text.ElementAt(textBox1.Text.Length - 1)))
            {
                textBox1.AppendText("+");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length == 1 && textBox1.Text.Equals("0"))
            {
                textBox1.Clear();
            }


            textBox1.AppendText("9");
        }

        // Equals button
        private void button9_Click_1(object sender, EventArgs e)
        {
            String toCalculate = textBox1.Text;
            result = calculate(toCalculate);

            textBox1.Clear();
            textBox1.AppendText(result.ToString());

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
            if (textBox1.Text.Length == 0 || !SYMBOLS.Contains(textBox1.Text.ElementAt(textBox1.Text.Length - 1)))
            {
                textBox1.AppendText("x");
            }
        }

        // Divide button
        private void button16_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || !SYMBOLS.Contains(textBox1.Text.ElementAt(textBox1.Text.Length - 1)))
            {
                textBox1.AppendText("/");
            }
        }

        // Button 5
        private void button7_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length == 1 && textBox1.Text.Equals("0"))
            {
                textBox1.Clear();
            }

            textBox1.AppendText("5");
        }

        // Button 7
        private void button12_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length == 1 && textBox1.Text.Equals("0"))
            {
                textBox1.Clear();
            }

            textBox1.AppendText("7");

        }

        // Button 8
        private void button11_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length == 1 && textBox1.Text.Equals("0"))
            {
                textBox1.Clear();
            }

            textBox1.AppendText("8");
        }

        // Button 0
        private void button14_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length == 1 && textBox1.Text.Equals("0"))
            {
                textBox1.Clear();
            }

            textBox1.AppendText("0");
        }

        // Button 9
        private void button10_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length == 1 && textBox1.Text.Equals("0"))
            {
                textBox1.Clear();
            }

            textBox1.AppendText("9");
        }

        // Subtract button
        private void button5_Click(object sender, EventArgs e)
        {
            

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
                if (parsed)
                {
                    numStr += c;
                }

                else if (numStr.Length != 0 && SYMBOLS.Contains(c))
                {
                    
                    nums.Add(int.Parse(numStr));
                    symbols.Add(c);
                    
                    numStr = "";

                }

            }

            if (numStr.Length != 0)
            {

                parsed = int.TryParse(numStr, out num);


                if (parsed)
                {
                    nums.Add(num);
                }
            }

        }

        private int calculate(String input)
        {

            parseCalculations(input);
            //foreach (int i in nums)
            //{
            //    Console.WriteLine(i);
            //}

            //foreach (char c in symbols)
            //{
            //    Console.WriteLine(c);
            //}

            if (nums.ToArray().Length - 1 != symbols.ToArray().Length)
            {
                return -1;
            }

            if (nums.ToArray().Length == 2)
            {
                return performArithmeticTwoNums(nums[0], nums[1], symbols[0]);
            }

            int index = 0;
            int num;
            int result = 0;

            foreach (char c in symbols)
            {
                num = nums[index];

                if (index == 0)
                {
                    result += performArithmeticTwoNums(num, nums[index+1], c);
                    index += 2;
                }

                else
                {
                    result = performArithmeticTwoNums(result, num, c);
                    index++;
                }

                if (result == -1)
                {
                    return -1;
                }


                
            }

            return result;
        }


        private int performArithmeticTwoNums(int num1, int num2, char symbol)
        {
            if (symbol == '+')
            {

                return num1 + num2;
            }

            else if (symbol == '-')
            {
                return num1 - num2;
            }

            else if (symbol == 'x')
            {
                return num1 * num2;
            }

            else if (symbol == '/')
            {
                if (num2 == 0)
                {
                    return -1;
                }

                return num1 / num2;
            }

            return -1;
        }


    }
}
