using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebApplication
{
    public static class database
    {
        static string connectionString = WebConfigurationManager.ConnectionStrings["IQChampionsEntities"].ToString();

        /// <summary>
        /// Visszaadja a játékosok neveit
        /// </summary>
        /// <returns></returns>
        public static List<string> getNamesOfPlayers()
        {
            List<string> retVal = new List<string>();
            string sqlCommand = "select name from dbUserSet";
            SqlCommand comm = new SqlCommand(sqlCommand);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                comm.Connection = conn;
                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                    retVal.Add(dr["name"].ToString());
            }
            return retVal;
        }

        /// <summary>
        /// Visszaadja, hogy az usernek jó-e a loginja
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static bool getLoginOfUser(string name, string pass)
        {
            bool retVal = false;
            SqlCommand sql = new SqlCommand("checkLogin") { CommandType = System.Data.CommandType.StoredProcedure };
            sql.Parameters.Add(new SqlParameter("name", name));
            sql.Parameters.Add(new SqlParameter("pass", pass));
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                sql.Connection = conn;
                conn.Open();
                SqlDataReader dr = sql.ExecuteReader();
                if (dr.Read())
                {
                    retVal = int.Parse(dr[0].ToString()) > 0;
                }
            }
            return retVal;
        }

        public static void registerUser(string name, string password, string email)
        {
            SqlCommand sql = new SqlCommand("regUser") { CommandType = System.Data.CommandType.StoredProcedure };
            sql.Parameters.Add(new SqlParameter("name", name));
            sql.Parameters.Add(new SqlParameter("pass", password));
            sql.Parameters.Add(new SqlParameter("email", email));
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                sql.Connection = conn;
                conn.Open();
                sql.ExecuteNonQuery();
            }
        }

        public static List<string> getTop5Players()
        {
            List<string> retVal = new List<string>();
            SqlCommand comm = new SqlCommand("getTop5Winner") { CommandType = System.Data.CommandType.StoredProcedure };
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                comm.Connection = conn;
                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                    retVal.Add(dr[0].ToString());
            }
            return retVal;
        }

        public static DataTable getStatistics4User(string userName)
        {
            DataTable retVal = new DataTable();
            retVal.Columns.Add(new DataColumn("played", typeof(int)) {Caption = "Játszott"});
            retVal.Columns.Add(new DataColumn("win", typeof(int)) {Caption = "Nyert"});
            retVal.Columns.Add(new DataColumn("winPercent", typeof(double)) {Expression = "win / played", Caption = "Nyerési százalék" });
            retVal.Columns.Add(new DataColumn("questions",typeof(int)) {Caption = "Megválaszolt kérdések"});
            retVal.Columns.Add(new DataColumn("goodAnswers", typeof(int)) {Caption = "Helyesen megválaszolt kérdések"});
            retVal.Columns.Add(new DataColumn("answerPercent", typeof(double)) {Expression = "goodAnswers / questions", Caption = "Százalék"});

            SqlCommand comm = new SqlCommand("getStatOfUsr") {CommandType = CommandType.StoredProcedure};
            comm.Parameters.Add(new SqlParameter("name",userName));
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                comm.Connection = conn;
                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    DataRow dtr = retVal.NewRow();
                    dtr["played"] = int.Parse(dr["played"].ToString());
                    dtr["goodAnswers"] = int.Parse(dr["goodanswers"].ToString());
                    dtr["questions"] = int.Parse(dr["questions"].ToString());
                    dtr["win"] = int.Parse(dr["win"].ToString());
                    retVal.Rows.Add(dtr);
                }
            }
            return retVal;
        }
    }
}