using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.com.antechdiagnostics.dev;

namespace WindowsFormsApplication1
{
    
    public partial class Form1 : Form
    {
        MyServices myService = new MyServices();
        public Form1()
        {
            InitializeComponent();
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                
                myService.fetchAndPrintResults(int.Parse(textBox1.Text), textBox2.Text, textBox3.Text);

                foreach (PubCodeListPrice lp in myService.getUSDOS(int.Parse(textBox1.Text), textBox2.Text, textBox3.Text))
                {
                    listBox1.Items.Add(lp.orderCodeName + " - " + lp.orderCodePrice);

                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myService.createMyLabOrder(int.Parse(textBox1.Text), textBox2.Text, textBox3.Text);
        }
    }
}