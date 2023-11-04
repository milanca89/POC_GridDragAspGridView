using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Expressions;

namespace GridViewWithFilter.UI
{
    public partial class _Default : System.Web.UI.Page
    {

        //Some calss to hold data for gridview.
        [Serializable]
        public class Outstanding
        {
            public int SequenceId { get; set; }
            public int RecordId { get; set; }

            public string Item { get; set; }
            public string Order { get; set; }
            public int Line { get; set; }
            public int Status { get; set; }
            public string ToLocation { get; set; }
            public decimal Qty { get; set; }
            public DateTime RegDate { get; set; }
            public string Location { get; set; }
            public decimal AllocQty { get; set; }


            public List<Outstanding> GetOutstanding()
            {
                List<Outstanding> lstOrders = new List<Outstanding>();

                lstOrders.Add(new Outstanding() { SequenceId=1, RecordId = 5000, Item = "CocaCola", Order = "000101", Line = 1, Status = 20, ToLocation = "Sydney", Qty = 2000, RegDate = new DateTime(2014, 1, 1), Location = "USA", AllocQty = 100 });
                lstOrders.Add(new Outstanding() { SequenceId = 3, RecordId = 5001,Item = "BubbleGum", Order = "000101", Line = 1, Status = 20, ToLocation = "Sydney", Qty = 2500, RegDate = new DateTime(2014, 1, 11), Location = "USA", AllocQty = 300 });
                lstOrders.Add(new Outstanding() { SequenceId = 4, RecordId = 5002, Item = "Coffee", Order = "000111", Line = 1, Status = 50, ToLocation = "Melbourne", Qty = 2500, RegDate = new DateTime(2014, 1, 10), Location = "USA", AllocQty = 100 });
                lstOrders.Add(new Outstanding() { SequenceId = 5, RecordId = 5003, Item = "Sugar", Order = "000112", Line = 1, Status = 50, ToLocation = "Melbourne", Qty = 2300, RegDate = new DateTime(2014, 1, 10), Location = "NZ", AllocQty = 300 });
                lstOrders.Add(new Outstanding() { SequenceId = 8, RecordId = 5004, Item = "Milk", Order = "000112", Line = 1, Status = 50, ToLocation = "Melbourne", Qty = 2300, RegDate = new DateTime(2014, 1, 10), Location = "NZ", AllocQty = 200 });
                lstOrders.Add(new Outstanding() { SequenceId = 9, RecordId = 5005, Item = "Green Tea", Order = "000112", Line = 1, Status = 20, ToLocation = "Melbourne", Qty = 300, RegDate = new DateTime(2014, 1, 10), Location = "NZ", AllocQty = 220 });
                lstOrders.Add(new Outstanding() { SequenceId = 10, RecordId = 5006, Item = "Biscuit", Order = "000131", Line = 1, Status = 70, ToLocation = "Perth", Qty = 200, RegDate = new DateTime(2014, 1, 12), Location = "IND", AllocQty = 10 });
                lstOrders.Add(new Outstanding() { SequenceId = 11, RecordId = 5007, Item = "Wrap", Order = "000131", Line = 1, Status = 20, ToLocation = "Perth", Qty = 2100, RegDate = new DateTime(2014, 1, 12), Location = "IND", AllocQty = 110 });
                return lstOrders;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Outstanding objOutstanding = new Outstanding();
                List<Outstanding> lstOutstandingOrders = new List<Outstanding>();
                lstOutstandingOrders = objOutstanding.GetOutstanding();
                grdViewOutstanding.DataSource = lstOutstandingOrders;
                grdViewOutstanding.DataBind();

                ViewState["lstOutstandingOrders"] = lstOutstandingOrders;
                upnlOutstanding.Update();
            }
        }

       
        protected void grdViewOutstanding_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["lstOutstandingOrders"] != null)
            {
                List<Outstanding> lstOutstandingOrders = (List<Outstanding>)ViewState["lstOutstandingOrders"];
                grdViewOutstanding.PageIndex = e.NewPageIndex;
                ViewState["lstOutstandingOrders"] = lstOutstandingOrders;
                grdViewOutstanding.DataSource = lstOutstandingOrders;
                grdViewOutstanding.DataBind();
               
                upnlOutstanding.Update();

            }
        }

        protected void btnUpdateOrderSequence_Click(object sender, EventArgs e)
        {
            int recordId = 0;
            int seq = 0;
            Dictionary<int, int> recordIdSeq = new Dictionary<int, int>();
            List<int> recordIds = new List<int>();
            List<int> sequences = new List<int>();
         
            foreach (GridViewRow row in grdViewOutstanding.Rows)
            {
                seq = int.Parse(row.Cells[0].Text.Trim());
                sequences.Add(seq); // List of Sequence Id
            }

            if (!string.IsNullOrEmpty(eleNewSeq.Value)) // New Sequenced RecordIds
            {
                string[] allNewOrderdRecordId = eleNewSeq.Value.Trim().Split(':'); // Get New Order of Record Ids
                for (int i = 0; i < allNewOrderdRecordId.Length; i++)
                {
                    if (int.TryParse(allNewOrderdRecordId[i], out recordId))
                    {
                        recordIds.Add(recordId);
                    }
                }
            }
            if (recordIds.Count > 0)
            {
                sequences.Sort();
                for (int i = 0; i < recordIds.Count; i++)
                {
                    recordIdSeq.Add(recordIds[i], sequences[i]); //Setup new order -> Which sequenceId is assinged to which Record
                }
            }
            //For the purpose of this demo this manipulation is done on the viewstate data. Ideally you may want to save the new sequencing in the database and rebind gridview
             if (ViewState["lstOutstandingOrders"] != null)
             {
                List<Outstanding> lstOutstandingOrders = (List<Outstanding>)ViewState["lstOutstandingOrders"];
                List<Outstanding> lstOutStandingResequenced = new List<Outstanding>();
                int i = 0;
                foreach (Outstanding objTemp in lstOutstandingOrders)
                {
                    objTemp.SequenceId = recordIdSeq[objTemp.RecordId];  // Assign New Sequence Id 
                    i++;
                    lstOutStandingResequenced.Add(objTemp);
                }
                if (lstOutStandingResequenced.Count > 0)
                {
                    lstOutStandingResequenced = lstOutStandingResequenced.OrderBy(x => x.SequenceId).ToList();
                    ViewState["lstOutstandingOrders"] = lstOutStandingResequenced;
                    grdViewOutstanding.DataSource = lstOutStandingResequenced;
                    grdViewOutstanding.DataBind();
                    upnlOutstanding.Update();
                }

            }


          

        }
        
    }


}


