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


                foreach (WindowsFormsApplication1.ServiceReference1.pubCodeListPrice lp in port.getPubCodeListPrice_US(lo))
                {
                    System.Diagnostics.Debug.WriteLine(lp.orderCodeName + " , " + lp.orderCodePrice);
                    listBox1.Items.Add(lp.orderCodeName + " - " + lp.orderCodePrice);

                }
                //to invoke a different method from the service - createLabOrder for example
                ServiceReference1.labOrder order = new ServiceReference1.labOrder(); //instantiate a lab order object
                ServiceReference1.labOrderItems item1 = new ServiceReference1.labOrderItems(); //instantiate lab order items

                //now stuff all the info to the order object
                order.antechAccountId = "45116";
                order.clientExtId = "124637";
                order.clientName = "Adam Smith";
                order.doctorName = "Aucoin";
                order.isCriticalFlag = "N";
                order.labId = "1";

                //keep stuff unitl full :), next with clientName, so on

                //stuff the order item
                item1.notes = "VET SCREEN";
                item1.orderCode = "SA025";
                order.petAge = "3";
                order.petBreed = "POM";
                order.petName = "bear";
                order.petSex = "M";
                order.petSpecies = "C";
                order.requisitionId = "WW-11111111"; //need to be unique everytime to create an order

                ServiceReference1.labOrderItems[] list = new ServiceReference1.labOrderItems[1];  //instantiate base on the numbers of orderitems that you will have              
                list[0] = item1; //assign each lab order item into the list
                order.labOrderItems = list; //then assign it to the laborder obj

                //invoke the service method to create your order

                WindowsFormsApplication1.ServiceReference1.labOrderResults result =  port.createLabOrder(lo, order);
                 
                System.Diagnostics.Debug.WriteLine(result.labOrderResults1);

            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

    }
}
