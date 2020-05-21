using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace Concesionario2020WPF
{
    class DAO_Coches_S
    {
        public SQLiteConnection conSQLite;
        //Initialize values
        public bool Conectar(string srv, string db, string user, string pwd)
        {
            string cadenaConexion = String.Format("Data Source={0};Version=3;FailIfMissing=true;", db);

            try
            {
                conSQLite = new SQLiteConnection(cadenaConexion);
                conSQLite.Open();
                return true;
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Error de conexión: " + ex.Data);
            }
        }

        public void Desconectar()
        {
            try
            {
                if (conSQLite != null)
                    conSQLite.Close();
            }
            catch (SQLiteException)
            {
                throw;
            }
        }

        public bool Conectado()
        {
            if (conSQLite != null)
                return conSQLite.State == System.Data.ConnectionState.Open;
            else
                return false;
        }
        //Select statement
        public List<Coche> Select(string sql)
        {
            List<Coche> listado = new List<Coche>();

            try
            {
                //Create Command
                SQLiteCommand cmd = new SQLiteCommand(sql, conSQLite);
                //Create a data reader and Execute the command
                SQLiteDataReader dataReader = cmd.ExecuteReader();

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
