using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace libConnection
{
    public class ConnectionPostgres:Icrud
    {
        public string CONECTION = "Server=127.0.0.1; Port=5432; User id=postgres; Password=siqueirosuth19; Database= SVC";
        public NpgsqlConnection con;
        public NpgsqlCommandBuilder cmb;
        public DataSet Ds = new DataSet();
        public NpgsqlDataAdapter Da;
        public NpgsqlCommand comd;
        public NpgsqlDataReader Dr;
        public string Validar;
        public bool cumple = false;

        public void Conexion()//INICIA LA CONEXION
        {
            con = new NpgsqlConnection(CONECTION);
        }
        public ConnectionPostgres()
        {
            Conexion();
        }

        //public void consultar(string query, string tablas)
        //{
        //    Ds.Tables.Clear();
        //    Da = new SqlDataAdapter(query, con);
        //    cmb = new SqlCommandBuilder(Da);
        //    Dr = comd.ExecuteReader();
        //    Da.Fill(Ds, tablas);
        //}
        public bool eliminar(string tablas, string condicion)
        {
            try
            {
                con.Open();
                string query = " DELETE FROM " + tablas + " WHERE " + condicion;
                comd = new NpgsqlCommand(query, con);
                int i = comd.ExecuteNonQuery();
                cumple = true;
                con.Close();
                if (i > 0)
                {
                    return true;
                }else
                {
                    return false;
                }
            }catch(NpgsqlException error)
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
                comd = new NpgsqlCommand(query, con);
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
            }catch(NpgsqlException error)
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
        public bool insertar(string tabla, string campos, string datos)
        {
            try
            {
                con.Open();
                string q = "INSERT INTO " + tabla + "(" + campos + ") VALUES (" + datos + ")";
                comd = new NpgsqlCommand(q, con);
                int k = comd.ExecuteNonQuery();
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
            catch (NpgsqlException error)
            {
                Validar = error.Message;
            }
            return cumple;
            con.Close();
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
                Da = new NpgsqlDataAdapter(q, con);
                cmb = new NpgsqlCommandBuilder(Da);
                Dr = comd.ExecuteReader();
                comd.ExecuteNonQuery();
                cumple = true;
                con.Close();
            }catch(NpgsqlException error)
            {
                Validar = error.Message;
            }
            return cumple;
        }
    }
}

