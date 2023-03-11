using System.Security.Claims;

namespace APIPRUEBAS.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public static dynamic validarToken(ClaimsIdentity identity)
        {
            try
            {
                if(identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Verificar si estas enviando un token valido",
                        result = ""
                    };
                }

                return new
                {
                    success = true,
                    message = "exito"
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success= false,
                    message = "Catch: "+ex.Message,
                    result = ""
                };
            }
        }
    }
}
