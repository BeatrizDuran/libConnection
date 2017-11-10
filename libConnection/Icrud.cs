using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libConnection
{
    public interface Icrud
    {
        bool insertar(string query);
        bool eliminar(string tabla, string condicion);
        bool modificar(string tabla, string campos, string condicion);
        bool consultar(string tabla);

    }
}
