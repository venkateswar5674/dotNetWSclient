using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            try {
                this.listBox1.Text = "Querying the web service ...";
                WindowsFormsApplication1.ServiceReference1.ServiceClient port = new ServiceReference1.ServiceClient();


                WindowsFormsApplication1.ServiceReference1.loginObject lo = new ServiceReference1.loginObject();
                lo.clinicId = int.Parse(textBox1.Text);
                lo.corporateId = 0;
                lo.userName = textBox2.Text;
                lo.password = textBox3.Text;


                foreach (WindowsFormsApplication1.ServiceReference1.pubCodeListPrice lp in port.getPubCodeListPrice_US(lo)) {
                    //System.Diagnostics.Debug.WriteLine(lp.orderCodeName + " , " + lp.orderCodePrice);
                    listBox1.Items.Add(lp.orderCodeName + " - " + lp.orderCodePrice );

                }

            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

    }
}
