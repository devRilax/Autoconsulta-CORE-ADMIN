using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Totem.VO;

namespace Totem.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IAlumnoWs" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IAlumnoWs
    {
        [OperationContract]
        string obtenerReporteAlumno(string run);

        [OperationContract]
        string validaLoginAlumno(string username, string password);

        [OperationContract]
        string cambiarClaveUsuario(string username, string password);

        [OperationContract]
        RespuestaReporte obtenerReporteAlumnoWs(string run);
    }
}
