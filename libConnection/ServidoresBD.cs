using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace libConnection
{
    public class ServidoresBD
    {
        Icrud mysql = new ConnectionMySql();
        Icrud pg = new ConnectionPostgres();
        Icrud sql = new ConnectionSQLServer();
        public string campos, tablas, valores;
        public bool estado = true;
        public object obj = new object();
        public void TimeStamp(string time)
        {
            Monitor.Enter(obj);
            using (StreamWriter sw = new StreamWriter(@"C:\HilosBaseDeDatos.txt"))
            {
                sw.WriteLine(time);
            }
            Monitor.Exit(obj);
        }
        public void insertarMysql()
        {
            mysql.insertar(valores);
            TimeStamp("Fin del hilo Mysql insertar: "+DateTime.Now.ToLocalTime().ToString());
        }
        public void insertarPG()
        {
            pg.insertar(valores);
            TimeStamp("Fin del hilo Postgres insertar: " + DateTime.Now.ToLocalTime().ToString());
        }
        public void insertarSql()
        {
            sql.insertar(valores);
            TimeStamp("Fin del hilo SQLServer insertar: " + DateTime.Now.ToLocalTime().ToString());
        }
        public void eliminarMysql()
        {
            mysql.eliminar(tablas, campos);
        }
        public void eliminarPG()
        {
            pg.eliminar(tablas, campos);
        }
        public void eliminarSql()
        {
            sql.eliminar(tablas, campos);
        }
        public bool insertarBDS(string valores)
        {
            this.valores = valores;
            Thread mysql = new Thread(new ThreadStart(insertarMysql));
            TimeStamp("Inicio del hilo Mysql insertar: "+DateTime.Now.ToLocalTime().ToString());
            mysql.Start();
            Thread sql = new Thread(new ThreadStart(insertarSql));
            TimeStamp("Inicio del hilo SQL insertar: " + DateTime.Now.ToLocalTime().ToString());
            sql.Start();
            Thread postgres = new Thread(new ThreadStart(insertarPG));
            TimeStamp("Inicio del hilo Postgres insertar: " + DateTime.Now.ToLocalTime().ToString());
            postgres.Start();
            return estado;

        }

    }
}
