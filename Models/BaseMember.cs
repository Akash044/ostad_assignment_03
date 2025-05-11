using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Runtime.Remoting.Messaging;

namespace ostad_assignment_03.Models
{
    public class BaseMember
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        DataTable dataTable = new DataTable();
        List<BaseMember> listMember = new List<BaseMember>();
        SqlConnection sqlConnection;

        public BaseMember()
        {
            string connString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();
        }

        public List<BaseMember>GetUsers()
        {


            string commandText = "select * from Users";
            SqlCommand cmd = new SqlCommand(commandText, sqlConnection);

            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();

            //Table Data
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //adapter.Fill(dataTable);

            //List
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BaseMember objMember = new BaseMember();

                    objMember.Id = Convert.ToInt32(reader["Id"].ToString());
                    objMember.FirstName = reader["FirstName"].ToString();
                    objMember.LastName = reader["LastName"].ToString();
                    objMember.Role = reader["Role"].ToString();
                    objMember.Gender = reader["Gender"].ToString();
                    objMember.Username = reader["Username"].ToString();

                    listMember.Add(objMember);

                }
            }

            cmd.Dispose();
            sqlConnection.Close();
            return listMember;
        }

        public int SaveUser()
        {
            FirstName = this.FirstName;
            LastName = this.LastName;
            Username = this.Username;
            Gender = this.Gender;
            Password = this.Password;
            Role = this.Role;

            string commandText = "INSERT INTO Users (FirstName, LastName, UserName, Role, Gender, Password) " +
                "VALUES ('"+FirstName+"', '"+ LastName + "', '"+Username+ "','"+Role+"',  '"+ Gender + "', '"+ Password + "')";

            SqlCommand cmd = new SqlCommand(commandText, sqlConnection);

            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();

            sqlConnection.Open();
            int returnValue = cmd.ExecuteNonQuery();

            cmd.Dispose();
            sqlConnection.Close();

            return returnValue;
        }

        public List<BaseMember> ValidateMember(string userName, string password)
        {


            string commandText = "select * from Users";
            SqlCommand cmd = new SqlCommand(commandText, sqlConnection);

            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();

            //Table Data
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //adapter.Fill(dataTable);

            //List
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BaseMember objMember = new BaseMember();

                    objMember.Id = Convert.ToInt32(reader["Id"].ToString());
                    objMember.FirstName = reader["FirstName"].ToString();
                    objMember.LastName = reader["LastName"].ToString();
                    objMember.Username = reader["Password"].ToString();
                    objMember.Password = reader["Password"].ToString();
                    objMember.Role = reader["Role"].ToString();
                    objMember.Gender = reader["Gender"].ToString();

                    listMember.Add(objMember);
                }
            }

            cmd.Dispose();
            sqlConnection.Close();
            return listMember;
        }

        public bool ValidateUser(string username, string password)
        {
            string commandText = "select * from Users where Username='" + username + "' and Password='" + password + "'";
            SqlCommand cmd = new SqlCommand(commandText, sqlConnection);

            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();

            //Table Data
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);



            cmd.Dispose();
            sqlConnection.Close();

            return dataTable.Rows.Count > 0;
        }

        public bool ValidateUsername(string username)
        {
            string commandText = "select * from Users where Username='" + username + "'";
            SqlCommand cmd = new SqlCommand(commandText, sqlConnection);

            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();

            //Table Data
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);

            cmd.Dispose();
            sqlConnection.Close();

            return dataTable.Rows.Count > 0;
        }

        public bool ResetPassword(string username, string password)
        {
            string commandText = "update Users set Password='" + password + "' where Username='" + username + "'";
            SqlCommand cmd = new SqlCommand(commandText, sqlConnection);

            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();

            //Table Data
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);

            cmd.Dispose();
            sqlConnection.Close();

            return dataTable.Rows.Count > 0;
        }
    }
}