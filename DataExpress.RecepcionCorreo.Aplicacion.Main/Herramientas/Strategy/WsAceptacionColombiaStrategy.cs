using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using DataExpress.RecepcionCorreo.Aplicacion.Interface;
using DataExpress.RecepcionCorreo.Infraestructura.Interface;
using System;
using System.Collections.Generic;
using static WsColombia.XdServiceSoapClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using WsColombia;
using DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Builder;
using System.Xml.Schema;
using WSRadian;
using System.Text.RegularExpressions;
using DataExpress.RecepcionCorreo.Infraestructura.Repository;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Strategy
{
    public class WsAceptacionColombiaStrategy : IWsAceptacion
    {
        private IAcuseRepository _acuseRepository;
        private IEmisorRepository _emisorRepository;
        private List<DocumentosDto> _ltsDocumentos;
        private ConfiguracionDto _optionsConfig;
        private SmtpConfigurationDTo _smtpConfiguration;
        private AceptantesConfig _aceptantes;
        private AceptanteDto _aceptante;
        private byte[] _byteXml;
        private byte[] _bytePdf;
        private string _nombrePdf;
        private string _nomreXml;
        private string _xDoc;
        private AttachedDocumentType _oXml;
        private PartyTaxSchemeType _oPartyTaxScheme;
        private string _proveedor;
        private DatosGeneralDto _general;
        private DocumentReferenceType _documentoRef;
        private string _idComprobante;
        private string _JsonResult;
        private XmlReaderSettings _xsdSettings;
        private XmlReader _xmlReader;
        private string _mensajeError;
        private string _mensaje;
        private bool _evento;

        public WsAceptacionColombiaStrategy(IAcuseRepository acuseRepository, IEmisorRepository emisorRepository, List<DocumentosDto> ltsDocumentos, ConfiguracionDto optionsConfig,
            SmtpConfigurationDTo smtpConfiguration, AceptantesConfig aceptantes)
        {
            _acuseRepository = acuseRepository;
            _emisorRepository = emisorRepository;
            _ltsDocumentos = ltsDocumentos;
            _optionsConfig = optionsConfig;
            _smtpConfiguration = smtpConfiguration;
            _aceptantes = aceptantes;
        }


        public void AceptarDocumento()
        {
            var agruparDocumentos = _ltsDocumentos.ToLookup(o => o.id);
            foreach (var doc in agruparDocumentos)
                SetDatosTrama(doc.Key);
        }

        private void SetDatosTrama(int id)
        {
            var dto = _ltsDocumentos.Where(o => o.id == id).ToList();
            try
            {
                if (dto.Count == 2)
                {
                    _byteXml = null;
                    _bytePdf = null;
                    var xml = dto.Where(x => x.nombreDocumento.ToLower().EndsWith("xml")).FirstOrDefault();
                    var pdf = dto.Where(x => x.nombreDocumento.ToLower().EndsWith("pdf")).FirstOrDefault();
                    _byteXml = xml.documentos;
                    _bytePdf = pdf.documentos;
                    _nombrePdf = pdf.nombreDocumento;
                    _nomreXml = xml.nombreDocumento;
                    _xDoc = Encoding.UTF8.GetString(_byteXml);
                    Console.WriteLine($"Procesando el documento {_nomreXml}");
                    var action = ValidarXml() && ValidaXmlXsd(_xDoc) ? (Action)ObtenerJson : EnviarCorreo;
                    action();
                }
            }
            catch(Exception ex) {
                Util.Log("Acuse", $"Error {ex.Message},,, ,{dto.FirstOrDefault().nombreDocumento}");
                Util.Log("AcuseCompleto", $"Error {ex.ToString()},,, ,{dto.FirstOrDefault().nombreDocumento}");
                if (ex.Message.IndexOf("The request channel timed out attempting to send after") > 0)
                    SetDatosTrama(id);
            }
        }

        private void ConexionWs()
        {
            _mensaje = String.Empty;
            using (XdServiceSoapClient client = new XdServiceSoapClient(EndpointConfiguration.XdServiceSoap, _optionsConfig.WsRecepcion))
            {
                var request = client.recibeInfoSAsync(_JsonResult, "").Result;
                var xDoc = new XmlDocument();
                xDoc.InnerXml = request.Body.recibeInfoSResult.OuterXml;
                var InvioceLine = (xDoc);

                foreach (XmlElement InvLine in InvioceLine)
                {
                    _mensaje = InvLine.GetElementsByTagName("RespuestaDian")[0].InnerText;
                    Util.Log("Acuse", $"{_mensaje},,, ,{_nomreXml}");

                }
            }
        }

        private bool ValidarXml()
        {
            try
            {
                _oXml = DeserializeXml<AttachedDocumentType>(_xDoc);
                _oPartyTaxScheme = _oXml.SenderParty.PartyTaxScheme.FirstOrDefault();
                _proveedor = _oPartyTaxScheme.CompanyID.Value;
                var existe = _oXml.ReceiverParty.PartyTaxScheme.Where(x => x.CompanyID.Value.Equals(_optionsConfig.Identificacion)).ToList();
                return existe.Count > 0 ? true : false;
            }
            catch(Exception ex) {
                Util.Log("Acuse", $"Error Estructura,,, ,{_nomreXml}");
            }
            return false;
        }

        private T DeserializeXml<T>(string xml)
        {
            var xDoc = new XmlDocument();
            xDoc.LoadXml(xml);
            var oSerializar = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xDoc.InnerXml))
            {
                var result = (T)oSerializar.Deserialize(reader);
                return result;
            }
        }

        private void ObtenerJson()
        {
            SetDatosAcuse();
            var enviar = true;
            for (var i = 0; i < 3; i++)
            {
                if (enviar && !string.IsNullOrEmpty(_idComprobante))
                {
                    _general.Tipo = i == 0 ? "030" : i == 1 ? "032" : "033";
                    var aceptanteConfig= _aceptantes.List.FirstOrDefault(p => p.Level.Equals(_general.Tipo));
                    ValidarRadian();
                    if (!_evento)
                    {
                        _aceptante = _acuseRepository.ObtenerAceptante(aceptanteConfig.User);
                        if (_aceptante != null)
                        {
                            _general.Aceptante = _aceptante;
                            var comprobante = new ComprobanteJson(_optionsConfig,_general, _idComprobante);
                            var director = new JsonDirector(comprobante);
                            director.GenerarJson(_optionsConfig.Identificacion);
                            var context = comprobante.Context();
                            context.GenerarJson();
                            _JsonResult = context.JsonResult;
                            _general.Observciones = context.comprobante.CondicionEntrega;
                            ActualizarAcuse();
                            ConexionWs();
                            Console.WriteLine(_mensaje);
                            enviar = _mensaje == "Procesado Correctamente." 
                                    || _mensaje == "Regla: LGC01, Rechazo: Evento registrado previamente"
                                    ||_mensaje.IndexOf("Documento procesado anteriormente.")>0 ? true : false;
                            if (enviar)
                                GuardarValidaciones(_general.Aceptante, _idComprobante, "1", _general.Observciones);
                            else
                            {
                                Console.WriteLine("Error DIAN "+_mensaje.IndexOf("Documento procesado anteriormente.").ToString());
                                Util.Log("Acuse", $"{_mensaje} ,,, ,{_nomreXml}");
                            }

                        }
                        else
                            Console.WriteLine("Los datos del Aceptante no son correctos");

                    }
                    else
                        Console.WriteLine(_mensaje);
                }
                if (string.IsNullOrEmpty(_idComprobante))
                {
                    Console.WriteLine("El comprobante no existe en la base de datos");
                    Util.Log("Acuse", $"El comprobante no existe en la base de datos,,, ,{_nomreXml}");
                }
            }
        }

        private void SetDatosAcuse()
        {
            _documentoRef = _oXml.ParentDocumentLineReference[0].DocumentReference;
            _idComprobante = _acuseRepository.ObtenerComprobante(_documentoRef.UUID.Value);
            _idComprobante = _idComprobante ?? String.Empty;
            var emisor = _emisorRepository.ObtenerDatosEmisor(_optionsConfig.Identificacion);
            var serie = _acuseRepository.ObtenerSerie();
            var _xml = new XmlDocument();
            _xml.LoadXml(_xDoc);
            _general = new DatosGeneralDto
            {
                Aceptante = _aceptante,
                Attached = _oXml,
                Emisor = emisor,
                Xml = _xml,
                Serie = serie
            };

        }

        private bool ValidaXmlXsd(string xml)
        {
            return true;
            try
            {
                var xDoc = new XmlDocument();
                xDoc.LoadXml(xml);
                _xsdSettings = new XmlReaderSettings();
                _xsdSettings.ValidationType = ValidationType.Schema;
                _xsdSettings.Schemas.Add("urn:un:unece:uncefact:data:specification:CoreComponentTypeSchemaModule:2", $"{_optionsConfig.RutaXsd}CCTS_CCT_SchemaModule-2.1.xsd");
                _xsdSettings.Schemas.Add("urn:oasis:names:specification:ubl:schema:xsd:AttachedDocument-2", $"{_optionsConfig.RutaXsd}UBL-AttachedDocument-2.1.xsd");
                _xsdSettings.Schemas.Add("urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2", $"{_optionsConfig.RutaXsd}UBL-CommonAggregateComponents-2.1.xsd");
                _xsdSettings.Schemas.Add("urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2", $"{_optionsConfig.RutaXsd}UBL-CommonBasicComponents-2.1.xsd");
                _xsdSettings.Schemas.Add("urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2", $"{_optionsConfig.RutaXsd}UBL-CommonExtensionComponents-2.1.xsd");
                _xsdSettings.Schemas.Add("urn:un:unece:uncefact:documentation:2", $"{_optionsConfig.RutaXsd}UBL-CoreComponentParameters-2.1.xsd");
                _xsdSettings.Schemas.Add("urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2", $"{_optionsConfig.RutaXsd}UBL-ExtensionContentDataType-2.1.xsd");
                _xsdSettings.Schemas.Add("urn:oasis:names:specification:ubl:schema:xsd:QualifiedDataTypes-2", $"{_optionsConfig.RutaXsd}UBL-QualifiedDataTypes-2.1.xsd");
                _xsdSettings.Schemas.Add("urn:oasis:names:specification:ubl:schema:xsd:UnqualifiedDataTypes-2", $"{_optionsConfig.RutaXsd}UBL-UnqualifiedDataTypes-2.1.xsd");
                _xsdSettings.Schemas.Add("dian:gov:co:facturaelectronica:Structures-2-1", $"{_optionsConfig.RutaXsd}DIAN_UBL_Structures.xsd");

                _xsdSettings.ValidationEventHandler += new ValidationEventHandler(ValidaEventHandler);
                var stm = new MemoryStream();
                xDoc.Save(stm);
                stm.Position = 0;
                _xmlReader = XmlReader.Create(stm, _xsdSettings);
                while (_xmlReader.Read())
                {

                }
                return true;
            }
            catch (Exception ex)
            {
                Util.Log("Acuse", $"Error Estructura Xsd,,, ,{_nomreXml}");

                return false;
            }
        }

        private void ValidaEventHandler(object seneder, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning) { }
            else if (e.Severity == XmlSeverityType.Error)
            {
                if (!string.IsNullOrEmpty(_mensajeError))
                    _mensajeError += $"{e.Message} ";
                else
                    _mensajeError = $"{e.Message} ";
            }
        }

        private void EnviarCorreo()
        {
            var mail = new EnviarCorreo(_smtpConfiguration);
            mail.EnviarMail(_bytePdf, _byteXml, _nombrePdf, _nomreXml, $"Xml incorrecto {_nomreXml}", _optionsConfig.Identificacion, _mensajeError);
        }

        private void ActualizarAcuse()
        {
            _acuseRepository.GuardarAcuase(_general, _idComprobante);
            _acuseRepository.ObtenerSerieFolio(_idComprobante);
        }

        public void ValidarRadian()
        {
            _evento = false;
            var nit = _acuseRepository.ConsultarNitAcuse(_documentoRef.UUID.Value);
            using (websRadianSoapClient client = new websRadianSoapClient(websRadianSoapClient.EndpointConfiguration.websRadianSoap, _optionsConfig.WsRadian))
            {
                var request = client.EventosAsync(_documentoRef.UUID.Value, nit, _general.Tipo).Result;
                var evento = request.Body.EventosResult;
                _evento = evento.Equals("false") ? false : true;
                _mensaje = _general.Tipo == "030" ? "La factura ya tiene acuse de recibo" : _general.Tipo == "032" ? "Ya tiene acuse de recibo los bienes o servicios" : "Los bienes o servicios ya se encuentran aceptados";
            }
        }

        private void GuardarValidaciones(AceptanteDto aceptante,string idComprobante,string status,string observaciones ) {

            if (!_acuseRepository.ExisteValidacion(idComprobante, aceptante.Id))
            {
                _acuseRepository.GuardarValidacion(aceptante, idComprobante, status, observaciones);
                Util.Log("Acuse", $"Acuse guardado {status} {observaciones},,, ,{_nomreXml}");
                Console.WriteLine($"Acuse guardado {status} {observaciones},,, ,{_nomreXml}");

            }
        }
    }
}
