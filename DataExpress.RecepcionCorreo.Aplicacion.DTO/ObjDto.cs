using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Xml;

namespace DataExpress.RecepcionCorreo.Aplicacion.DTO
{
    public class ObjDto
    {
        #region Privado

        #region SMTP
        private string _Host = String.Empty;
        private int _Port = 587;
        private Boolean _EnableSSL = false;
        private Boolean _UseDefaultCredentials = false;
        private string _CredentialsUSER = String.Empty;
        private string _CredentialsPass = String.Empty;
        private string _EmailTo = String.Empty;
        private string _EmailFrom = String.Empty;
        private string _EmailCC = String.Empty;
        private string _EmailBcc = String.Empty;
        private string _EmailSubject = String.Empty;
        private string _EmailBody = String.Empty;
        private Boolean _IsBodyHtml = true;
        private MailPriority _Priority = MailPriority.Normal;
        private ArrayList _Attachments = new ArrayList();
        #endregion

        #region EnteFiscal
        private string _URLWs = String.Empty;
        private string _URLWs2 = String.Empty;
        private string _URLWs3 = String.Empty;
        private string _URLWs4 = String.Empty;
        private string _URLWs5 = String.Empty;
        private string _RespuestaENTF = String.Empty;
        private string _RespuestaENTF2 = String.Empty;
        private string _RespuestaENTF3 = String.Empty;
        private string _RespuestaENTF4 = String.Empty;
        private string _RespuestaENTF5 = String.Empty;
        private string _UserENTF = String.Empty;
        private string _PassENTF = String.Empty;
        private string _Ambiente = String.Empty;
        private string _ArchivoENTF = String.Empty;
        private string _NombreArchivoENTF = String.Empty;
        private string _ExtencionArchivoENTF = String.Empty;
        private string _NombreENTF = String.Empty;
        private string _Certificado = String.Empty;
        private string _Certificado2 = String.Empty;
        private string _Certificado3 = String.Empty;
        private string _FechaCaducidadCertificado = String.Empty;
        private string _FechaCaducidadCertificado2 = String.Empty;
        private string _FechaCaducidadCertificado3 = String.Empty;
        private string _UserCertificado = String.Empty;
        private string _PassCertificado = String.Empty;
        private string _UserCertificado2 = String.Empty;
        private string _PassCertificado2 = String.Empty;
        private string _UserCertificado3 = String.Empty;
        private string _PassCertificado3 = String.Empty;
        private string _NoSolicitudID = String.Empty;
        private string _ResponseCodeENTF = String.Empty;
        private string _DescripcionRespuestaENTF = String.Empty;
        private string[] _NotaRespuestaENTF = null;
        private string _ApplicationResponse = String.Empty;
        #endregion


        private int idComprobante = 0;
        private string _DirDocus;

        #region
        private string _MensajeComprobante = String.Empty;
        private string _MensajeComprobanteTecnico = String.Empty;
        private string _MensajeComprobanteEnteFiscal = String.Empty;
        private string _MensajeComprobanteCodigo = String.Empty;
        private string _MensajeComprobanteCodigoEnteFiscal = String.Empty;

        #endregion

        #region Totales/Conceptos/Subconceptos
        private Array _Conceptos;
        private ArrayList _SubConceptos = new ArrayList();
        private ArrayList _ArraySubConceptos = new ArrayList();
        private ArrayList _Totales = new ArrayList();
        public string _tasaCambio = "";
        #endregion

        #region Footer
        private byte[] _ImgLogofooter;
        private byte[] _ImgLogoSecundariofooter;
        private byte[] _ImgInformativofooter;
        private string _TextInformativofooter;
        private string _Textfooter;
        private Array _InfoAdicionalfooter;
        #endregion
        private string FechaDocSustento;
        private string CargoGlobal;
        private string _VersionDoc = "0";
        private string _Cajero = "1";
        private ArrayList _MetodoPago = new ArrayList();
        private ArrayList _CInforme_DetalleAdicional = new ArrayList();
        private ArrayList _CInforme_DetalleAdicional1 = new ArrayList();
        public string _metodoUnico = "";
        private string _FolioReservacion;
        private string _FechaVencimiento;
        private string _Pedido = "";
        private string _Termino = "0";
        private string _FirmaEnteFiscal;
        private string _NoCertificado = "0";
        private string _Sello = "0";
        private string _CantLetras = "0";
        private string _CantLetrasIngles = "0";
        private string _DirPartida;
        private string _MovitoDocumento = "";
        private string _Placa;
        private DateTime? _FechaFinTransporte;
        private DateTime? _FechaIniTransporte;
        private string _TipoEnvio;
        private string _Creado = "0";
        private string _Tipo;
        private string _Estado;
        private string _Ptoemi;
        private string _Estab;
        private string _ClaveAcceso = "0";
        private string _TipoEmision;
        private string _Rise;
        private string _Email = String.Empty;
        private string _UrbanizacionEmisor = String.Empty;
        private bool _Trgrat = false;
        private string _CodOtrosConceptosPago = String.Empty;
        private string _OtrosConceptosPago = String.Empty;
        private string _DocReference = String.Empty;
        private string _Descripcion = "";
        private string _ResponseCode = String.Empty;
        private string _ReferenceID = String.Empty;
        private ArrayList _ArrayInfoAdicional = new ArrayList();
        private string _RetVal2 = "0";
        private string _RetCant3 = "0";
        private string _RetVal1 = "0";
        private string _RetCant2 = "0";
        private string _RetPeriodo = String.Empty;
        private string _RetCant = "0";
        private string _CodDocRet = String.Empty;
        private ArrayList _ArrayDetalleRet = new ArrayList();
        private string _MontoTotPerc_2 = "0";
        private string _MontoPerc_2 = "0";
        private string _PercCodMonto_2 = "0";
        private string _MontoTotPerc = "0";
        private string _MontoPerc = "0";
        private string _BaseImpon = "0";
        private string _PercCodMonto = "0";
        private ArrayList _ArrayDetalles = new ArrayList();
        private ArrayList _ArraylLeyenda = new ArrayList();
        private ArrayList _ArrayImpuestos = new ArrayList();
        private ArrayList _ArrayCargos = new ArrayList();
        private string _DescGlobMonto = "0";
        private string _ImpTotalVenta = "0";
        private string _MontoDesc = "0";
        private string _CodTipDesc = String.Empty;
        private string _SumOtrCargos = "0";
        private string _CodTotVentaGrat = String.Empty;
        private string _ValTotVentaGrat = "0";
        private string _CodInTribOtr = String.Empty;
        private string _NomTribOtrSum = String.Empty;
        private string _CodOtrTribSum = String.Empty;
        private string _SumOtrTrib = String.Empty;
        private string _CodInTribISCSum = String.Empty;
        private string _NomTribISCSum = String.Empty;
        private string _CodTribISCSum = String.Empty;
        private string _SumISC = "0";
        private string _CodInTribIGVSum = String.Empty;
        private string _NomTribIGVSum = String.Empty;
        private string _CodTribIGVSum = String.Empty;
        private string _SumIGV = "0";
        private string _MontoValorExo = "0";
        private string _CodTipMontoExo = String.Empty;
        private string _MontoValorIna = "0";
        private string _CodTipMontoIna = String.Empty;
        private string _MontoValorGrav = "0";
        private string _CodTipMontoGrav = String.Empty;
        private string _UrbanizacionReceptor = String.Empty;
        private string _UbigeoReceptor = String.Empty;
        private string _CodigoDocumentoModificado = String.Empty;
        private string _NumeroModificado = String.Empty;
        private string _TipoGuia = String.Empty;
        private string _NumeroGuia = String.Empty;
        private string _Moneda = String.Empty;
        private string _MsjArc = String.Empty;
        private string _DOCversion = String.Empty;
        private string _UBLversion = String.Empty;
        private string _numeroAutorizacion = String.Empty;
        private ArrayList _ArrayExportacion = new ArrayList();
        private string _Notas = String.Empty;
        private string _DocUsuario = String.Empty;
        private string _TipoUsuario = String.Empty;
        private string _ModalidadesPago = String.Empty;
        private string _Cobertura = String.Empty;
        private string _CodigoPrestador = String.Empty;
        private string _FechainicioFacturacion = String.Empty;
        private string _FechafinFacturacion = String.Empty;
        private string _NumeroIdentificacionUsuario = String.Empty;
        private string _PrimerNombreUser = String.Empty;
        private string _SegundoNombreUser = String.Empty;
        private string _PrimerApellidoUser = String.Empty;
        private string _SegundoApellidoUser = String.Empty;
        private string _NumeroAutorizacionUser = String.Empty;
        private string _NumeroMIPRES = String.Empty;
        private string _EntregaMIPRES = String.Empty;
        private string _NumeroContratoUser = String.Empty;
        private string _Poliza = String.Empty;
        private string _Copago = "0";
        private string _CuotaModeradora = "0";
        private string _PagosCompartidos = "0";
        private string _CuotaRecuperacion = "0";

