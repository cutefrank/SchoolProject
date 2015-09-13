using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data;
using System.IO;
using System.Reflection;

namespace Session
{
    public class Broker
    {
        OleDbConnection connection;
        OleDbCommand command;

        private void ConnectTo()
        {
            //string str = System.Environment.CurrentDirectory; 
            //string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Database.accdb");
            //connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + str + "\\Database.accdb");
            //connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=MapPath(""Database.accdb"")");
            //connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\user\Documents\Visual Studio 2013\Projects\SchoolProject\Database.accdb");
            command = connection.CreateCommand();
        }
      
        public Broker()
        {
            ConnectTo();
        }

        public void Insert(Person p)
        {
            try
            {
                command.CommandText = "INSERT INTO TPersons (FirstName,LastName) VALUES('" + p.FirstName + "', '" + p.LastName+"')";
                command.CommandType = CommandType.Text;
                connection.Open();

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                if(connection != null)
                {
                    connection.Close();
                }
            }
        }

        public List<Person> FillComboBox()
        {
            List<Person> personsList = new List<Person>();
            try
            {
                command.CommandText = "SELECT * FROM TPersons";
                command.CommandType = CommandType.Text;
                connection.Open();

                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Person p = new Person();

                    p.Id = Convert.ToInt32(reader["ID"].ToString());
                    p.FirstName = reader["FirstName"].ToString();
                    p.LastName = reader["LastName"].ToString();

                    personsList.Add(p);
                }
                return personsList;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public void Update(Person oldPerson,Person newPerson)
        {
            try
            {                     
                command.CommandText = "UPDATE TPersons  SET FirstName= '" + newPerson.FirstName + "',LastName = '" + newPerson.LastName +"' WHERE ID=" + oldPerson.Id ;
                command.CommandType = CommandType.Text;
                connection.Open();

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public void Delete(Person p)
        {
            try
            {
                command.CommandText = "DELETE FROM TPersons WHERE ID= " + p.Id;
                command.CommandType = CommandType.Text;
                connection.Open();

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
    }
}
