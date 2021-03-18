using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class CNProductos
    {
        // Aca realizamos la logica del negocio, validaciones y seguridad. Invocamos los metodos creados en la Capa Datos.
        private DbProductos objectCD = new DbProductos();
        public DataTable MostrarProd()
        {
            DataTable table = new DataTable();
            table = objectCD.Mostrar();
            return table;
        }

        public void InsertarProd(string nombre, string desc, string marca, string precio, string stock)
        {
            objectCD.Insertar(nombre, desc, marca, Convert.ToDouble(precio), Convert.ToInt32(stock));
        }

        public void EditarProd(string nombre, string desc, string marca, string precio, string stock, string id)
        {
            objectCD.Editar(nombre, desc, marca, Convert.ToDouble(precio), Convert.ToInt32(stock), Convert.ToInt32(id));
        }

        public void EliminarProd(string id)
        {
            objectCD.Eliminar(Convert.ToInt32(id));
        }
    }
}