        private string _NombreGenEvento = String.Empty;
        private string _DocGenEvento = String.Empty;
        private string _TipoDocGenEvento = String.Empty;
        private string _PrimerPago = "0";
        private string _SegundoPago = "0";
        private string _TercerPago = "0";
        private string _Evento = String.Empty;
        private string _Nota1 = String.Empty;
        private string _Nota2 = String.Empty;
        private string _TipoDocRef = String.Empty;
        private string _CufeRef = String.Empty;
        private string _VencimientoRef = String.Empty;
        private string _NombrePersona = String.Empty;
        private string _TipoPersona = String.Empty;
        private string _TipoDocPersona = String.Empty;
        private string _DocPersona = String.Empty;
        private string _DVPersona = String.Empty;
        private string _Factor = String.Empty;
        private string _DocumentoProceso = String.Empty;
        private string _Juez = String.Empty;
        private string _IdentificacionJuez = String.Empty;
        private string _TipoIdentificacionJuez = String.Empty;
        private string _TipoPersonaJuez = String.Empty;

        private string _TipoDocReferenciado = String.Empty;
        private string _CufeReferenciado = String.Empty;
        private string _VencimientoReferenciado = String.Empty;
        private string _tipoPersonaEmi = String.Empty;
        private string _tipoPersonaRece = String.Empty;
        private string _Issuer = String.Empty;
        private string _TipoPersonaIssuer = String.Empty;
        private string _TipoDocIssuer = String.Empty;
        private string _NitIssuer = String.Empty;
        private string _DVIssuer = String.Empty;
        private string _TipoOperacion = String.Empty;
        #region Archivos
        private Byte[] _ArchivoPDF = null;
        private Byte[] _ArchivoOrdenCompra = null;
        private string _NombreArchivoOrdenCompra = string.Empty;
        private string _ExtensionArchivoOrdenCompra = string.Empty;
        private List<Object[]> _AditionalFiles = null;
        private XmlDocument xml = new XmlDocument();
        private string _NombreRDLC = "Rep01.rdlc";
        private string _DireccionRDLC = $@"{AppDomain.CurrentDomain.BaseDirectory}RDLC\";
        #endregion

        #region Trama
        private String[] _AsLinea = null;
        private String[] _Lines = null;
        private String _Linea = String.Empty;
        private int _Numlin = 0;
        private Boolean _BanErrorArchivo = false;
        #endregion

        #region Imagenes
        private byte[] _Logo = null;
        private byte[] _LogoSecundario = null;
        private byte[] _BannerTop = null;
        #endregion

        #region Comprobante Informacion
        private string _CodigoDocumento = String.Empty;
        private string _NombreDocumeno = String.Empty;
        private string _Folio = String.Empty;
        private string _Serie = String.Empty;
        private string _Secuencial = String.Empty;
        private string _FechaEmision = String.Empty;
        private string _FechaAutorizacion = String.Empty;
        private string _FechaAutorizacionEnteFiscal = String.Empty;
        private string _FirmaDigital = String.Empty;
        #endregion


        #region Emisor Matriz Datos Fiscales
        private string _NombreComercial = String.Empty;
        private string _RazonSocial = String.Empty;
        private string _Identificacion = String.Empty;
        private string _TipoIdentificacion = String.Empty;
        private string _DireccionMatriz = String.Empty;
        private string _TelefonosMatriz = String.Empty;
        #endregion

        #region Emisor Sucursal Datos Fiscales
        private string _NombreComercialSucursal = String.Empty;
        private string _DireccionSucursal = String.Empty;
        private string _TelefonosSucursal = String.Empty;
        #endregion

        #region Emisor Informacion Adicional
        private string _PaisEmisor = String.Empty;
        private string _CodPostEmisor = String.Empty;
        private string _ProvinciaEmisor = String.Empty;
        private string _DepartamentoEmisor = String.Empty;
        private string _DistritoEmisor = String.Empty;
        private string _CodPaisEmisor = String.Empty;
        private Array _InfoAdicionalObligatoriaEmisor = null;
        private Array _InfoAdicionalEmisor = null;
        #endregion        


        #region Receptor Matriz Datos Fiscales
        private string _NombreComercialReceptor = String.Empty;
        private string _RazonSocialReceptor = String.Empty;
        private string _IdentificacionReceptor = String.Empty;
        private string _TipoIdentificacionReceptor = String.Empty;
        private string _DireccionMatrizReceptor = String.Empty;
        private string _TelefonosMatrizReceptor = String.Empty;
        private string _codigoCliente = string.Empty;
        private string _zonaCliente = string.Empty;
        private string _canalCliente = string.Empty;
        #endregion

        #region Receptor Sucursal Datos Fiscales
        private string _NombreComercialSucursalReceptor = String.Empty;
        private string _DireccionSucursalReceptor = String.Empty;
        private string _TelefonosSucursalReceptor = String.Empty;
        #endregion

        #region Receptor Informacion Adicional
        private string _PaisReceptor = String.Empty;
        private string _CodPostReceptor = String.Empty;
        private string _ProvinciaReceptor = String.Empty;
        private string _DepartamentoReceptor = String.Empty;
        private string _DistritoReceptor = String.Empty;
        private string _CodPaisReceptor = String.Empty;
        private Array _InfoAdicionalObligatoriaReceptor = null;
        private Array _InfoAdicionalReceptor = null;
        #endregion


        #region Informacion Adicional
        private Array _InfoAdicionalObligatoria = null;
        private Array _InfoAdicional = null;
        private string _nombreTransportista;
        private string _TransportistaId;
        private string _TransportistaRegimen;
        private string _TransportistaDireccion;
        private string _TransportistaMunicipio;
        private string _TransportistaDepartamento;
        private string _TransportistaPaisId;
        private string _DireccionEntrega;
        private string _MunicipioEntrega;
        private string _DepartamentoEntrega;
        private string _PaisIdEntrega;
        private string _CondicionEntrega;
        private string _CodigoPostalEntrega;
        #endregion

        #endregion

        #region Publico 

        public string ApplicationResponse
        {
            get
            {
                return _ApplicationResponse;
            }
            set
            {
                _ApplicationResponse = value;
            }
        }
        public string _FechaDocSustento
        {
            get
            {
                return FechaDocSustento;
            }
            set
            {
                FechaDocSustento = value;
            }
        }

