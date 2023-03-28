using DataExpress.RecepcionCorreo.Aplicacion.DTO;
using DataExpress.RecepcionCorreo.Aplicacion.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using WsColombia;
using static WsColombia.XdServiceSoapClient;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas.Strategy
{
    public class WsColombiaStrategy : IWsRecepcionStrategy
    {
        private List<DocumentosDto> _ltsDocumentos;
        private ConfiguracionDto _optionsConfig;
        private SmtpConfigurationDTo _smtpConfiguration;
        private ArrayOfString _ltsDatos;
        private AttachedDocumentType _oXml;
        private PartyTaxSchemeType _oPartyTaxScheme;
        private string _proveedor;
        private recibeInfoResponse _reponse;
        private XmlReaderSettings _xsdSettings;
        private string _mensajeError;
        private XmlReader _xmlReader;
        private byte[] _byteXml;
        private byte[] _bytePdf;
        private string _nombrePdf;
        private string _nomreXml;
        private string _mensaje;

        public WsColombiaStrategy(List<DocumentosDto> ltsDocumentos, ConfiguracionDto optionsConfig, SmtpConfigurationDTo smtpConfiguration)
        {
            _ltsDocumentos = ltsDocumentos;
            _optionsConfig = optionsConfig;
            _smtpConfiguration = smtpConfiguration;

        }

        public void RecepcionDocumentos()
        {
            var agruparDocumentos = _ltsDocumentos.ToLookup(o => o.id);
            Util.Log("Publicados", $"Asunto,Nombre,Email,Fecha Recibido,archivo");

            foreach (var doc in agruparDocumentos)
                SetDatosTrama(doc.Key,0);
        }

        private void SetDatosTrama(int id,int reproceso)
        {
            if (reproceso > 1) return;
            var dto = _ltsDocumentos.Where(o => o.id == id).ToList();
            Console.WriteLine($"ID: {id.ToString()}");
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
                    var xDoc = Encoding.UTF8.GetString(_byteXml);

                    _ltsDatos = new ArrayOfString()
                {
                    xml.nombreDocumento,
                    xDoc,
                    pdf.nombreDocumento,
                    Convert.ToBase64String(pdf.documentos),
                    String.Empty,
                    String.Empty,
                    String.Empty
                };

                    var x = ValidarXml();
                    var item = ValidaXmlXsd(_ltsDatos[1]);                    
                    _mensaje = item.Item2;

                   //var action = ValidarXml() && ValidaXmlXsd(_ltsDatos[1]) ? (Action)ConexionWs : EnviarCorreo;
                    var action = x && item.Item1 ? (Action)ConexionWs : EnviarCorreo;
                    action();
                }
                else
                {
                    Util.Log("WS", $"Archivo Incompleto ,'','','',{dto.FirstOrDefault().nombreDocumento}");
                    Console.WriteLine($"Archivo Incompleto,'','','',{dto.FirstOrDefault().nombreDocumento}");
                }

            }
            catch(Exception ex) {
                reproceso =reproceso+1 ;
                Console.WriteLine($"Error WS {ex.Message},'','','',{dto.FirstOrDefault().nombreDocumento}");
                Util.Log("WS", $"Error WS {ex.ToString()},'','','',{dto.FirstOrDefault().nombreDocumento}");
                SetDatosTrama(id,reproceso);
            }
        }

        private void ConexionWs()
        {
            using (XdServiceSoapClient client = new XdServiceSoapClient(EndpointConfiguration.XdServiceSoap, _optionsConfig.WsRecepcion))
            {
                
                _reponse = client.recibeInfoAsync(_ltsDatos, "XML", _optionsConfig.Identificacion, _optionsConfig.Pais.ToUpper(), _proveedor, "", "RECE", null, null).Result;

            }
            foreach (XmlElement InvLine in _reponse.Body.recibeInfoResult)
            {
                var mensaje = InvLine.GetElementsByTagName("msj")[0].InnerText;
                if (!string.IsNullOrEmpty(mensaje))
                {
                    int caracteres = mensaje?.Length > 40 ? 40 : mensaje.Length;
                    Util.Log("WS", $"{mensaje.Substring(0, caracteres)},'','','',{_ltsDatos[0]}");
                    Console.WriteLine($"El comprobante {_ltsDatos[0].ToString()} fue Enviado: {mensaje.Substring(0, caracteres)}");
                    Util.Log("WSExtendido", $"{mensaje},'','','',{_ltsDatos[0]}");

                }
                else
                {
                    Util.Log("WS", $"No hubo respuesta WS,'','','',{_ltsDatos[0]}");
                    Console.WriteLine($"El comprobante  {_ltsDatos[0]} se cargó correctamente");

                }
            }

        }

        private bool ValidarXml()
        {
            bool ret = true;
            try
            {
                _oXml = DeserializeXml<AttachedDocumentType>(_ltsDatos[1]);
                _oPartyTaxScheme = _oXml.ReceiverParty.PartyTaxScheme.FirstOrDefault();
                _proveedor = _oPartyTaxScheme.CompanyID.Value;
                var existe = "";
                
                if (_proveedor != _optionsConfig.Identificacion)
                {
                    existe = "1";
                }
                //var existe = _oXml.ReceiverParty.PartyTaxScheme.Where(x => x.CompanyID.Value.Equals(_optionsConfig.Identificacion)).ToList();
                //return existe.Count > 0 ? true : false;
                if (existe == "1")
                {
                    _mensaje = "El xml no pertence al receptor " + _optionsConfig.Identificacion + ", NIT enviado: " + _proveedor;
                    EnviarCorreo();
                    ret =  false;
                }
                else
                {
                    ret =  true;
                }
            }                                                                                                    
            catch {
                _mensaje = "No es un AttachedDocument";
                EnviarCorreo();
            }

            return ret;
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

        public (bool, string) ValidaXmlXsd(string xml)
        {
            var resp = "";
            bool resp2 = true;
            var MessageError = "";

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

                #region Variables
                var UBLVersionID = "";  //UBL 2.1
                var CustomizationID = "";   // Documentos Adjuntos
                var ProfileID = "";   // Factura Electrónica de Venta
                var ProfileExecutionID = "";  // 1
                var ID = "";
                var UUID = "";
                var IssueDate = "";
                var IssueTime = "";
                var DocumentType = "";   // Contenedor de Factura Electrónica
                var ParentDocumentID = "";
                var RegistrationNameSender = "";
                var CompanyIDSender = "";
                var TaxLevelCodeSender = "";
                var IdTaxSchemeSender = "";
                var NameTaxSchemeSender = "";
                var RegistrationNameReceiver = "";
                var CompanyIDReceiver = "";
                var TaxLevelCodeReceiver = "";
                var MimeCode = "";
                var EncodingCode ="";
                var Description = "";
                var LineID = "";
                var IDDocReference ="";
                var UUIDDocReference = "";
                var IssueDateDocReference = "";
                var DocumentTypeDocReference ="";
                var MimeCodeReference ="";
                var EncodingCodeReference = "";
                var DescriptionReference = "";
                var ValidatorID = "";
                var ValidationResultCode = "";
                var ValidationDate = "";
                var ValidationTime =  "";
                
                String[] ErrorMessage = new String[50];
                #endregion

                var UBLExtensions = xDoc.GetElementsByTagName("ext:UBLExtensions")[0] != null ? true : throw new Exception("el Nodo UBLExtensions no existe");
                UBLVersionID = xDoc.GetElementsByTagName("cbc:UBLVersionID")[0] != null ? xDoc.GetElementsByTagName("cbc:UBLVersionID")[0].InnerText : throw new Exception("el Nodo UBLVersionID no existe");  //UBL 2.1
                if (!string.IsNullOrEmpty(UBLVersionID))
                {
                    if (!UBLVersionID.Equals("UBL 2.1"))
                    {
                        ErrorMessage[0] = "No contiene el literal UBL 2.1(Nodo cbc: UBLVersionID";
                        //throw new Exception("No contiene el literal UBL 2.1 (Nodo cbc:UBLVersionID)");
                    }                    
                }
                else
                {
                    ErrorMessage[0] = "el Nodo UBLVersionID viene vacio (Nodo cbc:UBLVersionID)";
                    //throw new Exception("el Nodo UBLVersionID viene vacio (Nodo cbc:UBLVersionID)");
                }
                //CustomizationID = xDoc.GetElementsByTagName("cbc:CustomizationID")[0] != null ? xDoc.GetElementsByTagName("cbc:CustomizationID")[0].InnerText : "";   // Documentos Adjuntos
                CustomizationID = xDoc.GetElementsByTagName("cbc:CustomizationID")[0] != null ? xDoc.GetElementsByTagName("cbc:CustomizationID")[0].InnerText : throw new Exception("el Nodo CustomizationID no existe");
                if(!string.IsNullOrEmpty(CustomizationID))
                {
                    if (!CustomizationID.Equals("Documentos adjuntos"))
                    {
                        ErrorMessage[1] = "No contiene el literal Documentos adjuntos (Nodo cbc:CustomizationID)";
                        //throw new Exception("No contiene el literal Documentos adjuntos (Nodo cbc:CustomizationID)");
                    }                    
                }
                else
                {
                    ErrorMessage[1] = "el Nodo CustomizationID viene vacio(Nodo cbc: CustomizationID)";
                   // throw new Exception("el Nodo CustomizationID viene vacio (Nodo cbc:CustomizationID)");
                }
                ProfileID = xDoc.GetElementsByTagName("cbc:ProfileID")[0] != null ? xDoc.GetElementsByTagName("cbc:ProfileID")[0].InnerText : throw new Exception("el Nodo ProfileID no existe");   // Factura Electrónica de Venta
                if (!string.IsNullOrEmpty(ProfileID))
                {
                    if (!ProfileID.Equals("Factura Electrónica de Venta"))
                    {
                        ErrorMessage[2] = "No contiene el literal Factura Electrónica de Venta (Nodo cbc:ProfileID)";
                        //throw new Exception("No contiene el literal Factura Electrónica de Venta (Nodo cbc:ProfileID)");
                    }                    
                }
                else
                {
                    ErrorMessage[2] = "el Nodo ProfileID viene vacio";
                    //throw new Exception("el Nodo ProfileID viene vacio");
                }
                ProfileExecutionID = xDoc.GetElementsByTagName("cbc:ProfileExecutionID")[0] != null ? xDoc.GetElementsByTagName("cbc:ProfileExecutionID")[0].InnerText : throw new Exception("el Nodo ProfileID no existe");
                if (string.IsNullOrEmpty(ProfileExecutionID))
                {
                    ErrorMessage[3] = "el Nodo ProfileExecutionID viene vacio";
                    //throw new Exception("el Nodo ProfileExecutionID viene vacio");
                }
                ID = xDoc.GetElementsByTagName("cbc:ID")[0] != null ? xDoc.GetElementsByTagName("cbc:ID")[0].InnerText : throw new Exception("el Nodo ID no existe");
                if (string.IsNullOrEmpty(ID))
                {
                    ErrorMessage[4] = "el Nodo ID viene vacio";
                    //throw new Exception("el Nodo ID viene vacio");
                }
                UUID = xDoc.GetElementsByTagName("cbc:UUID")[0] != null ? xDoc.GetElementsByTagName("cbc:UUID")[0].InnerText : throw new Exception("el Nodo ID no existe");
                if (string.IsNullOrEmpty(UUID))
                {
                    ErrorMessage[5] = "el Nodo UUID viene vacio";
                    //throw new Exception("el Nodo UUID viene vacio");
                }
                IssueDate = xDoc.GetElementsByTagName("cbc:IssueDate")[0] != null ? xDoc.GetElementsByTagName("cbc:IssueDate")[0].InnerText : throw new Exception("el Nodo IssueDate no existe");
                if (string.IsNullOrEmpty(IssueDate))
                {
                    ErrorMessage[6] = "el Nodo IssueDate viene vacio";
                    //throw new Exception("el Nodo IssueDate viene vacio");
                }
                IssueTime = xDoc.GetElementsByTagName("cbc:IssueTime")[0] != null ? xDoc.GetElementsByTagName("cbc:IssueTime")[0].InnerText : throw new Exception("el Nodo IssueTime no existe");
                if (string.IsNullOrEmpty(IssueTime))
                {
                    ErrorMessage[7] = "el Nodo IssueDate viene vacio";
                    //throw new Exception("el Nodo IssueDate viene vacio");
                }
                DocumentType = xDoc.GetElementsByTagName("cbc:DocumentType")[0] != null ? xDoc.GetElementsByTagName("cbc:DocumentType")[0].InnerText : throw new Exception("el Nodo DocumentType no existe");   // Contenedor de Factura Electrónica
                if (!string.IsNullOrEmpty(DocumentType))
                {
                    if (!DocumentType.Equals("Contenedor de Factura Electrónica"))
                    {
                        ErrorMessage[8] = "No contiene el literal Contenedor de Factura Electrónica (Nodo cbc:DocumentType)";
                        //throw new Exception("No contiene el literal Contenedor de Factura Electrónica (Nodo cbc:DocumentType)");
                    }                    
                }
                else
                {
                    ErrorMessage[8] = "el Nodo DocumentType viene vacio (Nodo cbc:DocumentType)";
                    //throw new Exception("el Nodo DocumentType viene vacio (Nodo cbc:DocumentType)");
                }
                ParentDocumentID = xDoc.GetElementsByTagName("cbc:ParentDocumentID")[0] != null ? xDoc.GetElementsByTagName("cbc:ParentDocumentID")[0].InnerText : throw new Exception("el Nodo DocumentType no existe");
                if (string.IsNullOrEmpty(ParentDocumentID))
                {
                    ErrorMessage[9] = "el Nodo ParentDocumentID viene vacio";
                    //throw new Exception("el Nodo ParentDocumentID viene vacio");
                }

                var SenderParty = xDoc.GetElementsByTagName("cac:SenderParty")[0] != null ? true : throw new Exception("el Nodo SenderParty no existe");
                XmlNodeList senderParty = xDoc.GetElementsByTagName("cac:SenderParty");
                foreach (XmlElement sender in senderParty)
                {
                    RegistrationNameSender = sender.GetElementsByTagName("cbc:RegistrationName")[0] != null ? sender.GetElementsByTagName("cbc:RegistrationName")[0].InnerText : throw new Exception("el Nodo RegistrationName no existe");
                    if (string.IsNullOrEmpty(RegistrationNameSender))
                    {
                        ErrorMessage[10] = "el Nodo RegistrationName del Sender viene vacio";
                        //throw new Exception("el Nodo RegistrationName del Sender viene vacio");
                    }
                    CompanyIDSender = sender.GetElementsByTagName("cbc:CompanyID")[0] != null ? sender.GetElementsByTagName("cbc:CompanyID")[0].InnerText : throw new Exception("el Nodo CompanyID del sender no existe");
                    if (!string.IsNullOrEmpty(CompanyIDSender))
                    {
                        XmlNodeList companySender = sender.GetElementsByTagName("cbc:CompanyID");
                        foreach (XmlNode company in companySender)
                        {
                            var schemeAgencyID = company.Attributes["schemeAgencyID"].Value;
                            if (string.IsNullOrEmpty(schemeAgencyID))
                            {
                                ErrorMessage[11] = "El atributo schemeAgencyID del senderParty esta vacio";
                                //throw new Exception("El atributo schemeAgencyID del senderParty esta vacio");
                            }
                            if (!schemeAgencyID.Equals("195"))
                            {
                                ErrorMessage[11] = "No tiene el literal 195 (Atributo schemeAgencyID)";
                                //throw new Exception("No tiene el literal 195 (Atributo schemeAgencyID)");
                            }

                            var schemeID = company.Attributes["schemeID"].Value;
                            if (string.IsNullOrEmpty(schemeID))
                            {
                                ErrorMessage[12] = "El atributo schemeID (Digito de Verificación) del senderParty esta vacio";
                                //throw new Exception("El atributo schemeID (Digito de Verificación) del senderParty esta vacio");
                            }

                            var schemeName = company.Attributes["schemeName"].Value;
                            if (string.IsNullOrEmpty(schemeName))
                            {
                                ErrorMessage[13] = "El atributo schemeName (Tipo de Documento) del senderParty esta vacio";
                                //throw new Exception("El atributo schemeName (Tipo de Documento) del senderParty esta vacio");
                            }
                        }

                    }
                    else
                    {
                        ErrorMessage[14] = "el Nodo CompanyID del Sender viene vacio";
                        //throw new Exception("el Nodo CompanyID del Sender viene vacio");
                    }
                    TaxLevelCodeSender = sender.GetElementsByTagName("cbc:TaxLevelCode")[0] != null ? sender.GetElementsByTagName("cbc:TaxLevelCode")[0].InnerText : throw new Exception("el Nodo TaxLevelCode del sender no existe");
                    if (string.IsNullOrEmpty(TaxLevelCodeSender))
                    {
                        ErrorMessage[15] = "el Nodo TaxLevelCode del Sender viene vacio";
                        //throw new Exception("el Nodo TaxLevelCode del Sender viene vacio");
                    }
                    var TaxSchemeSender = sender.GetElementsByTagName("cac:TaxScheme")[0] != null ? true : throw new Exception("el Nodo TaxScheme del sender no existe");
                    XmlNodeList taxSchemeSender = sender.GetElementsByTagName("cac:TaxScheme");
                    foreach (XmlElement tax in taxSchemeSender)
                    {
                        IdTaxSchemeSender = tax.GetElementsByTagName("cbc:ID")[0] != null ? tax.GetElementsByTagName("cbc:ID")[0].InnerText : throw new Exception("el Nodo ID del sender no existe");
                        if (string.IsNullOrEmpty(IdTaxSchemeSender))
                        {
                            ErrorMessage[16] = "el Nodo ID del Sender viene vacio";
                            //throw new Exception("el Nodo ID del Sender viene vacio");
                        }
                        NameTaxSchemeSender = tax.GetElementsByTagName("cbc:Name")[0] != null ? tax.GetElementsByTagName("cbc:Name")[0].InnerText : throw new Exception("el Nodo Name del sender no existe");
                        if (string.IsNullOrEmpty(NameTaxSchemeSender))
                        {
                            ErrorMessage[17] = "el Nodo Name del Sender viene vacio";
                            //throw new Exception("el Nodo Name del Sender viene vacio");
                        }
                    }
                }

                var ReceiverParty = xDoc.GetElementsByTagName("cac:ReceiverParty")[0] != null ? true : throw new Exception("el Nodo ReceiverParty no existe");
                XmlNodeList receiverParty = xDoc.GetElementsByTagName("cac:ReceiverParty");
                foreach (XmlElement receiver in receiverParty)
                {
                    RegistrationNameReceiver = receiver.GetElementsByTagName("cbc:RegistrationName")[0] != null ? receiver.GetElementsByTagName("cbc:RegistrationName")[0].InnerText : throw new Exception("el Nodo RegistrationName del Receiver no existe");
                    if (string.IsNullOrEmpty(RegistrationNameReceiver))
                    {
                        throw new Exception("el Nodo RegistrationName del Receiver viene vacio");
                    }
                    CompanyIDReceiver = receiver.GetElementsByTagName("cbc:CompanyID")[0] != null ? receiver.GetElementsByTagName("cbc:CompanyID")[0].InnerText : throw new Exception("el Nodo CompanyID del Receiver no existe");
                    if (!string.IsNullOrEmpty(CompanyIDReceiver))
                    {
                        XmlNodeList companyReceiver = receiver.GetElementsByTagName("cbc:CompanyID");
                        foreach (XmlNode companyR in companyReceiver)
                        {
                            var schemeAgencyID = companyR.Attributes["schemeAgencyID"].Value;
                            if (string.IsNullOrEmpty(schemeAgencyID))
                            {
                                ErrorMessage[18] = "El atributo schemeAgencyID del receiverParty esta vacio";
                                //throw new Exception("El atributo schemeAgencyID del receiverParty esta vacio");
                            }
                            if (!schemeAgencyID.Equals("195"))
                            {
                                ErrorMessage[19] = "No tiene el literal 195";
                                //throw new Exception("No tiene el literal 195");
                            }

                            var schemeID = companyR.Attributes["schemeID"].Value;
                            if (string.IsNullOrEmpty(schemeID))
                            {
                                ErrorMessage[20] = "El atributo schemeID (Digito de Verificación) del receiverParty esta vacio";
                                //throw new Exception("El atributo schemeID (Digito de Verificación) del receiverParty esta vacio");
                            }

                            var schemeName = companyR.Attributes["schemeName"].Value;
                            if (string.IsNullOrEmpty(schemeName))
                            {
                                ErrorMessage[21] = "El atributo schemeName (Tipo de Documento) del receiverParty esta vacio";
                                //throw new Exception("El atributo schemeName (Tipo de Documento) del receiverParty esta vacio");
                            }
                        }                        
                    }
                    else
                    {
                        ErrorMessage[22] = "el Nodo CompanyID del Receiver viene vacio";
                        //throw new Exception("el Nodo CompanyID del Receiver viene vacio");
                    }
                    TaxLevelCodeReceiver = receiver.GetElementsByTagName("cbc:TaxLevelCode")[0] != null ? receiver.GetElementsByTagName("cbc:TaxLevelCode")[0].InnerText : throw new Exception("el Nodo TaxLevelCode del Receiver no existe");
                    if (string.IsNullOrEmpty(TaxLevelCodeReceiver))
                    {
                        ErrorMessage[23] = "el Nodo TaxLevelCode del Receiver viene vacio";
                       // throw new Exception("el Nodo TaxLevelCode del Receiver viene vacio");
                    }
                    var TaxSchemeReceiver = receiver.GetElementsByTagName("cac:TaxScheme")[0] != null ? true : throw new Exception("el Nodo TaxScheme del Receiver no existe");
                    XmlNodeList taxSchemeReceiver = receiver.GetElementsByTagName("cac:TaxScheme");
                    foreach (XmlElement taxreceiver in taxSchemeReceiver)
                    {
                        IdTaxSchemeSender = taxreceiver.GetElementsByTagName("cbc:ID")[0] != null ? taxreceiver.GetElementsByTagName("cbc:ID")[0].InnerText : throw new Exception("el Nodo ID del Receiver no existe");
                        if (string.IsNullOrEmpty(IdTaxSchemeSender))
                        {
                            ErrorMessage[24] = "el Nodo ID del Receiver viene vacio";
                            //throw new Exception("el Nodo ID del Receiver viene vacio");
                        }
                        NameTaxSchemeSender = taxreceiver.GetElementsByTagName("cbc:Name")[0] != null ? taxreceiver.GetElementsByTagName("cbc:Name")[0].InnerText : throw new Exception("el Nodo Name del Receiver no existe");
                        if (string.IsNullOrEmpty(NameTaxSchemeSender))
                        {
                            ErrorMessage[25] = "el Nodo Name del Receiver viene vacio";
                            //throw new Exception("el Nodo Name del Receiver viene vacio");
                        }
                    }
                }

                var Attachment = xDoc.GetElementsByTagName("cac:Attachment")[0] != null ? true : throw new Exception("el Nodo Attachment no existe");
                XmlNodeList attachment = xDoc.GetElementsByTagName("cac:Attachment");
                foreach (XmlElement attach in attachment[0])
                {
                    MimeCode = attach.GetElementsByTagName("cbc:MimeCode")[0] != null ? attach.GetElementsByTagName("cbc:MimeCode")[0].InnerText : throw new Exception("el Nodo MimeCode no existe");
                    if (!string.IsNullOrEmpty(MimeCode))
                    {
                        if (!MimeCode.Equals("text/xml"))
                        {
                            ErrorMessage[26] = "El valor debe ser text/xml (Nodo cbc:MimeCode)";
                            //throw new Exception("El valor debe ser text/xml (Nodo cbc:MimeCode)");
                        }                        
                    }
                    else
                    {
                        ErrorMessage[26] = "el Nodo MimeCode viene vacio";
                        //throw new Exception("el Nodo MimeCode viene vacio");
                    }
                    EncodingCode = attach.GetElementsByTagName("cbc:EncodingCode")[0] != null ? attach.GetElementsByTagName("cbc:EncodingCode")[0].InnerText : throw new Exception("el Nodo EncodingCode no existe");
                    if (!string.IsNullOrEmpty(EncodingCode))
                    {
                        if (!EncodingCode.Equals("UTF-8"))
                        {
                            ErrorMessage[27] = "La codificación del archivo debe ser UTF-8 (Nodo cbc:EncodingCode)";
                           // throw new Exception("La codificación del archivo debe ser UTF-8 (Nodo cbc:EncodingCode)");
                        }                        
                    }
                    else
                    {
                        ErrorMessage[27] = "el Nodo EncodingCode viene vacio";
                        //throw new Exception("el Nodo EncodingCode viene vacio");
                    }
                    Description = attach.GetElementsByTagName("cbc:Description")[0] != null ? attach.GetElementsByTagName("cbc:Description")[0].InnerText : throw new Exception("el Nodo Description no existe");
                    if (string.IsNullOrEmpty(Description))
                    {
                        ErrorMessage[28] = "el Nodo Description viene vacio";
                       // throw new Exception("el Nodo Description viene vacio");
                    }
                }

                var ParentDocumentLineReference = xDoc.GetElementsByTagName("cac:ParentDocumentLineReference")[0] != null ? true : throw new Exception("el Nodo ParentDocumentLineReference no existe");
                XmlNodeList parentDocumentLineReference = xDoc.GetElementsByTagName("cac:ParentDocumentLineReference");
                foreach (XmlElement parentDocumentLine in parentDocumentLineReference)
                {
                    LineID = parentDocumentLine.GetElementsByTagName("cbc:LineID")[0] != null ? parentDocumentLine.GetElementsByTagName("cbc:LineID")[0].InnerText : throw new Exception("el Nodo LineID no existe");
                    if (string.IsNullOrEmpty(LineID))
                    {
                        ErrorMessage[29] = "el Nodo LineID viene vacio";
                        //throw new Exception("el Nodo LineID viene vacio");
                    }
                    var DocumentReference = parentDocumentLine.GetElementsByTagName("cac:DocumentReference")[0] != null ? true : throw new Exception("el Nodo DocumentReference no existe");
                    XmlNodeList documentReference = parentDocumentLine.GetElementsByTagName("cac:DocumentReference");
                    foreach (XmlElement docReference in documentReference)
                    {
                        IDDocReference = docReference.GetElementsByTagName("cbc:ID")[0] != null ? docReference.GetElementsByTagName("cbc:ID")[0].InnerText : throw new Exception("el Nodo ID de DocReference no existe");
                        if (string.IsNullOrEmpty(IDDocReference))
                        {
                            ErrorMessage[30] = "el Nodo ID de DocReference viene vacio (Serie y consecutivo de la factura referenciada)";
                           // throw new Exception("el Nodo ID de DocReference viene vacio (Serie y consecutivo de la factura referenciada)");
                        }
                        UUIDDocReference = docReference.GetElementsByTagName("cbc:UUID")[0] != null ? docReference.GetElementsByTagName("cbc:UUID")[0].InnerText : throw new Exception("el Nodo UUID de DocReference no existe");
                        if (string.IsNullOrEmpty(UUIDDocReference))
                        {
                            ErrorMessage[31] = "el Nodo UUID de DocReference viene vacio (CUFE de la factura referenciada)";
                           // throw new Exception("el Nodo UUID de DocReference viene vacio (CUFE de la factura referenciada)");
                        }
                        IssueDateDocReference = docReference.GetElementsByTagName("cbc:IssueDate")[0] != null ? docReference.GetElementsByTagName("cbc:IssueDate")[0].InnerText : throw new Exception("el Nodo IssueDate de DocReference no existe");
                        if (string.IsNullOrEmpty(IssueDateDocReference))
                        {
                            ErrorMessage[32] = "el Nodo IssueDate de DocReference viene vacio";
                            //throw new Exception("el Nodo IssueDate de DocReference viene vacio");
                        }
                        DocumentTypeDocReference = docReference.GetElementsByTagName("cbc:DocumentType")[0] != null ? docReference.GetElementsByTagName("cbc:DocumentType")[0].InnerText : throw new Exception("el Nodo DocumentType de DocReference no existe");
                        if (!string.IsNullOrEmpty(DocumentTypeDocReference))
                        {
                            if (!DocumentTypeDocReference.Equals("ApplicationResponse"))
                            {
                                ErrorMessage[33] = "No contiene el literal ApplicationResponse (Nodo cbc:DocumentType)";
                                //throw new Exception("No contiene el literal ApplicationResponse (Nodo cbc:DocumentType)");
                            }                            
                        }
                        else
                        {
                            ErrorMessage[33] = "el Nodo DocumentType de DocReference viene vacio";
                            //throw new Exception("el Nodo DocumentType de DocReference viene vacio");
                        }
                        var AttachmentDocReference = docReference.GetElementsByTagName("cac:Attachment")[0] != null ? true : throw new Exception("el Nodo Attachment de DocReference no existe");
                        XmlNodeList attachmentDocReference = docReference.GetElementsByTagName("cac:Attachment");
                        foreach (XmlElement attachReference in attachmentDocReference)
                        {
                            MimeCodeReference = attachReference.GetElementsByTagName("cbc:MimeCode")[0] != null ? attachReference.GetElementsByTagName("cbc:MimeCode")[0].InnerText : throw new Exception("el Nodo MimeCode de DocReference no existe");
                            if (!string.IsNullOrEmpty(MimeCodeReference))
                            {
                                if (!MimeCodeReference.Equals("text/xml"))
                                {
                                    ErrorMessage[34] = "El valor debe ser text/xml (Nodo cbc:MimeCode)";
                                   // throw new Exception("El valor debe ser text/xml (Nodo cbc:MimeCode)");
                                }
                            }
                            else
                            {
                                ErrorMessage[34] = "el Nodo MimeCode viene vacio";
                               // throw new Exception("el Nodo MimeCode viene vacio");
                            }
                            EncodingCodeReference = attachReference.GetElementsByTagName("cbc:EncodingCode")[0] != null ? attachReference.GetElementsByTagName("cbc:EncodingCode")[0].InnerText : throw new Exception("el Nodo EncodingCode de DocReference no existe");
                            if (!string.IsNullOrEmpty(EncodingCodeReference))
                            {
                                if (!EncodingCodeReference.Equals("UTF-8"))
                                {
                                    ErrorMessage[35] = "La codificación del archivo debe ser UTF-8 (Nodo cbc:EncodingCode)";
                                    //throw new Exception("La codificación del archivo debe ser UTF-8 (Nodo cbc:EncodingCode)");
                                }
                            }
                            else
                            {
                                ErrorMessage[35] = "el Nodo EncodingCode de DocReference viene vacio";
                                //throw new Exception("el Nodo EncodingCode de DocReference viene vacio");
                            }
                            DescriptionReference = attachReference.GetElementsByTagName("cbc:Description")[0] != null ? attachReference.GetElementsByTagName("cbc:Description")[0].InnerText : throw new Exception("el Nodo Description de DocReference no existe");
                            if (string.IsNullOrEmpty(DescriptionReference))
                            {
                                ErrorMessage[36] = "el Nodo Description de DocReference viene vacio";
                                //throw new Exception("el Nodo Description de DocReference viene vacio");
                            }
                        }
                        var ResultOfVerificationDocReference = docReference.GetElementsByTagName("cac:ResultOfVerification")[0] != null ? true: throw new Exception("el Nodo ResultOfVerification de DocReference no existe");
                        XmlNodeList resultOfVerificationDocReference = docReference.GetElementsByTagName("cac:ResultOfVerification");
                        foreach (XmlElement result in resultOfVerificationDocReference)
                        {
                            ValidatorID = result.GetElementsByTagName("cbc:ValidatorID")[0] != null ? result.GetElementsByTagName("cbc:ValidatorID")[0].InnerText : throw new Exception("el Nodo ValidatorID no existe");
                            if (!string.IsNullOrEmpty(ValidatorID))
                            {
                                if (!ValidatorID.Equals("Unidad Especial Dirección de Impuestos y Aduanas Nacionales"))
                                {
                                    ErrorMessage[37] = "No contiene el literal - Unidad Especial Dirección de Impuestos y Aduanas Nacionales (Nodo cbc:ValidatorID)";
                                   // throw new Exception("No contiene el literal - Unidad Especial Dirección de Impuestos y Aduanas Nacionales (Nodo cbc:ValidatorID)");
                                }                                
                            }
                            else
                            {
                                ErrorMessage[37] = "el Nodo ValidatorID viene vacio";
                                //throw new Exception("el Nodo ValidatorID viene vacio");
                            }
                            ValidationResultCode = result.GetElementsByTagName("cbc:ValidationResultCode")[0] != null ? result.GetElementsByTagName("cbc:ValidationResultCode")[0].InnerText : throw new Exception("el Nodo ValidationResultCode no existe");
                            if (string.IsNullOrEmpty(ValidationResultCode))
                            {
                                ErrorMessage[38] = "el Nodo ValidationResultCode viene vacio";
                                //throw new Exception("el Nodo ValidationResultCode viene vacio");
                            }
                            ValidationDate = result.GetElementsByTagName("cbc:ValidationDate")[0] != null ? result.GetElementsByTagName("cbc:ValidationDate")[0].InnerText : throw new Exception("el Nodo ValidationDate no existe");
                            if (string.IsNullOrEmpty(ValidationDate))
                            {
                                ErrorMessage[39] = "el Nodo ValidationDate viene vacio";
                               // throw new Exception("el Nodo ValidationDate viene vacio");
                            }
                            ValidationTime = result.GetElementsByTagName("cbc:ValidationTime")[0] != null ? result.GetElementsByTagName("cbc:ValidationTime")[0].InnerText : throw new Exception("el Nodo ValidationTime no existe");
                            if (string.IsNullOrEmpty(ValidationTime))
                            {
                                ErrorMessage[40] = "el Nodo ValidationTime viene vacio";
                                //throw new Exception("el Nodo ValidationTime viene vacio");
                            }
                        }
                    }
                }

                foreach (var message in ErrorMessage)
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        if (!string.IsNullOrEmpty(MessageError))
                        {
                            MessageError += $"\r\n{message}\r\n";
                            resp2 = false;
                        }        
                        else
                            MessageError = message;
                    }
                }
                //while (_xmlReader.Read())
                //{

                //}

                //if (_mensajeError != null)
                //{
                //    return false;
                //}
                //else
                //{
                //    return true;
                //}

            }
            catch (Exception ex)
            {
                resp = ex.ToString();
                resp2 = false;

                return (resp2, MessageError);
            }

            return (resp2, MessageError);
        }

        private void ValidaEventHandler(object seneder, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning) 
            {
                if (!string.IsNullOrEmpty(_mensajeError))
                    _mensajeError += $"{e.Message} ";
                else
                    _mensajeError = $"{e.Message} ";
            }
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
            Util.Log("WS", $"Estructura Invalida.,'','','',{_ltsDatos[0]}");
            var mail = new EnviarCorreo(_smtpConfiguration);
            mail.EnviarMail(_bytePdf, _byteXml, _nombrePdf, _nomreXml, $"Xml incorrecto {_nomreXml}", _optionsConfig.Identificacion, _mensajeError + _mensaje);
        }
    }
}
