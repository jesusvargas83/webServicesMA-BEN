using System.Collections.Generic;

namespace core.Util
{
    public static class TipoBeneficio
    {
        private const string SERVYESP = "SERVICIO Y ESPECIE";
        private const string SERV = "SERVICIO";
        private const string ECOYESP = "ECONÓMICO Y ESPECIE";
        private const string OTROS = "OTROS";
        private const string ECO = "ECONÓMICO";
        private const string ESP = "ESPECIE";
        private const string ECOYSERV = "ECONÓMICO Y SERVICIO";

        private static readonly Dictionary<string, string> Tipo = new Dictionary<string, string>()
        {
            { nameof(SERVYESP), SERVYESP },
            { nameof(SERV), SERV },
            { nameof(ECOYESP), ECOYESP },
            { nameof(OTROS), OTROS },
            { nameof(ECO), ECO },
            { nameof(ESP), ESP },
            { nameof(ECOYSERV), ECOYSERV }
        };

        public static Dictionary<string, string> GetTipo()
        {
            return Tipo;
        }
    }
}