        public string _CargoGlobal
        {
            get
            {
                return CargoGlobal;
            }

            set
            {
                CargoGlobal = value;
            }
        }
        public ArrayList ArrayExportacion
        {
            get
            {
                return _ArrayExportacion;
            }

            set
            {
                _ArrayExportacion = value;
            }
        }
        public ArrayList CInforme_DetalleAdicional1
        {
            get
            {
                return _CInforme_DetalleAdicional1;
            }
            set
            {
                _CInforme_DetalleAdicional1 = value;
            }
        }
        public ArrayList CInforme_DetalleAdicional
        {
            get
            {
                return _CInforme_DetalleAdicional;
            }
            set
            {
                _CInforme_DetalleAdicional = value;
            }
        }
        public string ArchivoENTF
        {
            get
            {
                return _ArchivoENTF;
            }
            set
            {
                _ArchivoENTF = value;
            }
        }
        /// <summary>
        /// Archivos Adjuntos, Nombre/Archivo Byte convertido en String
        /// </summary>
        public ArrayList Attachments
        {
            get
            {
                return _Attachments;
            }
            set
            {
                _Attachments = value;
            }
        }
        public string Certificado
        {
            get
            {
                return _Certificado;
            }
            set
            {
                _Certificado = value;
            }
        }
        public string Certificado2
        {
            get
            {
                return _Certificado2;
            }
            set
            {
                _Certificado2 = value;
            }
        }
        public string Certificado3
        {
            get
            {
                return _Certificado3;
            }
            set
            {
                _Certificado3 = value;
            }
        }
        public string CredentialsPass
        {
            get
            {
                return _CredentialsPass;
            }
            set
            {
                _CredentialsPass = value;
            }
        }
        public string CredentialsUSER
        {
            get
            {
                return _CredentialsUSER;
            }
            set
            {
                _CredentialsUSER = value;
            }
        }
        public string DescripcionRespuestaENTF
        {
            get
            {
                return _DescripcionRespuestaENTF;
            }
            set
            {
                _DescripcionRespuestaENTF = value;
            }
        }
        public string DirDocus
        {
            get
            {
                return _DirDocus;
            }
            set
            {
                _DirDocus = value;
            }
        }
        public Array Conceptos
        {
            get
            {
                return _Conceptos;
            }
            set
            {
                _Conceptos = value;
            }
        }

        public string tasaCambio
        {
            get
            {
                return _tasaCambio;
            }
            set
            {
                _tasaCambio = value;
            }
        }
        public string DireccionRDLC
        {
            get
            {
                return _DireccionRDLC;
            }
            set
            {
                _DireccionRDLC = value;
            }
        }
        public string EmailBcc
        {
            get
            {
                return _EmailBcc;
            }
            set
            {
                _EmailBcc = value;
            }
        }
        public string EmailBody
        {
            get
            {
                return _EmailBody;
            }
            set
            {
                _EmailBody = value;
            }
        }
        public string EmailCC
        {
            get
            {
                return _EmailCC;
            }
            set
            {
                _EmailCC = value;
            }
        }
        public string EmailFrom
        {
            get
            {
                return _EmailFrom;
            }
            set
            {
                _EmailFrom = value;
            }
        }
        public string EmailSubject
        {
            get
            {
                return _EmailSubject;
            }
            set
            {
                _EmailSubject = value;
            }
        }
        public string EmailTo
        {
            get
            {
                return _EmailTo;
            }
            set
            {
                _EmailTo = value;
            }
        }
        public Boolean EnableSSL
        {
            get
            {
                return _EnableSSL;
            }
            set
            {
                _EnableSSL = value;
            }
        }
        public string ExtencionArchivoENTF
        {
            get
            {
                return _ExtencionArchivoENTF;
            }
            set
            {
                _ExtencionArchivoENTF = value;
            }
        }
        public string FechaCaducidadCertificado
        {
            get
            {
                return _FechaCaducidadCertificado;
            }
            set
            {
                _FechaCaducidadCertificado = value;
            }
        }
        public string FechaCaducidadCertificado2
        {
            get
            {
                return _FechaCaducidadCertificado2;
            }
            set
            {
                _FechaCaducidadCertificado2 = value;
            }
        }
        public string FechaCaducidadCertificado3
        {
            get
            {
                return _FechaCaducidadCertificado3;
            }
            set
            {
                _FechaCaducidadCertificado3 = value;
            }
        }
        public string Host
        {
            get
            {
                return _Host;
            }
            set
            {
                _Host = value;
            }
        }
        public int IdComprobante
        {
            get
            {
                return idComprobante;
            }
            set
            {
                idComprobante = value;
            }
        }
        public Boolean IsBodyHtml
        {
            get
            {
                return _IsBodyHtml;
            }
            set
            {
                _IsBodyHtml = value;
            }
        }
        public string MensajeComprobante
        {
            get
            {
                return _MensajeComprobante;
            }
            set
            {
                _MensajeComprobante = value;
            }
        }
        public string MensajeComprobanteCodigo
        {
            get
            {
                return _MensajeComprobanteCodigo;
            }
            set
            {
                _MensajeComprobanteCodigo = value;
            }
        }
        public string MensajeComprobanteCodigoEnteFiscal
        {
            get
            {
                return _MensajeComprobanteCodigoEnteFiscal;
            }
            set
            {
                _MensajeComprobanteCodigoEnteFiscal = value;
            }
        }
        public string MensajeComprobanteEnteFiscal
        {
            get
            {
                return _MensajeComprobanteEnteFiscal;
            }
            set
            {
                _MensajeComprobanteEnteFiscal = value;
            }
        }
        public string MensajeComprobanteTecnico
        {
            get
            {
                return _MensajeComprobanteTecnico;
            }
            set
            {
                _MensajeComprobanteTecnico = value;
            }
        }
        public string NombreArchivoENTF
        {
            get
            {
                return _NombreArchivoENTF;
            }
            set
            {
                _NombreArchivoENTF = value;
            }
        }
        public string NombreENTF
        {
            get
            {
                return _NombreENTF;
            }
            set
            {
                _NombreENTF = value;
            }
        }
        public string NombreRDLC
        {
            get
            {
                return _NombreRDLC;
            }
            set
            {
                _NombreRDLC = value;
            }
        }
        public string NoSolicitudID
        {
            get
            {
                return _NoSolicitudID;
            }
            set
            {
                _NoSolicitudID = value;
            }
        }
        public string[] NotaRespuestaENTF
        {
            get
            {
                return _NotaRespuestaENTF;
            }
            set
            {
                _NotaRespuestaENTF = value;
            }
        }
        public string PassCertificado
        {
            get
            {
                return _PassCertificado;
            }
            set
            {
                _PassCertificado = value;
            }
        }
        public string PassCertificado2
        {
            get
            {
                return _PassCertificado2;
            }
            set
            {
                _PassCertificado2 = value;
            }
        }
        public string PassCertificado3
        {
            get
            {
                return _PassCertificado3;
            }
            set
            {
                _PassCertificado3 = value;
            }
        }
        public string PassENTF
        {
            get
            {
                return _PassENTF;
            }
            set
            {
                _PassENTF = value;
            }
        }
        public int Port
        {
            get
            {
                return _Port;
            }
            set
            {
                _Port = value;
            }
        }
        public MailPriority Priority
        {
            get
            {
                return _Priority;
            }
            set
            {
                _Priority = value;
            }
        }
        public string ResponseCodeENTF
        {
            get
            {
                return _ResponseCodeENTF;
            }
            set
            {
                _ResponseCodeENTF = value;
            }
        }
        public string RespuestaENTF
        {
            get
            {
                return _RespuestaENTF;
            }
            set
            {
                _RespuestaENTF = value;
            }
        }
        public string RespuestaENTF2
        {
            get
            {
                return _RespuestaENTF2;
            }
            set
            {
                _RespuestaENTF2 = value;
            }
        }
        public string RespuestaENTF3
        {
            get
            {
                return _RespuestaENTF3;
            }
            set
            {
                _RespuestaENTF3 = value;
            }
        }
        public string RespuestaENTF4
        {
            get
            {
                return _RespuestaENTF4;
            }
            set
            {
                _RespuestaENTF4 = value;
            }
        }
        public string RespuestaENTF5
        {
            get
            {
                return _RespuestaENTF5;
            }
            set
            {
                _RespuestaENTF5 = value;
            }
        }
        public ArrayList SubConceptos
        {
            get
            {
                return _SubConceptos;
            }
            set
            {
                _SubConceptos = value;
            }
        }
        public ArrayList Totales
        {
            get
            {
                return _Totales;
            }
            set
            {
                _Totales = value;
            }
        }
        public byte[] ImgInformativofooter
        {
            get
            {
                return _ImgInformativofooter;
            }
            set
            {
                _ImgInformativofooter = value;
            }
        }
        public byte[] ImgLogofooter
        {
            get
            {
                return _ImgLogofooter;
            }
            set
            {
                _ImgLogofooter = value;
            }
        }
        public byte[] ImgLogoSecundariofooter
        {
            get
            {
                return _ImgLogoSecundariofooter;
            }
            set
            {
                _ImgLogoSecundariofooter = value;
            }
        }
        public Array InfoAdicionalfooter
        {
            get
            {
                return _InfoAdicionalfooter;
            }
            set
            {
                _InfoAdicionalfooter = value;
            }
        }
        public string Textfooter
        {
            get
            {
                return _Textfooter;
            }
            set
            {
                _Textfooter = value;
            }
        }
        public string TextInformativofooter
        {
            get
            {
                return _TextInformativofooter;
            }
            set
            {
                _TextInformativofooter = value;
            }
        }
        public string Ambiente
        {
            get
            {
                return _Ambiente;
            }
            set
            {
                _Ambiente = value;
            }
        }
        public Byte[] ArchivoPDF
        {
            get
            {
                return _ArchivoPDF;
            }
            set
            {
                _ArchivoPDF = value;
            }
        }
        public Byte[] ArchivoOrdenCompra
        {
            get
            {
                return _ArchivoOrdenCompra;
            }
            set
            {
                _ArchivoOrdenCompra = value;
            }
        }

