using System.Collections.Generic;

namespace core.Util
{
    public static class CampoDato
    {
        private const string PREFIJOBENEFICIO = "BENEFICIO {0} - ";

        private const string TIPOIDENTIFICACION = "Tipo de identificación";
        private const string NOIDENTIFICACION = "Número de identificación";
        private const string PRIMERNOMBRE = "Primer nombre";
        private const string SEGUNDONOMBRE = "Segundo nombre";
        private const string PRIMERAPELLIDO = "Primer apellido";
        private const string SEGUNDOAPELLIDO = "Segundo apellido";
        private const string CORREO = "Correo";
        private const string TELEFONOCONTACTO = "Teléfono de contacto";

        private const string FECHACORTE = PREFIJOBENEFICIO + "Fecha corte";
        private const string NOMBREENTIDAD = PREFIJOBENEFICIO + "Nombre de entidad";
        private const string PROGRAMA = PREFIJOBENEFICIO + "Programa";
        private const string MARCOEMERGENCIASANITARIA = PREFIJOBENEFICIO + "Marco de emergencia sanitaria";
        private const string AREARESPONSABLE = PREFIJOBENEFICIO + "Área responsable";
        private const string PROYECTOINVERSION = PREFIJOBENEFICIO + "Proyecto de inversión";
        private const string NOCONVENIO = PREFIJOBENEFICIO + "Número de convenio";
        private const string DESCRIPCIONCONVENIO = PREFIJOBENEFICIO + "Descripción de convenio";
        private const string ANIO = PREFIJOBENEFICIO + "Año";
        private const string FECHAINICIO = PREFIJOBENEFICIO + "Fecha de inicio";
        private const string FECHAFIN = PREFIJOBENEFICIO + "Fecha fin";
        private const string NITCONTRATISTA = PREFIJOBENEFICIO + "Nit de contratista";
        private const string RAZONSOCIALCONTRATISTA = PREFIJOBENEFICIO + "Razón social de contratista";
        private const string TIPOBENEFICIO = PREFIJOBENEFICIO + "Tipo de beneficio";
        private const string NOMBREPREDIO = PREFIJOBENEFICIO + "Nombre del predio";
        private const string DEPARTAMENTO = PREFIJOBENEFICIO + "Departamento";
        private const string MUNICIPIO = PREFIJOBENEFICIO + "Municipio";
        private const string VEREDA = PREFIJOBENEFICIO + "Vereda";
        private const string DIRECCION = PREFIJOBENEFICIO + "Dirección";
        private const string PRODUCTO = PREFIJOBENEFICIO + "Producto";
        private const string VALORBENEFICIO = PREFIJOBENEFICIO + "Valor de beneficio";

        private static readonly Dictionary<string, string> InformacionBasica = new Dictionary<string, string>()
        {
            { nameof(TIPOIDENTIFICACION), TIPOIDENTIFICACION },
            { nameof(NOIDENTIFICACION), NOIDENTIFICACION },
            { nameof(PRIMERNOMBRE), PRIMERNOMBRE },
            { nameof(SEGUNDONOMBRE), SEGUNDONOMBRE },
            { nameof(PRIMERAPELLIDO), PRIMERAPELLIDO },
            { nameof(SEGUNDOAPELLIDO), SEGUNDOAPELLIDO },
            { nameof(CORREO), CORREO },
            { nameof(TELEFONOCONTACTO), TELEFONOCONTACTO }
        };

        private static readonly Dictionary<string, string> InformacionBeneficio = new Dictionary<string, string>()
        {
            { nameof(FECHACORTE), FECHACORTE },
            { nameof(NOMBREENTIDAD), NOMBREENTIDAD },
            { nameof(PROGRAMA), PROGRAMA },
            { nameof(MARCOEMERGENCIASANITARIA), MARCOEMERGENCIASANITARIA },
            { nameof(AREARESPONSABLE), AREARESPONSABLE },
            { nameof(PROYECTOINVERSION), PROYECTOINVERSION },
            { nameof(NOCONVENIO), NOCONVENIO },
            { nameof(DESCRIPCIONCONVENIO), DESCRIPCIONCONVENIO },
            { nameof(ANIO), ANIO },
            { nameof(FECHAINICIO), FECHAINICIO },
            { nameof(FECHAFIN), FECHAFIN },
            { nameof(NITCONTRATISTA), NITCONTRATISTA },
            { nameof(RAZONSOCIALCONTRATISTA), RAZONSOCIALCONTRATISTA },
            { nameof(TIPOBENEFICIO), TIPOBENEFICIO },
            { nameof(NOMBREPREDIO), NOMBREPREDIO },
            { nameof(DEPARTAMENTO), DEPARTAMENTO },
            { nameof(MUNICIPIO), MUNICIPIO },
            { nameof(VEREDA), VEREDA },
            { nameof(DIRECCION), DIRECCION },
            { nameof(PRODUCTO), PRODUCTO },
            { nameof(VALORBENEFICIO), VALORBENEFICIO }
        };

        public static Dictionary<string, string> GetInformacionBasica()
        {
            return InformacionBasica;
        }

        public static Dictionary<string,string> GetInformacionBeneficio()
        {
            return InformacionBeneficio;
        }
    }
}
