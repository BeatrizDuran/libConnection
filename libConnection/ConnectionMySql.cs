using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;

namespace libConnection
{
    public class ConnectionMySql:Icrud
    {
       public bool cumple = false;
        public static string Validar;
        public static string CONECTION = "host=127.0.0.1;uid=root;pwd=siqueirosuth19;database=SVC";
            public MySqlConnection con;      
            public MySqlCommandBuilder cmb;
            public DataSet Ds = new DataSet();
            public MySqlDataAdapter Da;
            public MySqlCommand comd;
            public MySqlDataReader Dr;


        public void Conexion()//INICIA LA CONEXION
        {
            con = new MySqlConnection(CONECTION);
        }
        public ConnectionMySql()
        {
            Conexion();
        }
        /// <summary>
        /// realiza la consulta de la tabla
        /// </summary>
        /// <param name="tabla">nombre de la tabla</param>
        /// <returns></returns>
        public bool consultar(string tabla)
        {
            try
            {
                con.Open();
                Ds.Tables.Clear();
                string q = "SELECT * FROM " + tabla;
                Da = new MySqlDataAdapter(q, con);
                cmb = new MySqlCommandBuilder(Da);
                Dr = comd.ExecuteReader();
                comd.ExecuteNonQuery();
                cumple = true;
            }
            catch (MySqlException error)
            {
                Validar = error.Message;
            }

            con.Close();
            return cumple;

        }
        /// <summary>
        /// Elimina la los datos que le solictastes en la condicion
        /// </summary>
        /// <param name="tabla">nombre de la tabla</param>
        /// <param name="condicion">nombre de la condicion</param>
        /// <returns></returns>
        public bool eliminar(string tabla, string condicion)
        {
            try
            {
                con.Open();
                string query = " DELETE FROM " + tabla + " WHERE " + condicion;
                comd = new MySqlCommand(query, con);
                int i = comd.ExecuteNonQuery();
                cumple = true;
                con.Close();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException error)
            {
                Validar = error.Message;
            }
            return cumple;
            }
        /// <summary>
        /// Modificacion de registros 
        /// </summary>
        /// <param name="tablas">nombre de la tabla</param>
        /// <param name="campos">campos los cuales se quieren eliminar</param>
        /// <param name="condicion">que registro desea eliminar</param>
        /// <returns></returns>
        public bool modificar(string tablas, string campos, string condicion)
            {
            try
            {
                con.Open();
                string query = "UPDATE " + tablas + " SET " + campos + " WHERE " + condicion;
                comd = new MySqlCommand(query, con);
                int j = comd.ExecuteNonQuery();
                cumple = true;
                con.Close();
                if (j > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException error)
            {
                Validar = error.Message;
            }
            return cumple;
            }
        /// <summary>
        /// agrega registro
        /// </summary>
        /// <param name="tabla">nombre de la tabla</param>
        /// <param name="campos">campos los cuales se desean agregar</param>
        /// <param name="datos">datos lo cuales quieres agregar</param>
        /// <returns></returns>
        public bool insertar(string query)
        {
            try
            {
                //string q = "INSERT INTO " + tabla + "(" + campos + ") VALUES (" + datos + ")";
                comd = new MySqlCommand(query, con);
                con.Open();
                int k = comd.ExecuteNonQuery();
                con.Close();
                cumple = true;
                if (k > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException error)
            {
                Validar = error.Message;
            }
            return cumple;
            }
        }
    }