        public string NombreArchivoOrdenCompra
        {
            get
            {
                return _NombreArchivoOrdenCompra;
            }
            set
            {
                _NombreArchivoOrdenCompra = value;
            }
        }
        public string ExtensionArchivoOrdenCompra
        {
            get
            {
                return _ExtensionArchivoOrdenCompra;
            }
            set
            {
                _ExtensionArchivoOrdenCompra = value;
            }
        }

        public List<object[]> AditionalFiles
        {
            get
            {
                return _AditionalFiles;
            }
            set
            {
                _AditionalFiles = value;
            }
        }

        public ArrayList ArrayDetalleRet
        {
            get
            {
                return _ArrayDetalleRet;
            }
            set
            {
                _ArrayDetalleRet = value;
            }
        }
        public ArrayList ArrayDetalles
        {
            get
            {
                return _ArrayDetalles;
            }
            set
            {
                _ArrayDetalles = value;
            }
        }
        public ArrayList ArraySubConceptos
        {
            get
            {
                return _ArraySubConceptos;
            }
            set
            {
                _ArraySubConceptos = value;
            }
        }
        public ArrayList ArrayImpuestos
        {
            get
            {
                return _ArrayImpuestos;
            }
            set
            {
                _ArrayImpuestos = value;
            }
        }
        public ArrayList ArrayInfoAdicional
        {
            get
            {
                return _ArrayInfoAdicional;
            }
            set
            {
                _ArrayInfoAdicional = value;
            }
        }
        public ArrayList ArraylLeyenda
        {
            get
            {
                return _ArraylLeyenda;
            }
            set
            {
                _ArraylLeyenda = value;
            }
        }
        public String[] AsLinea
        {
            get
            {
                return _AsLinea;
            }
            set
            {
                _AsLinea = value;
            }
        }
        public Boolean BanErrorArchivo
        {
            get
            {
                return _BanErrorArchivo;
            }
            set
            {
                _BanErrorArchivo = value;
            }
        }
        public byte[] BannerTop
        {
            get
            {
                return _BannerTop;
            }
            set
            {
                _BannerTop = value;
            }
        }
        public string BaseImpon
        {
            get
            {
                return _BaseImpon;
            }
            set
            {
                _BaseImpon = value;
            }
        }
        public string Cajero
        {
            get
            {
                return _Cajero;
            }
            set
            {
                _Cajero = value;
            }
        }
        public string CantLetras
        {
            get
            {
                return _CantLetras;
            }
            set
            {
                _CantLetras = value;
            }
        }
        public string CantLetrasIngles
        {
            get
            {
                return _CantLetrasIngles;
            }
            set
            {
                _CantLetrasIngles = value;
            }
        }
        public string ClaveAcceso
        {
            get
            {
                return _ClaveAcceso;
            }
            set
            {
                _ClaveAcceso = value;
            }
        }
        public string CodDocRet
        {
            get
            {
                return _CodDocRet;
            }
            set
            {
                _CodDocRet = value;
            }
        }
        public string CodigoDocumento
        {
            get
            {
                return _CodigoDocumento;
            }
            set
            {
                _CodigoDocumento = value;
            }
        }
        public string CodigoDocumentoModificado
        {
            get
            {
                return _CodigoDocumentoModificado;
            }
            set
            {
                _CodigoDocumentoModificado = value;
            }
        }
        public string CodInTribIGVSum
        {
            get
            {
                return _CodInTribIGVSum;
            }
            set
            {
                _CodInTribIGVSum = value;
            }
        }
        public string CodInTribISCSum
        {
            get
            {
                return _CodInTribISCSum;
            }
            set
            {
                _CodInTribISCSum = value;
            }
        }
        public string CodInTribOtr
        {
            get
            {
                return _CodInTribOtr;
            }
            set
            {
                _CodInTribOtr = value;
            }
        }
        public string CodOtrosConceptosPago
        {
            get
            {
                return _CodOtrosConceptosPago;
            }
            set
            {
                _CodOtrosConceptosPago = value;
            }
        }
        public string CodPaisEmisor
        {
            get
            {
                return _CodPaisEmisor;
            }
            set
            {
                _CodPaisEmisor = value;
            }
        }
        public string CodPaisReceptor
        {
            get
            {
                return _CodPaisReceptor;
            }
            set
            {
                _CodPaisReceptor = value;
            }
        }
        public string CodPostEmisor
        {
            get
            {
                return _CodPostEmisor;
            }
            set
            {
                _CodPostEmisor = value;
            }
        }
        public string CodPostReceptor
        {
            get
            {
                return _CodPostReceptor;
            }
            set
            {
                _CodPostReceptor = value;
            }
        }
        public string CodTipDesc
        {
            get
            {
                return _CodTipDesc;
            }
            set
            {
                _CodTipDesc = value;
            }
        }
        public string CodTipMontoExo
        {
            get
            {
                return _CodTipMontoExo;
            }
            set
            {
                _CodTipMontoExo = value;
            }
        }
        public string CodTipMontoGrav
        {
            get
            {
                return _CodTipMontoGrav;
            }
            set
            {
                _CodTipMontoGrav = value;
            }
        }
        public string CodTipMontoIna
        {
            get
            {
                return _CodTipMontoIna;
            }
            set
            {
                _CodTipMontoIna = value;
            }
        }
        public string CodTotVentaGrat
        {
            get
            {
                return _CodTotVentaGrat;
            }
            set
            {
                _CodTotVentaGrat = value;
            }
        }
        public string CodTribIGVSum
        {
            get
            {
                return _CodTribIGVSum;
            }
            set
            {
                _CodTribIGVSum = value;
            }
        }
        public string CodTribISCSum
        {
            get
            {
                return _CodTribISCSum;
            }
            set
            {
                _CodTribISCSum = value;
            }
        }
        public string creado
        {
            get
            {
                return _Creado;
            }
            set
            {
                _Creado = value;
            }
        }
        public string DirPartida
        {
            get
            {
                return _DirPartida;
            }
            set
            {
                _DirPartida = value;
            }
        }
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
            }
        }
        public string estab
        {
            get
            {
                return _Estab;
            }
            set
            {
                _Estab = value;

            }
        }
        public string estado
        {
            get
            {
                return _Estado;
            }
            set
            {
                _Estado = value;
            }
        }
        public DateTime? fechaFinTransporte
        {
            get
            {
                return _FechaFinTransporte;
            }
            set
            {
                _FechaFinTransporte = value;
            }
        }
        public DateTime? fechaIniTransporte
        {
            get
            {
                return _FechaIniTransporte;
            }
            set
            {
                _FechaIniTransporte = value;
            }
        }
        public string FechaVencimiento
        {
            get
            {
                return _FechaVencimiento;
            }
            set
            {
                _FechaVencimiento = value;
            }
        }
        public string FirmaEnteFiscal
        {
            get
            {
                return _FirmaEnteFiscal;
            }
            set
            {
                _FirmaEnteFiscal = value;
            }
        }
        public string FolioReservacion
        {
            get
            {
                return _FolioReservacion;
            }
            set
            {
                _FolioReservacion = value;
            }
        }
        public ArrayList MetodoPago
        {
            get
            {
                return _MetodoPago;
            }
            set
            {
                _MetodoPago = value;
            }
        }



        public string MovitoDocumento
        {
            get
            {
                return _MovitoDocumento;
            }
            set
            {
                _MovitoDocumento = value;
            }
        }
        public string NoCertificado
        {
            get
            {
                return _NoCertificado;
            }
            set
            {
                _NoCertificado = value;
            }
        }
        public string NomTribOtrSum
        {
            get
            {
                return _NomTribOtrSum;
            }
            set
            {
                _NomTribOtrSum = value;
            }
        }
        public string DepartamentoEmisor
        {
            get
            {
                return _DepartamentoEmisor;
            }
            set
            {
                _DepartamentoEmisor = value;
            }
        }
        public string DepartamentoReceptor
        {
            get
            {
                return _DepartamentoReceptor;
            }
            set
            {
                _DepartamentoReceptor = value;
            }
        }
        public string DescGlobMonto
        {
            get
            {
                return _DescGlobMonto;
            }
            set
            {
                _DescGlobMonto = value;
            }
        }
        public string Descripcion
        {
            get
            {
                return _Descripcion;
            }
            set
            {
                _Descripcion = value;
            }
        }
        public string DireccionMatriz
        {
            get
            {
                return _DireccionMatriz;
            }
            set
            {
                _DireccionMatriz = value;
            }
        }
        public string DireccionMatrizReceptor
        {
            get
            {
                return _DireccionMatrizReceptor;
            }
            set
            {
                _DireccionMatrizReceptor = value;
            }
        }
        public string DireccionSucursal
        {
            get
            {
                return _DireccionSucursal;
            }
            set
            {
                _DireccionSucursal = value;
            }
        }
        public string DireccionSucursalReceptor
        {
            get
            {
                return _DireccionSucursalReceptor;
            }
            set
            {
                _DireccionSucursalReceptor = value;
            }
        }
        public string DistritoEmisor
        {
            get
            {
                return _DistritoEmisor;
            }
            set
            {
                _DistritoEmisor = value;
            }
        }
        public string DistritoReceptor
        {
            get
            {
                return _DistritoReceptor;
            }
            set
            {
                _DistritoReceptor = value;
            }
        }
        public string DocReference
        {
            get
            {
                return _DocReference;
            }
            set
            {
                _DocReference = value;
            }
        }
        public string DOCversion
        {
            get
            {
                return _DOCversion;
            }
            set
            {
                _DOCversion = value;
            }
        }
        public string FechaAutorizacion
        {
            get
            {
                return _FechaAutorizacion;
            }
            set
            {
                _FechaAutorizacion = value;
            }
        }
        public string FechaAutorizacionEnteFiscal
        {
            get
            {
                return _FechaAutorizacionEnteFiscal;
            }
            set
            {
                _FechaAutorizacionEnteFiscal = value;
            }
        }
        public string FechaEmision
        {
            get
            {
                return _FechaEmision;
            }
            set
            {
                _FechaEmision = value;
                _FechaEmision = _FechaEmision.Replace(".", "");
                _FechaEmision = _FechaEmision.Replace("a m", "am");
                _FechaEmision = _FechaEmision.Replace("p m", "pm");
            }
        }
        public string FirmaDigital
        {
            get
            {
                return _FirmaDigital;
            }
            set
            {
                _FirmaDigital = value;
            }
        }
        public string Folio
        {
            get
            {
                return _Folio;
            }
            set
            {
                _Folio = value;
            }
        }
        public string Identificacion
        {
            get
            {
                return _Identificacion;
            }
            set
            {
                _Identificacion = value;
            }
        }
        public string IdentificacionReceptor
        {
            get
            {
                return _IdentificacionReceptor;
            }
            set
            {
                _IdentificacionReceptor = value;
            }
        }
        public string ImpTotalVenta
        {
            get
            {
                return _ImpTotalVenta;
            }
            set
            {
                _ImpTotalVenta = value;
            }
        }
        public Array InfoAdicional
        {
            get
            {
                return _InfoAdicional;
            }
            set
            {
                _InfoAdicional = value;
            }
        }
        public Array InfoAdicionalEmisor
        {
            get
            {
                return _InfoAdicionalEmisor;
            }
            set
            {
                _InfoAdicionalEmisor = value;
            }
        }
        public Array InfoAdicionalObligatoria
        {
            get
            {
                return _InfoAdicionalObligatoria;
            }
            set
            {
                _InfoAdicionalObligatoria = value;
            }
        }
        public Array InfoAdicionalObligatoriaEmisor
        {
            get
            {
                return _InfoAdicionalObligatoriaEmisor;
            }
            set
            {
                _InfoAdicionalObligatoriaEmisor = value;
            }
        }
        public Array InfoAdicionalObligatoriaReceptor
        {
            get
            {
                return _InfoAdicionalObligatoriaReceptor;
            }
            set
            {
                _InfoAdicionalObligatoriaReceptor = value;
            }
        }
        public Array InfoAdicionalReceptor
        {
            get
            {
                return _InfoAdicionalReceptor;
            }
            set
            {
                _InfoAdicionalReceptor = value;
            }
        }
        public String Linea
        {
            get
            {
                return _Linea;
            }
            set
            {
                _Linea = value;
            }
        }
        public String[] Lines
        {
            get
            {
                return _Lines;
            }
            set
            {
                _Lines = value;
            }
        }
        public byte[] Logo
        {
            get
            {
                return _Logo;
            }
            set
            {
                _Logo = value;
            }
        }
        public byte[] LogoSecundario
        {
            get
            {
                return _LogoSecundario;
            }
            set
            {
                _LogoSecundario = value;
            }
        }
        public string Moneda
        {
            get
            {
                return _Moneda;
            }
            set
            {
                _Moneda = value;
            }
        }
        public string MontoDesc
        {
            get
            {
                return _MontoDesc;
            }
            set
            {
                _MontoDesc = value;
            }
        }
        public string MontoPerc
        {
            get
            {
                return _MontoPerc;
            }
            set
            {
                _MontoPerc = value;
            }
        }
        public string MontoPerc_2
        {
            get
            {
                return _MontoPerc_2;
            }
            set
            {
                _MontoPerc_2 = value;
            }
        }
        public string MontoTotPerc
        {
            get
            {
                return _MontoTotPerc;
            }
            set
            {
                _MontoTotPerc = value;
            }
        }
        public string MontoTotPerc_2
        {
            get
            {
                return _MontoTotPerc_2;
            }
            set
            {
                _MontoTotPerc_2 = value;
            }
        }
        public string MontoValorExo
        {
            get
            {
                return _MontoValorExo;
            }
            set
            {
                _MontoValorExo = value;
            }
        }
        public string MontoValorGrav
        {
            get
            {
                return _MontoValorGrav;
            }
            set
            {
                _MontoValorGrav = value;
            }
        }
        public string MontoValorIna
        {
            get
            {
                return _MontoValorIna;
            }
            set
            {
                _MontoValorIna = value;
            }
        }
        public string MsjArc
        {
            get
            {
                return _MsjArc;
            }
            set
            {
                _MsjArc = value;
            }
        }
        public string NombreComercial
        {
            get
            {
                return _NombreComercial;
            }
            set
            {
                _NombreComercial = value;
            }
        }
        public string NombreComercialReceptor
        {
            get
            {
                return _NombreComercialReceptor;
            }
            set
            {
                _NombreComercialReceptor = value;
            }
        }
        public string NombreComercialSucursal
        {
            get
            {
                return _NombreComercialSucursal;
            }
            set
            {
                _NombreComercialSucursal = value;
            }
        }
        public string CodigoCliente
        {
            get
            {
                return _codigoCliente;
            }
            set
            {
                _codigoCliente = value;
            }
        }
        public string zonaCliente
        {
            get
            {
                return _zonaCliente;
            }
            set
            {
                _zonaCliente = value;
            }
        }
        public string canalCliente
        {
            get
            {
                return _canalCliente;
            }
            set
            {
                _canalCliente = value;
            }
        }

        public string NombreComercialSucursalReceptor
        {
            get
            {
                return _NombreComercialSucursalReceptor;
            }
            set
            {
                _NombreComercialSucursalReceptor = value;
            }
        }
        public string NombreDocumeno
        {
            get
            {
                return _NombreDocumeno;
            }
            set
            {
                _NombreDocumeno = value;
            }
        }
        public string NomTribIGVSum
        {
            get
            {
                return _NomTribIGVSum;
            }
            set
            {
                _NomTribIGVSum = value;
            }
        }
        public string NomTribISCSum
        {
            get
            {
                return _NomTribISCSum;
            }
            set
            {
                _NomTribISCSum = value;
            }
        }
        public string NumeroGuia
        {
            get
            {
                return _NumeroGuia;
            }
            set
            {
                _NumeroGuia = value;
            }
        }
        public string NumeroModificado
        {
            get
            {
                return _NumeroModificado;
            }
            set
            {
                _NumeroModificado = value;
            }
        }
        public int Numlin
        {
            get
            {
                return _Numlin;
            }
            set
            {
                _Numlin = value;
            }
        }
        public string OtrosConceptosPago
        {
            get
            {
                return _OtrosConceptosPago;
            }
            set
            {
                _OtrosConceptosPago = value;
            }
        }
        public string PaisEmisor
        {
            get
            {
                return _PaisEmisor;
            }
            set
            {
                _PaisEmisor = value;
            }
        }
        public string PaisReceptor
        {
            get
            {
                return _PaisReceptor;
            }
            set
            {
                _PaisReceptor = value;
            }
        }
        public string Pedido
        {
            get
            {
                return _Pedido;
            }
            set
            {
                _Pedido = value;
            }
        }
        public string PercCodMonto
        {
            get
            {
                return _PercCodMonto;
            }
            set
            {
                _PercCodMonto = value;
            }
        }
        public string PercCodMonto_2
        {
            get
            {
                return _PercCodMonto_2;
            }
            set
            {
                _PercCodMonto_2 = value;
            }
        }
        public string Placa
        {
            get
            {
                return _Placa;
            }
            set
            {
                _Placa = value;
            }
        }
        public string ProvinciaEmisor
        {
            get
            {
                return _ProvinciaEmisor;
            }
            set
            {
                _ProvinciaEmisor = value;
            }
        }
        public string ProvinciaReceptor
        {
            get
            {
                return _ProvinciaReceptor;
            }
            set
            {
                _ProvinciaReceptor = value;
            }
        }
        public string ptoemi
        {
            get
            {
                return _Ptoemi;
            }
            set
            {
                _Ptoemi = value;
            }
        }
        public string RazonSocial
        {
            get
            {
                return _RazonSocial;
            }
            set
            {
                _RazonSocial = value;
            }
        }
        public string RazonSocialReceptor
        {
            get
            {
                return _RazonSocialReceptor;
            }
            set
            {
                _RazonSocialReceptor = value;
            }
        }
        public string ReferenceID
        {
            get
            {
                return _ReferenceID;
            }
            set
            {
                _ReferenceID = value;
            }
        }
        public string ResponseCode
        {
            get
            {
                return _ResponseCode;
            }
            set
            {
                _ResponseCode = value;
            }
        }
        public string RetCant
        {
            get
            {
                return _RetCant;
            }
            set
            {
                _RetCant = value;
            }
        }
        public string RetCant2
        {
            get
            {
                return _RetCant2;
            }
            set
            {
                _RetCant2 = value;
            }
        }
        public string RetCant3
        {
            get
            {
                return _RetCant3;
            }
            set
            {
                _RetCant3 = value;
            }
        }
        public string RetPeriodo
        {
            get
            {
                return _RetPeriodo;
            }
            set
            {
                _RetPeriodo = value;
            }
        }
        public string RetVal1
        {
            get
            {
                return _RetVal1;
            }
            set
            {
                _RetVal1 = value;
            }
        }
        public string RetVal2
        {
            get
            {
                return _RetVal2;
            }
            set
            {
                _RetVal2 = value;
            }
        }
        public string Rise
        {
            get
            {
                return _Rise;
            }
            set
            {
                _Rise = value;
            }
        }
        public string Secuencial
        {
            get
            {
                return _Secuencial;
            }
            set
            {
                _Secuencial = value;
            }
        }
        public string sello
        {
            get
            {
                return _Sello;
            }
            set
            {
                _Sello = value;
            }
        }
        public string Serie
        {
            get
            {
                return _Serie;
            }
            set
            {
                _Serie = value;
            }
        }
        public string SumIGV
        {
            get
            {
                return _SumIGV;
            }
            set
            {
                _SumIGV = value;
            }
        }
        public string SumISC
        {
            get
            {
                return _SumISC;
            }
            set
            {
                _SumISC = value;
            }
        }
        public string SumOtrCargos
        {
            get
            {
                return _SumOtrCargos;
            }
            set
            {
                _SumOtrCargos = value;
            }
        }
        public string SumOtrTrib
        {
            get
            {
                return _SumOtrTrib;
            }
            set
            {
                _SumOtrTrib = value;
            }
        }
        public string CodOtrTribSum
        {
            get
            {
                return _CodOtrTribSum;
            }
            set
            {
                _CodOtrTribSum = value;
            }
        }
        public string TelefonosMatriz
        {
            get
            {
                return _TelefonosMatriz;
            }
            set
            {
                _TelefonosMatriz = value;
            }
        }
        public string TelefonosMatrizReceptor
        {
            get
            {
                return _TelefonosMatrizReceptor;
            }
            set
            {
                _TelefonosMatrizReceptor = value;
            }
        }
        public string TelefonosSucursal
        {
            get
            {
                return _TelefonosSucursal;
            }
            set
            {
                _TelefonosSucursal = value;
            }
        }
        public string TelefonosSucursalReceptor
        {
            get
            {
                return _TelefonosSucursalReceptor;
            }
            set
            {
                _TelefonosSucursalReceptor = value;
            }
        }
        public string Termino
        {
            get
            {
                return _Termino;
            }
            set
            {
                _Termino = value;
            }
        }
        public string tipo
        {
            get
            {
                return _Tipo;
            }
            set
            {
                _Tipo = value;
            }
        }
        public string TipoEmision
        {
            get
            {
                return _TipoEmision;
            }
            set
            {
                _TipoEmision = value;
            }
        }
        public string TipoEnvio
        {
            get
            {
                return _TipoEnvio;
            }
            set
            {
                _TipoEnvio = value;
            }

        }
        public string TipoGuia
        {
            get
            {
                return _TipoGuia;
            }
            set
            {
                _TipoGuia = value;
            }
        }
        public string TipoIdentificacion
        {
            get
            {
                return _TipoIdentificacion;
            }
            set
            {
                _TipoIdentificacion = value;
            }
        }
        public bool Trgrat
        {
            get
            {
                return _Trgrat;
            }
            set
            {
                _Trgrat = value;
            }
        }
        public string UbigeoReceptor
        {
            get
            {
                return _UbigeoReceptor;
            }
            set
            {
                _UbigeoReceptor = value;
            }
        }
        public string UBLversion
        {
            get
            {
                return _UBLversion;
            }
            set
            {
                _UBLversion = value;
            }
        }
        public string UrbanizacionEmisor
        {
            get
            {
                return _UrbanizacionEmisor;
            }
            set
            {
                _UrbanizacionEmisor = value;
            }
        }
        public string UrbanizacionReceptor
        {
            get
            {
                return _UrbanizacionReceptor;
            }
            set
            {
                _UrbanizacionReceptor = value;
            }
        }
        public string URLWs
        {
            get
            {
                return _URLWs;
            }
            set
            {
                _URLWs = value;
            }
        }
        public string URLWs2
        {
            get
            {
                return _URLWs2;
            }
            set
            {
                _URLWs2 = value;
            }
        }
        public string URLWs3
        {
            get
            {
                return _URLWs3;
            }
            set
            {
                _URLWs3 = value;
            }
        }
        public string URLWs4
        {
            get
            {
                return _URLWs4;
            }
            set
            {
                _URLWs4 = value;
            }
        }
        public string URLWs5
        {
            get
            {
                return _URLWs5;
            }
            set
            {
                _URLWs5 = value;
            }
        }
        public Boolean UseDefaultCredentials
        {
            get
            {
                return _UseDefaultCredentials;
            }
            set
            {
                _UseDefaultCredentials = value;
            }
        }
        public string UserCertificado
        {
            get
            {
                return _UserCertificado;
            }
            set
            {
                _UserCertificado = value;
            }
        }
        public string UserCertificado2
        {
            get
            {
                return _UserCertificado2;
            }
            set
            {
                _UserCertificado2 = value;
            }
        }
        public string UserCertificado3
        {
            get
            {
                return _UserCertificado3;
            }
            set
            {
                _UserCertificado3 = value;
            }
        }
        public string UserENTF
        {
            get
            {
                return _UserENTF;
            }
            set
            {
                _UserENTF = value;
            }
        }
        public string ValTotVentaGrat
        {
            get
            {
                return _ValTotVentaGrat;
            }
            set
            {
                _ValTotVentaGrat = value;
            }
        }
        public string VersionDoc
        {
            get
            {
                return _VersionDoc;
            }
            set
            {
                _VersionDoc = value;
            }

        }
        public string TipoIdentificacionReceptor
        {
            get
            {
                return _TipoIdentificacionReceptor;
            }
            set
            {
                _TipoIdentificacionReceptor = value;
            }
        }
        public string nombreTransportista
        {
            get
            {
                return _nombreTransportista;
            }
            set
            {
                _nombreTransportista = value;
            }
        }

        public string TransportistaId
        {
            get
            {
                return _TransportistaId;
            }
            set
            {
                _TransportistaId = value;
            }
        }
        public string TransportistaRegimen
        {
            get
            {
                return _TransportistaRegimen;
            }
            set
            {
                _TransportistaRegimen = value;
            }
        }
        public string TransportistaDireccion
        {
            get
            {
                return _TransportistaDireccion;
            }
            set
            {
                _TransportistaDireccion = value;
            }
        }

        public string TransportistaMunicipio
        {
            get
            {
                return _TransportistaMunicipio;
            }
            set
            {
                _TransportistaMunicipio = value;
            }
        }
        public string TransportistaDepartamento
        {
            get
            {
                return _TransportistaDepartamento;
            }
            set
            {
                _TransportistaDepartamento = value;
            }
        }

        public string TransportistaPaisId
        {
            get
            {
                return _TransportistaPaisId;
            }
            set
            {
                _TransportistaPaisId = value;
            }
        }
        public string DireccionEntrega
        {
            get
            {
                return _DireccionEntrega;
            }
            set
            {
                _DireccionEntrega = value;
            }
        }
        public string MunicipioEntrega
        {
            get
            {
                return _MunicipioEntrega;
            }
            set
            {
                _MunicipioEntrega = value;
            }
        }
        public string DepartamentoEntrega
        {
            get
            {
                return _DepartamentoEntrega;
            }
            set
            {
                _DepartamentoEntrega = value;
            }
        }
        public string PaisIdEntrega
        {
            get
            {
                return _PaisIdEntrega;
            }
            set
            {
                _PaisIdEntrega = value;
            }
        }
        public string CondicionEntrega
        {
            get
            {
                return _CondicionEntrega;
            }
            set
            {
                _CondicionEntrega = value;
            }
        }
        public string CodigoPostalEntrega
        {
            get
            {
                return _CodigoPostalEntrega;
            }
            set
            {
                _CodigoPostalEntrega = value;
            }
        }
        public string Notas
        {
            get
            {
                return _Notas;
            }
            set
            {
                _Notas = value;
            }
        }

        public string DocUsuario
        {
            get
            {
                return _DocUsuario;
            }
            set
            {
                _DocUsuario = value;
            }
        }
        public string TipoUsuario
        {
            get
            {
                return _TipoUsuario;
            }
            set
            {
                _TipoUsuario = value;
            }
        }
        public string ModalidadesPago
        {
            get
            {
                return _ModalidadesPago;
            }
            set
            {
                _ModalidadesPago = value;
            }
        }
        public string Cobertura
        {
            get
            {
                return _Cobertura;
            }
            set
            {
                _Cobertura = value;
            }
        }

        public string CodigoPrestador
        {
            get
            {
                return _CodigoPrestador;
            }
            set
            {
                _CodigoPrestador = value;
            }
        }
        public string FechainicioFacturacion
        {
            get
            {
                return _FechainicioFacturacion;
            }
            set
            {
                _FechainicioFacturacion = value;
            }
        }
        public string FechafinFacturacion
        {
            get
            {
                return _FechafinFacturacion;
            }
            set
            {
                _FechafinFacturacion = value;
            }
        }
        public string NumeroIdentificacionUsuario
        {
            get
            {
                return _NumeroIdentificacionUsuario;
            }
            set
            {
                _NumeroIdentificacionUsuario = value;
            }
        }
        public string PrimerNombreUser
        {
            get
            {
                return _PrimerNombreUser;
            }
            set
            {
                _PrimerNombreUser = value;
            }
        }
        public string SegundoNombreUser
        {
            get
            {
                return _SegundoNombreUser;
            }
            set
            {
                _SegundoNombreUser = value;
            }
        }
        public string PrimerApellidoUser
        {
            get
            {
                return _PrimerApellidoUser;
            }
            set
            {
                _PrimerApellidoUser = value;
            }
        }
        public string SegundoApellidoUser
        {
            get
            {
                return _SegundoApellidoUser;
            }
            set
            {
                _SegundoApellidoUser = value;
            }
        }
        public string NumeroAutorizacionUser
        {
            get
            {
                return _NumeroAutorizacionUser;
            }
            set
            {
                _NumeroAutorizacionUser = value;
            }
        }
        public string NumeroMIPRES
        {
            get
            {
                return _NumeroMIPRES;
            }
            set
            {
                _NumeroMIPRES = value;
            }
        }
        public string EntregaMIPRES
        {
            get
            {
                return _EntregaMIPRES;
            }
            set
            {
                _EntregaMIPRES = value;
            }
        }
        public string NumeroContratoUser
        {
            get
            {
                return _NumeroContratoUser;
            }
            set
            {
                _NumeroContratoUser = value;
            }
        }
        public string Poliza
        {
            get
            {
                return _Poliza;
            }
            set
            {
                _Poliza = value;
            }
        }
        public string Copago
        {
            get
            {
                return _Copago;
            }
            set
            {
                _Copago = value;
            }
        }
        public string CuotaModeradora
        {
            get
            {
                return _CuotaModeradora;
            }
            set
            {
                _CuotaModeradora = value;
            }
        }
        public string PagosCompartidos
        {
            get
            {
                return _PagosCompartidos;
            }
            set
            {
                _PagosCompartidos = value;
            }
        }
        public string CuotaRecuperacion
        {
            get
            {
                return _CuotaRecuperacion;
            }
            set
            {
                _CuotaRecuperacion = value;
            }
        }
        public string NombreGenEvento
        {
            get
            {
                return _NombreGenEvento;
            }
            set
            {
                _NombreGenEvento = value;
            }
        }
        public string DocGenEvento
        {
            get
            {
                return _DocGenEvento;
            }
            set
            {
                _DocGenEvento = value;
            }
        }
        public string TipoDocGenEvento
        {
            get
            {
                return _TipoDocGenEvento;
            }
            set
            {
                _TipoDocGenEvento = value;
            }
        }
        public string PrimerPago
        {
            get
            {
                return _PrimerPago;
            }
            set
            {
                _PrimerPago = value;
            }
        }
        public string SegundoPago
        {
            get
            {
                return _SegundoPago;
            }
            set
            {
                _SegundoPago = value;
            }
        }
        public string TercerPago
        {
            get
            {
                return _TercerPago;
            }
            set
            {
                _TercerPago = value;
            }
        }
        public string Evento
        {
            get
            {
                return _Evento;
            }
            set
            {
                _Evento = value;
            }
        }
        public string Nota1
        {
            get
            {
                return _Nota1;
            }
            set
            {
                _Nota1 = value;
            }
        }
        public string Nota2
        {
            get
            {
                return _Nota2;
            }
            set
            {
                _Nota2 = value;
            }
        }
        public string TipoDocRef
        {
            get
            {
                return _TipoDocRef;
            }
            set
            {
                _TipoDocRef = value;
            }
        }
        public string CufeRef
        {
            get
            {
                return _CufeRef;
            }
            set
            {
                _CufeRef = value;
            }
        }
        public string VencimientoRef
        {
            get
            {
                return _VencimientoRef;
            }
            set
            {
                _VencimientoRef = value;
            }
        }
        public string NombrePersona
        {
            get
            {
                return _NombrePersona;
            }
            set
            {
                _NombrePersona = value;
            }
        }
        public string TipoPersona
        {
            get
            {
                return _TipoPersona;
            }
            set
            {
                _TipoPersona = value;
            }
        }
        public string TipoDocPersona
        {
            get
            {
                return _TipoDocPersona;
            }
            set
            {
                _TipoDocPersona = value;
            }
        }
        public string DocPersona
        {
            get
            {
                return _DocPersona;
            }
            set
            {
                _DocPersona = value;
            }
        }
        public string DVPersona
        {
            get
            {
                return _DVPersona;
            }
            set
            {
                _DVPersona = value;
            }
        }
        public string Factor
        {
            get
            {
                return _Factor;
            }
            set
            {
                _Factor = value;
            }
        }

        public string DocumentoProceso
        {
            get
            {
                return _DocumentoProceso;
            }
            set
            {
                _DocumentoProceso = value;
            }
        }

        public string Juez
        {
            get
            {
                return _Juez;
            }
            set
            {
                _Juez = value;
            }
        }

        public string IdentificacionJuez
        {
            get
            {
                return _IdentificacionJuez;
            }
            set
            {
                _IdentificacionJuez = value;
            }
        }

        public string TipoIdentificacionJuez
        {
            get
            {
                return _TipoIdentificacionJuez;
            }
            set
            {
                _TipoIdentificacionJuez = value;
            }
        }

        public string TipoPersonaJuez
        {
            get
            {
                return _TipoPersonaJuez;
            }
            set
            {
                _TipoPersonaJuez = value;
            }
        }

        public string TipoDocReferenciado
        {
            get
            {
                return _TipoDocReferenciado;
            }
            set
            {
                _TipoDocReferenciado = value;
            }
        }
        public string CufeReferenciado
        {
            get
            {
                return _CufeReferenciado;
            }
            set
            {
                _CufeReferenciado = value;
            }
        }
        public string VencimientoReferenciado
        {
            get
            {
                return _VencimientoReferenciado;
            }
            set
            {
                _VencimientoReferenciado = value;
            }
        }
        public string tipoPersonaEmi
        {
            get
            {
                return _tipoPersonaEmi;
            }
            set
            {
                _tipoPersonaEmi = value;
            }
        }
        public string tipoPersonaRece
        {
            get
            {
                return _tipoPersonaRece;
            }
            set
            {
                _tipoPersonaRece = value;
            }
        }
        public string Issuer
        {
            get
            {
                return _Issuer;
            }
            set
            {
                _Issuer = value;
            }
        }
        public string TipoPersonaIssuer
        {
            get
            {
                return _TipoPersonaIssuer;
            }
            set
            {
                _TipoPersonaIssuer = value;
            }
        }

        public string TipoDocIssuer
        {
            get
            {
                return _TipoDocIssuer;
            }
            set
            {
                _TipoDocIssuer = value;
            }
        }
        public string NitIssuer
        {
            get
            {
                return _NitIssuer;
            }
            set
            {
                _NitIssuer = value;
            }
        }

        public string DVIssuer
        {
            get
            {
                return _DVIssuer;
            }
            set
            {
                _DVIssuer = value;
            }
        }
        public string TipoOperacion
        {
            get
            {
                return _TipoOperacion;
            }
            set
            {
                _TipoOperacion = value;
            }
        }
        public string numeroAutorizacion
        {
            get { return _numeroAutorizacion; }
            set { _numeroAutorizacion = value; }
        }
        #endregion

        #region Byte to String y Reversion
        /// <summary>
        /// Convierte un Byte[] a un String
        /// </summary>
        /// <param name="Bytee">Byte[] a convertir en String</param>
        /// <returns></returns>
        public string ToString(Byte[] Bytee)
        {
            string s3 = Convert.ToBase64String(Bytee);// gsjqFw==
            return s3;
        }
        /// <summary>
        /// Revierte la conversion de un Byte(String) a un Byte[]
        /// </summary>
        /// <param name="Stringg">Byte[] convertido en String</param>
        /// <returns></returns>
        public Byte[] ToByte(String Stringg)
        {
            byte[] decByte3 = Convert.FromBase64String(Stringg);// decByte3 same as bytes
            return decByte3;
        }
        public XmlDocument Xml
        {
            get
            {
                return xml;
            }
            set
            {
                xml = value;
            }
        }
        #endregion

        public void pasarValor()
        {

            foreach (string[] a in _MetodoPago)
            {
                foreach (var b in a)
                {
                    _metodoUnico = b;
                }
            }

        }
        public ArrayList ArrayCargos
        {
            get { return _ArrayCargos; }
            set { _ArrayCargos = value; }
        }
    }
}
