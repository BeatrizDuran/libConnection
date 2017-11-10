using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace libConnection
{
    public class ConnectionSQLServer:Icrud
    {
        public string CONECTION = ("Data Source=BEATRIZDURAN-PC;Integrated Security=SSPI;Initial Catalog=SVC");
        public SqlConnection con;
        public SqlCommandBuilder cmb;
        public DataSet Ds = new DataSet();
        public SqlDataAdapter Da;
        public SqlCommand comd;
        public SqlDataReader Dr;
        public DataRow Drow;
        public bool cumple = false;
        public string Validar;


        public void Conexion()//INICIA LA CONEXION
        {
            con = new SqlConnection(CONECTION);
        }
        public ConnectionSQLServer()
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
                Da = new SqlDataAdapter(q, con);
                cmb = new SqlCommandBuilder(Da);
                Dr = comd.ExecuteReader();
                comd.ExecuteNonQuery();
                cumple = true;
                con.Close();
            }catch(SqlException error)
            {
                Validar = error.Message;
            }
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
                string query = "DELETE FROM " + tabla + " WHERE " + condicion;
                comd = new SqlCommand(query, con);
                int i = comd.ExecuteNonQuery();
                cumple = true;
                con.Close();
            }
            catch (SqlException error)
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
        public bool modificar(string tabla, string campos, string condicion)
        {
            try {
                con.Open();
                string query = "UPDATE " + tabla + " SET " + campos + " WHERE " + condicion;
                comd = new SqlCommand(query, con);
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
            } catch (SqlException error)
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
                //string q = "INSERT INTO " + tabla + "(" + campos + ") VALUES (" + datos + ");";
                comd = new SqlCommand(query, con);
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
            catch (SqlException error)
            {
                Validar = error.Message;
            }
            return cumple;
        }

    }
}
