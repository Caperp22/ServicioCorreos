using System;
using System.Xml;
using DataExpress.RecepcionCorreo.Aplicacion.DTO;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Builder
{
    public class ComprobanteJson : IComprobanteJson
    {
        private JsonContext _Json;
        private EmisorDto _emisor;
        private AceptanteDto _aceptante;
        private string _tipo;
        private long _tipoPersonaProveedor;
        private string _factor;
        private string _idComprobante;
        private AttachedDocumentType _attached;
        private XmlDocument _xml;
        private string _serie;
        private ConfiguracionDto _configuracion;
        public ComprobanteJson(ConfiguracionDto configuracion,DatosGeneralDto datos, string idComprobante)
        {
            _aceptante = datos.Aceptante;
            _emisor = datos.Emisor;
            _tipo = datos.Tipo;
            _tipoPersonaProveedor = datos.TipoPersonaProveedor;
            _factor = datos.Factor;
            _attached = datos.Attached;
            _xml = datos.Xml;
            _serie = datos.Serie;
            _idComprobante = idComprobante;
            _configuracion = configuracion;
        }

        public void CUDE()
        {
            
            _Json.comprobante.FechaEmision = (Localization.Now).ToString("yyyy/MM/dd HH:mm:ss");
            _Json.comprobante.NumeroModificado = _attached.ID.Value;
            var CufeReferenciado = _attached.ParentDocumentLineReference[0].DocumentReference.UUID.Value;
            _Json.comprobante.CufeReferenciado = CufeReferenciado;
            var doc = _attached.ProfileID.Value.ToString();
            _Json.comprobante.TipoDocReferenciado = doc.Contains("Factura") ? "01" : doc.Contains("DIAN 2.1") ? "01" : doc.Contains("Crédito") ? "07" : "05";
            _Json.comprobante.Identificacion = _emisor.NIT;
            _Json.comprobante.RazonSocial = _emisor.RazonSocial;
            _Json.comprobante.ArrayInfoAdicional.Add(new[] { "", "tipoPersonaEmi", _emisor.TipoPersona.ToString() });
            _Json.comprobante.TipoIdentificacion = "31";
        }

        public void DatosGenerales()
        {
            var fecha = _attached.IssueDate.Value;
            var dueDate = _xml.GetElementsByTagName("cbc:DueDate");
            _Json.comprobante.VencimientoReferenciado = dueDate != null && dueDate.Count > 0
                ? dueDate[0].InnerText : fecha.ToString();
            _Json.comprobante.IdComprobante = Int32.Parse(_idComprobante);
            _Json.comprobante.CodigoDocumento = "06";
            _Json.comprobante.Issuer = _aceptante.Nombre;
            _Json.comprobante.ArrayInfoAdicional.Add(new[] { "", "FamilyName", _aceptante.Apellidos });
            _Json.comprobante.TipoDocIssuer = _aceptante.TipoPersona.ToString();
            _Json.comprobante.NitIssuer = _aceptante.Identificacion;
            _Json.comprobante.DVIssuer = _aceptante.DV;
            _Json.comprobante.ArrayInfoAdicional.Add(new[] { "", "JobTitle", _aceptante.Cargo });
            _Json.comprobante.ArrayInfoAdicional.Add(new[] { "", "OrganizationDepartment", _aceptante.Area });
            _Json.comprobante.Secuencial = "0";
            _Json.comprobante.Serie = _serie;
        }

        public void Eventos()
        {
            _Json.comprobante.Evento = _tipo;
            _Json.comprobante.CondicionEntrega = _tipo == "030" ? "ACUSE" : _tipo == "031" ? "RECHAZADO" : _tipo == "032" ? "RECIBO" : "ACEPTADO";
            if (_Json.comprobante.Evento.Equals("031"))
                _Json.comprobante.Factor = _emisor.Factor;

            if (_Json.comprobante.Evento.Equals("034"))
            {
                _Json.comprobante.CondicionEntrega = "ACEPTADO";
                _Json.comprobante.RazonSocialReceptor = "Dirección de Impuestos y Aduanas Nacionales";
                _Json.comprobante.IdentificacionReceptor = "800197268";
                _Json.comprobante.TipoIdentificacionReceptor = "31";
                _Json.comprobante.tipoPersonaRece = "1";
                _Json.comprobante.Nota1 = ($@"Manifiesto bajo la gravedad de juramento que transcurridos 3 días hábiles contados desde la creación del Recibo de bienes y servicios {_Json.comprobante.DocReference} con CUDE {_Json.comprobante.CufeRef}, el adquirente {_Json.comprobante.RazonSocial} identificado con NIT {_Json.comprobante.Identificacion} no manifestó expresamente la aceptación o rechazo de la referida factura, ni reclamó en contra de su contenido");
                _Json.comprobante.Email = "";
            }
        }


        public void Receptor()
        {
            _Json.comprobante.IdentificacionReceptor = _attached.SenderParty.PartyTaxScheme[0].CompanyID.Value.ToString();
            _Json.comprobante.RazonSocialReceptor = _attached.SenderParty.PartyTaxScheme[0].RegistrationName.Value.ToString();
            _Json.comprobante.TipoIdentificacionReceptor = _attached.SenderParty.PartyTaxScheme[0].CompanyID.schemeName;
            _Json.comprobante.ArrayInfoAdicional.Add(new[] { "", "tipoPersonaRece", _tipoPersonaProveedor.ToString() });
            _Json.comprobante.PaisEmisor = "CO";
        }

        public void Reiniciar(string Identificacion) {
            _Json = new JsonContext();
            _Json.comprobante.DireccionRDLC = _configuracion.RutaRdlc;
            _Json.comprobante.NombreRDLC = _configuracion.NombreRdlc;

        }


        public JsonContext Context() => _Json;
    }
}
