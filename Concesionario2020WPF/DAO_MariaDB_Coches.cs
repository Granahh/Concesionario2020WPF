using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Concesionario2020WPF
{
    class DAO_Coches_M
    {
        public MySqlConnection conMariaDb;
        //Initialize values
        public void Conectar(string srv, string db, string user, string pwd)
        {

            string conMariaDbString;
            conMariaDbString = String.Format("server={0};database={1};uid={2};pwd={3}", srv, db, user, pwd);
            try
            {
                conMariaDb = new MySqlConnection(conMariaDbString);
                conMariaDb.Open();
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        throw new Exception("Cannot connect to server.  Contact administrator");

                    case 1045:
                        throw new Exception("Invalid username/password, please try again");

                    default:
                        throw;
                }
            }
        }

        //Close conMariaDb
        public void Desconectar()
        {
            try
            {
                if (conMariaDb != null)
                    conMariaDb.Close();
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        //Select statement
        public List<Coche> Select(string sql)
        {
            List<Coche> listado = new List<Coche>();

            try
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sql, conMariaDb);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    Coche unCoche = new Coche();
                    unCoche.codcoche = dataReader["codcoche"].ToString();
                    unCoche.cifm = dataReader["cifm"].ToString();
                    unCoche.nombre = dataReader["nombre"].ToString();
                    unCoche.modelo = dataReader["modelo"].ToString();
                    listado.Add(unCoche);
                }
                dataReader.Close();
                return listado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //stored procedure statement
        public List<Coche> Rutina(int num)
        {
            List<Coche> listado = new List<Coche>();

            try
            {
                //Create Command
                string sql = "call spCoches(" + num + ");";
                MySqlCommand cmd = new MySqlCommand(sql, conMariaDb);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    Coche unCoche = new Coche();
                    unCoche.codcoche = dataReader["codcoche"].ToString();
                    unCoche.cifm = dataReader["cifm"].ToString();
                    unCoche.nombre = dataReader["nombre"].ToString();
                    unCoche.modelo = dataReader["modelo"].ToString();
                    listado.Add(unCoche);
                }
                dataReader.Close();
                return listado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
