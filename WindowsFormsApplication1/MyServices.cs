using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.com.antechdiagnostics.dev;

namespace WindowsFormsApplication1
{
    class MyServices : com.antechdiagnostics.dev.services
    {

        public MyServices()
        {
        }
        private String m_HeaderName;
        private String m_HeaderValue;
        private LabOrderItems[] labOrderItems;
        private int numLabOrderItems = 1;

        protected override WebRequest GetWebRequest(Uri uri)
        {
            HttpWebRequest req = (HttpWebRequest)base.GetWebRequest(uri);

            try
            {

                if (null != this.m_HeaderName)
                {
                    req.Headers.Add(this.m_HeaderName, this.m_HeaderValue);
                    req.PreAuthenticate = true;
                }

            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
            return (WebRequest)req;
        }
        public void SetRequestHeader(String headerName, String headerValue)
        {
            this.m_HeaderName = headerName;
            this.m_HeaderValue = headerValue;
        }


        public void fetchAndPrintResults(int id, String username, String password)
        {
            try
            {
                //login info
                LoginObject login = new LoginObject();
                login.clinicId = id;              
                login.userName = username;
                login.password = password;

                    //get specific accession result
                //LabAccessionIdObject lid = new LabAccessionIdObject();
                //lid.labAccessionId = "KEV1478181230221";
                //LabResultObject lor = this.getLabResults(login, lid);
                //System.Diagnostics.Debug.Print("getLabResults: " + lor.labResults);
                ////    using (System.IO.StreamWriter file =
                ////    new System.IO.StreamWriter(@"C:\tmp\getLabResults.txt", true))
                ////    {
                ////        file.WriteLine(lor.labResults);
                ////    }

                //get all results
                LabResults[] l = this.getAllLabResults(login);
                foreach (LabResults lr in l)
                {
                    System.Diagnostics.Debug.Print("getAllLabResults: " + lr.labResults);
                }

                //Requisition requisition = new Requisition();
                //requisition.requisitionId = "109207-CCS00000071";
                //LabResultObject labResultObject = getLabResultsByRequisitionId(login,requisition);
                //System.Diagnostics.Debug.WriteLine(labResultObject.accessionResultId);
                //System.Diagnostics.Debug.WriteLine(labResultObject.labResults);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public PubCodeListPrice[] getUSDOS(int id, String username, String password)
        {
            LoginObject login = new LoginObject();
            login.clinicId = id;
            login.userName = username;
            login.password = password;
            return getUSPubCodeListPrice(login);
        }

        public PubCodeListPrice[] getCanadaDOS(int id, String username, String password)
        {
            LoginObject login = new LoginObject();
            login.clinicId = id;
            login.userName = username;
            login.password = password;
            return getCanadaPubCodeListPrice(login);
        }

        public void createMyLabOrder(int id, String username, String password)
        {
            LoginObject login = new LoginObject();
            login.clinicId = id;
            login.userName = username;
            login.password = password;

            labOrderItems = new LabOrderItems[numLabOrderItems];
            for (int i = 0; i < numLabOrderItems; i++)
            {
                labOrderItems[i] = new LabOrderItems();
                labOrderItems[i].orderCode = "SA100";
                labOrderItems[i].notes = "note for item " + i;
            }
                
            LabOrder labOrder = new LabOrder();
            labOrder.antechAccountId = "500";
            labOrder.clientExtId = "123456";
            labOrder.clientName = "Ondoy, O";
            labOrder.doctorName = "Smith";
            labOrder.isCriticalFlag = "N";
    
            labOrder.petAge = "1Y";
            labOrder.petBreed = "GRET";
            labOrder.petExtId = "11111";
            labOrder.petName = "Rocky";
            labOrder.petSex = "M";
            labOrder.petSpecies = "C";
            labOrder.requisitionId = "1789-KEV911"; //your unique order id

            labOrder.labOrderItems = labOrderItems;

            createLabOrder(login, labOrder);
        }
    }
}

