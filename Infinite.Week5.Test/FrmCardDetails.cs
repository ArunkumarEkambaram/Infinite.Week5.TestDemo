using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Infinite.Week5.Test
{
    public partial class FrmCardDetails : Form
    {
        private SqlConnection con = null;
        private SqlCommand cmd = null;

        public FrmCardDetails()
        {
            InitializeComponent();
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            int returnValue = 0;

            using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["QuickKart"].ConnectionString))
            {
                using (cmd = new SqlCommand("usp_CreateNewCardDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CardNumber", TxtCardNumber.Text);
                    cmd.Parameters.AddWithValue("@NameOnCard", TxtNameOnCard.Text);
                    cmd.Parameters.AddWithValue("@CardType", CbCardType.GetItemText(CbCardType.SelectedItem));
                    cmd.Parameters.AddWithValue("@CVVNumber", TxtCVVNumber.Text);
                    cmd.Parameters.AddWithValue("@ExpiryDate", DTPExpiryDate.Value.FirstDayOfMonth());
                    cmd.Parameters.AddWithValue("@Balance", TxtBalance.Text);
                    //To get stored procedure return value
                    SqlParameter retVal = cmd.Parameters.Add("Return_Value", SqlDbType.Int);
                    retVal.Direction = ParameterDirection.ReturnValue;

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    cmd.ExecuteNonQuery();
                    //returnValue = Convert.ToInt32(retVal.Value);
                    returnValue = (int)retVal.Value;
                }
            }

            if (returnValue == -1)
            {
                LblMessage.Text = "Invalid Card Number";
            }
            else if (returnValue == -2)
            {
                LblMessage.Text = "Card Number already exists";
            }
            else if (returnValue == -3)
            {
                LblMessage.Text = "Card Type should be either 'M' or 'V'";
            }
            else if (returnValue == -4)
            {
                LblMessage.Text = "Invalid CVV Number";
            }
            else if (returnValue == -5)
            {
                LblMessage.Text = "Invalid Expiry Date";
            }
            else if (returnValue == -6)
            {
                LblMessage.Text = "Balance should be greater than 1000";
            }
            else if (returnValue == -99)
            {
                LblMessage.Text = "Server error. Please try later";
            }
            else
            {
                LblMessage.Text = $"Card Details created successfully\nYour Card Number is {TxtCardNumber.Text}";
            }
        }
    }
}
