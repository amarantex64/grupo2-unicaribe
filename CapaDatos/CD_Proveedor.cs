using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Proveedor
    {
        public static readonly Lazy<CD_Proveedor> _instancia = new Lazy<CD_Proveedor>(() => new CD_Proveedor(), true);
        public static CD_Proveedor Instancia => _instancia.Value;

        private CD_Proveedor() { }

        public List<Proveedor> ObtenerProveedor()
        {
            List<Proveedor> rptListaProveedor = new List<Proveedor>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerProveedores", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaProveedor.Add(new Proveedor()
                        {
                            IdProveedor = Convert.ToInt32(dr["IdProveedor"].ToString()),
                            Ruc = dr["Ruc"].ToString(),
                            RazonSocial = dr["RazonSocial"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"].ToString())

                        });
                    }
                    dr.Close();

                    return rptListaProveedor;

                }
                catch
                {
                    rptListaProveedor = null;
                    return rptListaProveedor;
                }
            }
        }

        public bool RegistrarProveedor(Proveedor oProveedor)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarProveedor", oConexion);
                    cmd.Parameters.AddWithValue("Ruc", oProveedor.Ruc);
                    cmd.Parameters.AddWithValue("RazonSocial", oProveedor.RazonSocial);
                    cmd.Parameters.AddWithValue("Telefono", oProveedor.Telefono);
                    cmd.Parameters.AddWithValue("Correo", oProveedor.Correo);
                    cmd.Parameters.AddWithValue("Direccion", oProveedor.Direccion);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        public bool ModificarProveedor(Proveedor oProveedor)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ModificarProveedor", oConexion);
                    cmd.Parameters.AddWithValue("IdProveedor", oProveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("Ruc", oProveedor.Ruc);
                    cmd.Parameters.AddWithValue("RazonSocial", oProveedor.RazonSocial);
                    cmd.Parameters.AddWithValue("Telefono", oProveedor.Telefono);
                    cmd.Parameters.AddWithValue("Correo", oProveedor.Correo);
                    cmd.Parameters.AddWithValue("Direccion", oProveedor.Direccion);
                    cmd.Parameters.AddWithValue("Activo", oProveedor.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

        public bool EliminarProveedor(int IdProveedor)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_EliminarProveedor", oConexion);
                    cmd.Parameters.AddWithValue("IdProveedor", IdProveedor);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

    }
}
