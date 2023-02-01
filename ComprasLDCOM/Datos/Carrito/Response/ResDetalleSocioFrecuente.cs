using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComprasLDCOM.Datos.Carrito.Response
{
    public class ResDetalleSocioFrecuente
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        public string Data { get; set; }
        public List<DetalleSocioFrecuenteDetalle> DataL { get; set; }
    }

    public class DetalleSocioFrecuenteDetalle
    {
        public string Codigo_Activacion { get; set; }
        public string Codigo_Postal { get; set; }
        public bool Comisionable { get; set; }
        public string Correo { get; set; }
        public string Dias_No_Compra { get; set; }
        public string Edad1 { get; set; }
        public string Edad2 { get; set; }
        public string Fec_Actualizacion { get; set; }
        public string Fecha_Nacimiento { get; set; }
        public int Fecha_Nacimiento_Ano { get; set; }
        public int Fecha_Nacimiento_Dia { get; set; }
        public int Fecha_Nacimiento_Mes { get; set; }
        public string Filtro1_Nombre { get; set; }
        public string Filtro2_Nombre { get; set; }
        public string Filtro3_Nombre { get; set; }
        public string Filtro4_Nombre { get; set; }
        public string Filtro5_Nombre { get; set; }
        public string Filtro_1 { get; set; }
        public string Filtro_2 { get; set; }
        public string Filtro_3 { get; set; }
        public string Filtro_4 { get; set; }
        public string Filtro_5 { get; set; }
        public string Hash { get; set; }
        public int Id { get; set; }
        public string Llave { get; set; }
        public string Mensaje { get; set; }
        public string MonederoTarj_Id { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public bool Promocion { get; set; }
        public decimal Saldo { get; set; }
        public int Socio_Dia_Estado_Cuenta { get; set; }
        public string Socio_Estado { get; set; }
        public string Socio_NumCelular { get; set; }
        public string Socio_RedSocial { get; set; }
        public string Socio_RedSocial_Id { get; set; }
        public string Socio_Ult_Suc_Compra { get; set; }
        public string Telefono { get; set; }
        public string Telefono_FIjo { get; set; }
        public int Vendedor_Afiliado_Id { get; set; }
        public string Vendedor_Afiliado_Nombre { get; set; }



        public DetalleSocioFrecuenteDetalle(string codigo_Activacion, string codigo_Postal, bool comisionable, string correo, string dias_No_Compra, string edad1, string edad2, string fec_Actualizacion, string fecha_Nacimiento, int fecha_Nacimiento_Ano, int fecha_Nacimiento_Dia, int fecha_Nacimiento_Mes, string filtro1_Nombre, string filtro2_Nombre, string filtro3_Nombre, string filtro4_Nombre, string filtro5_Nombre, string filtro_1, string filtro_2, string filtro_3, string filtro_4, string filtro_5, string hash, int id, string llave, string mensaje, string monederoTarj_Id, string nombre, string password, bool promocion, decimal saldo, int socio_Dia_Estado_Cuenta, string socio_Estado, string socio_NumCelular, string socio_RedSocial, string socio_RedSocial_Id, string socio_Ult_Suc_Compra, string telefono, string telefono_FIjo, int vendedor_Afiliado_Id, string vendedor_Afiliado_Nombre)
        {
            Codigo_Activacion = codigo_Activacion;
            Codigo_Postal = codigo_Postal;
            Comisionable = comisionable;
            Correo = correo;
            Dias_No_Compra = dias_No_Compra;
            Edad1 = edad1;
            Edad2 = edad2;
            Fec_Actualizacion = fec_Actualizacion;
            Fecha_Nacimiento = fecha_Nacimiento;
            Fecha_Nacimiento_Ano = fecha_Nacimiento_Ano;
            Fecha_Nacimiento_Dia = fecha_Nacimiento_Dia;
            Fecha_Nacimiento_Mes = fecha_Nacimiento_Mes;
            Filtro1_Nombre = filtro1_Nombre;
            Filtro2_Nombre = filtro2_Nombre;
            Filtro3_Nombre = filtro3_Nombre;
            Filtro4_Nombre = filtro4_Nombre;
            Filtro5_Nombre = filtro5_Nombre;
            Filtro_1 = filtro_1;
            Filtro_2 = filtro_2;
            Filtro_3 = filtro_3;
            Filtro_4 = filtro_4;
            Filtro_5 = filtro_5;
            Hash = hash;
            Id = id;
            Llave = llave;
            Mensaje = mensaje;
            MonederoTarj_Id = monederoTarj_Id;
            Nombre = nombre;
            Password = password;
            Promocion = promocion;
            Saldo = saldo;
            Socio_Dia_Estado_Cuenta = socio_Dia_Estado_Cuenta;
            Socio_Estado = socio_Estado;
            Socio_NumCelular = socio_NumCelular;
            Socio_RedSocial = socio_RedSocial;
            Socio_RedSocial_Id = socio_RedSocial_Id;
            Socio_Ult_Suc_Compra = socio_Ult_Suc_Compra;
            Telefono = telefono;
            Telefono_FIjo = telefono_FIjo;
            Vendedor_Afiliado_Id = vendedor_Afiliado_Id;
            Vendedor_Afiliado_Nombre = vendedor_Afiliado_Nombre;
        }
    }
}

