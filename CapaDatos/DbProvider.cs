using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DbProvider
    {
        private Entity Entity;
        private SqlConnection connection;
        private SqlConnection GetSqlConnection()
        {
            if (connection == null || connection.State == ConnectionState.Closed)
            {
                connection = new SqlConnection("Server=DESKTOP-HI4MRD8\\SQLEXPRESS; Database=PracticaCRUDNegocio; Integrated security=True;");
                connection.Open();
            }
            return connection;
        }
        private SqlCommand CommandBuilder(DbActionType type)
        {
            SqlCommand command = new SqlCommand(type.ToString(), GetSqlConnection())
            {
                CommandType = CommandType.StoredProcedure
            };

            if (Entity == null && (type == DbActionType.EditarProductos || type == DbActionType.InsertarProductos || type == DbActionType.EliminarProducto))
            {
                throw new ArgumentNullException();
            }

            if (type == DbActionType.EditarProductos || type == DbActionType.InsertarProductos)
            {
                command.Parameters.AddWithValue("@nombre", Entity.Name);
                command.Parameters.AddWithValue("@descrip", Entity.Descripcion);
                command.Parameters.AddWithValue("@marca", Entity.Marca);
                command.Parameters.AddWithValue("@precio", Entity.Precio);
                command.Parameters.AddWithValue("@stock", Entity.Stock);
            }

            if (type == DbActionType.EditarProductos || type == DbActionType.EliminarProducto)
            {
                command.Parameters.AddWithValue("@stock", Entity.Id);
            }

            return command;
        }
        public int ExecuteCommand(Entity entity, DbActionType type)
        {
            Entity = entity;
            try
            {
                return CommandBuilder(type).ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable GetAllEntities()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(CommandBuilder(DbActionType.MostrarProductos).ExecuteReader());
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
