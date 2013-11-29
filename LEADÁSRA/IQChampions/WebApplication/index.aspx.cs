using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQUtil;
using System.Data.Entity;
using System.Text;
using System.Data;


namespace WebApplication
{
    public partial class index : System.Web.UI.Page
    {
        StringBuilder SB = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            SB.Clear();
            int szaml = 1;
            foreach (var item in database.getTop5Players())
            {
                SB.Append(string.Format("<b>{0}. {1}</b>",szaml++,item));
                SB.Append("<br>");
            }
            lbTop5.Text = SB.ToString();

            if (!String.IsNullOrEmpty(loginPortletUname.Text) && !String.IsNullOrEmpty(loginPortletPass.Text) && IsPostBack)
            {
                //User bejelentkeztetése
                string uname = loginPortletUname.Text;
                string pass = loginPortletPass.Text;
                string passInMd5 = Hash.generate(pass);
                if (database.getLoginOfUser(uname, passInMd5))
                {
                    //Helyes login
                    Session.Add("currUsr", uname);
                }
                else
                    jsLiteral.Text = "alert('Hibás jelszó vagy felhasználónév!');";
            }

            string currentUser = Session["currUsr"] != null ? Session["currUsr"].ToString() : "";
            if (!String.IsNullOrEmpty(currentUser))
            {
                onBeforeLogin.Visible = false;
                pnRegistration.Visible = false;
                nameLabelLogOff.Text = currentUser;
                logoffPanel.Visible = true;
                statistics.Visible = true;
                DataTable dt = database.getStatistics4User(currentUser);
                SB.Clear();
                SB.Append("<table border=1>");
                SB.Append("<tr>");
                foreach (DataColumn item in dt.Columns)
                {
                    SB.AppendFormat("<td>{0}</td>", item.Caption);
                }
                SB.Append("</tr>");
                foreach (DataRow item in dt.Rows)
	            {
                    SB.Append("<tr>");
		            foreach (DataColumn inner in dt.Columns)
	                {
		                SB.Append("<td>");
                        if (inner.DataType == typeof(double))
                        {
                            SB.AppendFormat("{0} %", double.Parse(item[inner].ToString()) * 100);
                        }
                        else
                        {
                            SB.Append(item[inner]);
                        }
                        SB.Append("</td>"); 
	                }
                    SB.Append("</tr>");
	            }
                SB.Append("</table>");
                litStatistics.Text = SB.ToString();
            }
            else
            {
                //regisztrációs panel megjelenítése
                pnRegistration.Visible = true;
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string uname = lbUsrName_reg.Text;
            string pass = lbPass1_reg.Text;
            string mail = tbEmail.Text;
            string md5Pass = Hash.generate(pass);
            database.registerUser(uname, md5Pass, mail);
            pnRegistration.Visible = false;
            tbThankYaou.Visible = true;
            lbThankYouMessage.Text = String.Format("Köszönjük, hogy regisztáltál {0}, most már bejentkezhetsz!", uname);
        }

        protected void logOffButton_Click(object sender, EventArgs e)
        {
            if (Session["currUsr"] != null)
                Session.Remove("currUsr");
            Response.Redirect("index.aspx");
        }
    }
}