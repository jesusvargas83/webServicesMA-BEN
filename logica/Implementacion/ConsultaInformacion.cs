using core.modelo;
using core.Modelo.ConsultaInformacion;
using core.Util;
using datos.Interfaz;
using logica.Interfaz;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace logica.Implementacion
{
    /// <summary>
    /// Clase que realiza implementación de lógica de consulta de información
    /// </summary>
    public class ConsultaInformacion : IConsultaInformacion
    {
        private const string ERRORMESSAGE = "Error durante ejecución de consultas y creación de respuesta.";
        private const string TEXTOFECHA = "Fecha";

        private readonly ILogger<ConsultaInformacion> logger;
        private readonly IConsultaInformacionRepository ConsultaInformacionRepository;

        /// <summary>
        /// Constructor de la clase que recibe la inyección de dependencias
        /// </summary>
        /// <param name="consultaInformacionRepository">Objeto para realizar consultas SQL necesarias a base de datos</param>
        /// <param name="logger">Objeto para realizar el logging</param>
        public ConsultaInformacion(IConsultaInformacionRepository consultaInformacionRepository, ILogger<ConsultaInformacion> logger)
        {
            this.ConsultaInformacionRepository = consultaInformacionRepository;
            this.logger = logger;
        }

        public async Task<Respuesta> ConsultaInformacionServicio(Peticion peticion)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                Persona persona = await ConsultaInformacionRepository.DatosBasicosPersona(peticion);
                if (persona != null)
                {
                    SetDatosBasicosPersona(respuesta, persona);
                    List<Beneficio> beneficios = (List<Beneficio>)await ConsultaInformacionRepository.DatosBeneficios(peticion);
                    SetDatosBeneficios(respuesta, beneficios);
                }
            }
            catch (Exception exception)
            {
                logger.LogError(ERRORMESSAGE, exception);
                throw;
            }

            return respuesta;
        }

        /// <summary>
        /// Método para organizar la información de los beneficios en la estructura definida
        /// </summary>
        /// <param name="respuesta">Objeto en el cual se almacena la información a retornar</param>
        /// <param name="beneficios">Objeto con la información de los beneficios</param>
        private static void SetDatosBeneficios(Respuesta respuesta, List<Beneficio> beneficios)
        {
            int contadorBeneficio = 0;
            DatoConsultado datoConsultado;
            var propiedadesBeneficio = typeof(Beneficio).GetProperties();

            foreach (var beneficio in beneficios)
            {
                contadorBeneficio++;

                foreach (var propiedad in propiedadesBeneficio.Where(x => CampoDato.GetInformacionBeneficio().ContainsKey(x.Name.ToUpperInvariant())))
                {
                    RealizarConversiones(beneficio, propiedad);

                    datoConsultado = new DatoConsultado
                    {
                        CampoDato = string.Format(CampoDato.GetInformacionBeneficio()[propiedad.Name.ToUpperInvariant()], contadorBeneficio),
                        ValorDato = (string)(propiedad.GetValue(beneficio) ?? string.Empty)
                    };
                    respuesta.DatoConsultado.Add(datoConsultado);
                }
            }
        }

        /// <summary>
        /// Métoco para realizar conversiones a la información de los beneficios
        /// </summary>
        /// <param name="beneficio">Objeto de beneficio</param>
        /// <param name="propiedad">Propiedad del objeto de beneficio</param>
        private static void RealizarConversiones(Beneficio beneficio, System.Reflection.PropertyInfo propiedad)
        {
            var nombrePropiedad = propiedad.Name;
            // Se pregunta si la propiedad es alguna de las fechas para realizar conversión al formato requerido
            if (nombrePropiedad.StartsWith(TEXTOFECHA))
            {
                propiedad.SetValue(beneficio, propiedad.GetValue(beneficio) != null ? Fecha.FechaAnioMesDia((string)propiedad.GetValue(beneficio)) : string.Empty);
            }
            // Se pregunta si la propiedad es el tipo de beneficio para hacer la conversión necesaria
            else if (nombrePropiedad == nameof(beneficio.TipoBeneficio) && propiedad.GetValue(beneficio) != null)
            {
                propiedad.SetValue(beneficio, TipoBeneficio.GetTipo().ContainsKey((string)propiedad.GetValue(beneficio)) ? TipoBeneficio.GetTipo()[(string)propiedad.GetValue(beneficio)] : string.Empty);
            }
        }

        /// <summary>
        /// Método para organizar la información de la persona en la estructura definida
        /// </summary>
        /// <param name="respuesta">Objeto en el cual se almacena la información a retornar</param>
        /// <param name="persona">Objeto con la información de la persona</param>
        private static void SetDatosBasicosPersona(Respuesta respuesta, Persona persona)
        {
            DatoConsultado datoConsultado;
            var propiedadesPersona = persona.GetType().GetProperties();

            foreach (var propiedad in propiedadesPersona.Where(x => CampoDato.GetInformacionBasica().ContainsKey(x.Name.ToUpperInvariant())))
            {
                datoConsultado = new DatoConsultado
                {
                    CampoDato = CampoDato.GetInformacionBasica()[propiedad.Name.ToUpperInvariant()],
                    ValorDato = (string)(propiedad.GetValue(persona) ?? string.Empty)
                };
                respuesta.DatoConsultado.Add(datoConsultado);
            }
        }
    }
}
