using core.modelo;
using core.Modelo.ConsultaInformacion;
using datos.interfaz;
using datos.Interfaz;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace datos.Implementacion
{
    /// <summary>
    /// Clase que realiza la implementación de las consultas SQL necesarias a base de datos
    /// </summary>
    public class ConsultaInformacionRepository : IConsultaInformacionRepository
    {
        private const string ERRORMESSAGEBASICDATA = "Error durante ejecución de consulta de información básica.";
        private const string ERRORMESSAGEBENEFITDATA = "Error durante ejecución de consulta de información de beneficios.";
        private const string FILTROESTADO = "AC";
        private const int LIMITEDATOSBASICOS = 1;

        private readonly ILogger<ConsultaInformacionRepository> logger;
        private readonly IDapperConnector DapperConnector;

        /// <summary>
        /// Constructor de la clase que recibe la inyección de dependencias
        /// </summary>
        /// <param name="dapperConnector">Objeto para realizar el llamado a base de datos y ejecución de consultas SQL</param>
        /// <param name="logger">Objeto para realizar el logging</param>
        public ConsultaInformacionRepository(IDapperConnector dapperConnector, ILogger<ConsultaInformacionRepository> logger)
        {
            this.DapperConnector = dapperConnector;
            this.logger = logger;
        }

        public async Task<Persona> DatosBasicosPersona(Peticion peticion)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select upper(td.\"Nombre\") as TipoIdentificacion, b.\"NoIdentificacion\", upper(b.\"PrimerNombre\") as PrimerNombre, upper(b.\"SegundoNombre\") as SegundoNombre, ");
            sql.Append("upper(b.\"PrimerApellido\") as PrimerApellido, upper(b.\"SegundoApellido\") as SegundoApellido, upper(b.\"Correo\") as Correo, b.\"TelefonoContacto\" ");
            sql.Append("from \"CARGUEMASIVO\".\"Beneficiario\" b ");
            sql.Append("inner join \"PRODUCTORES\".\"TipoDocumentos\" td on td.\"Sigla\" = b.\"TipoIdentificacion\" ");
            sql.AppendFormat("where b.\"TipoIdentificacion\" = '{0}' ", peticion.TipoId);
            sql.AppendFormat("and b.\"NoIdentificacion\" = '{0}' ", peticion.IdUsuario);
            sql.AppendFormat("and b.\"Estado\" = '{0}' ", FILTROESTADO);
            sql.Append("order by b.\"FechaCorte\" desc ");
            sql.AppendFormat("limit {0};", LIMITEDATOSBASICOS);

            try
            {
                Persona persona = await DapperConnector.QuerySingleOrDefaultAsync<Persona>(sql.ToString(), commandType: System.Data.CommandType.Text);

                return persona;
            }
            catch (Exception exception)
            {
                logger.LogError(ERRORMESSAGEBASICDATA, exception);
                throw;
            }
        }

        public async Task<IReadOnlyList<Beneficio>> DatosBeneficios(Peticion peticion)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select distinct ");
            sql.Append("b.\"FechaCorte\", upper(e.\"Nombre\") as NombreEntidad, upper(b.\"Programa\") as Programa, case when b.\"MarcoEmergenciaSanitaria\" is true then 'SÍ' when b.\"MarcoEmergenciaSanitaria\" is false then 'NO' end as MarcoEmergenciaSanitaria, ");
            sql.Append("upper(ar.\"Nombre\") as AreaResponsable, upper(b.\"ProyectoInversion\") as ProyectoInversion, b.\"NoConvenio\", upper(b.\"DescripcionConvenio\") as DescripcionConvenio, b.\"Anio\", b.\"FechaInicio\", b.\"FechaFin\", b.\"NitContratista\", ");
            sql.Append("upper(b.\"RazonSocialContratista\") as RazonSocialContratista, upper(b.\"TipoBeneficio\") as TipoBeneficio, upper(b.\"NombrePredio\") as NombrePredio, upper(d.\"Nombre\") as Departamento, upper(m.\"Nombre\") as Municipio, ");
            sql.Append("upper(b.\"Vereda\") as Vereda, upper(b.\"Direccion\") as Direccion, upper(p.\"Propiedad\") as Producto, b.\"ValorBeneficio\" ");
            sql.Append("from \"CARGUEMASIVO\".\"Beneficiario\" b ");
            sql.Append("inner join \"PRODUCTORES\".\"Entidades\" e on e.\"Codigo\" = b.\"NombreEntidad\" ");
            sql.Append("left join \"CARGUEMASIVO\".\"AreaResponsable\" ar on ar.\"Sigla\" = b.\"AreaResponsable\" ");
            sql.Append("left join \"PRODUCTORES\".\"Departamentos\" d on d.\"Codigo\" = b.\"Departamento\" ");
            sql.Append("left join \"PRODUCTORES\".\"Municipios\" m on m.\"Codigo\" = b.\"Municipio\" ");
            sql.Append("left join \"PRODUCTORES\".\"Productos\" p on p.\"Codigo\" = b.\"Producto\" ");
            sql.AppendFormat("where b.\"TipoIdentificacion\" = '{0}' ", peticion.TipoId);
            sql.AppendFormat("and b.\"NoIdentificacion\" = '{0}' ", peticion.IdUsuario);
            sql.AppendFormat("and b.\"Estado\" = '{0}' ", FILTROESTADO);
            sql.Append("order by b.\"Anio\" desc;");

            try
            {
                List<Beneficio> beneficios = (List<Beneficio>) await DapperConnector.QueryAsync<Beneficio>(sql.ToString(), commandType: System.Data.CommandType.Text);

                return beneficios;
            }
            catch (Exception exception)
            {
                logger.LogError(ERRORMESSAGEBENEFITDATA, exception);
                throw;
            }
        }
    }
}
