﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WsColombia
{
    using DataExpress.RecepcionCorreo.Aplicacion.DTO;
    using System;
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfString", Namespace="http://tempuri.org/", ItemName="string")]
    public class ArrayOfString : System.Collections.Generic.List<string>
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfAnyType", Namespace="http://tempuri.org/", ItemName="anyType")]
    public class ArrayOfAnyType : System.Collections.Generic.List<object>
    {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WsColombia.XdServiceSoap")]
    public interface XdServiceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/recibeInfo", ReplyAction="*")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(WsColombia.ArrayOfString))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(WsColombia.ArrayOfAnyType[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(WsColombia.ArrayOfAnyType))]
        System.Threading.Tasks.Task<WsColombia.recibeInfoResponse> recibeInfoAsync(WsColombia.recibeInfoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/recibeInfoS", ReplyAction="*")]
        System.Threading.Tasks.Task<WsColombia.recibeInfoSResponse> recibeInfoSAsync(WsColombia.recibeInfoSRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/recibeInfoSXML", ReplyAction="*")]
        System.Threading.Tasks.Task<WsColombia.recibeInfoSXMLResponse> recibeInfoSXMLAsync(WsColombia.recibeInfoSXMLRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/RecibeJson", ReplyAction="*")]
        System.Threading.Tasks.Task<WsColombia.RecibeJsonResponse> RecibeJsonAsync(WsColombia.RecibeJsonRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class recibeInfoRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="recibeInfo", Namespace="http://tempuri.org/", Order=0)]
        public WsColombia.recibeInfoRequestBody Body;
        
        public recibeInfoRequest()
        {
        }
        
        public recibeInfoRequest(WsColombia.recibeInfoRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class recibeInfoRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public WsColombia.ArrayOfString Trama;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Extencion;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Emisor;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string Pais;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string Usuario;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string Contraseña;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string TipoProceso;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string RG;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public WsColombia.ArrayOfAnyType[] AditionalFiles;
        
        public recibeInfoRequestBody()
        {
        }
        
        public recibeInfoRequestBody(WsColombia.ArrayOfString Trama, string Extencion, string Emisor, string Pais, string Usuario, string Contraseña, string TipoProceso, string RG, WsColombia.ArrayOfAnyType[] AditionalFiles)
        {
            this.Trama = Trama;
            this.Extencion = Extencion;
            this.Emisor = Emisor;
            this.Pais = Pais;
            this.Usuario = Usuario;
            this.Contraseña = Contraseña;
            this.TipoProceso = TipoProceso;
            this.RG = RG;
            this.AditionalFiles = AditionalFiles;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class recibeInfoResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="recibeInfoResponse", Namespace="http://tempuri.org/", Order=0)]
        public WsColombia.recibeInfoResponseBody Body;
        
        public recibeInfoResponse()
        {
        }
        
        public recibeInfoResponse(WsColombia.recibeInfoResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class recibeInfoResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.XmlElement recibeInfoResult;
        
        public recibeInfoResponseBody()
        {
        }
        
        public recibeInfoResponseBody(System.Xml.XmlElement recibeInfoResult)
        {
            this.recibeInfoResult = recibeInfoResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class recibeInfoSRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="recibeInfoS", Namespace="http://tempuri.org/", Order=0)]
        public WsColombia.recibeInfoSRequestBody Body;
        
        public recibeInfoSRequest()
        {
        }
        
        public recibeInfoSRequest(WsColombia.recibeInfoSRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class recibeInfoSRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string info;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string RG;
        
        public recibeInfoSRequestBody()
        {
        }
        
        public recibeInfoSRequestBody(string info, string RG)
        {
            this.info = info;
            this.RG = RG;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class recibeInfoSResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="recibeInfoSResponse", Namespace="http://tempuri.org/", Order=0)]
        public WsColombia.recibeInfoSResponseBody Body;
        
        public recibeInfoSResponse()
        {
        }
        
        public recibeInfoSResponse(WsColombia.recibeInfoSResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class recibeInfoSResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.XmlElement recibeInfoSResult;
        
        public recibeInfoSResponseBody()
        {
        }
        
        public recibeInfoSResponseBody(System.Xml.XmlElement recibeInfoSResult)
        {
            this.recibeInfoSResult = recibeInfoSResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class recibeInfoSXMLRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="recibeInfoSXML", Namespace="http://tempuri.org/", Order=0)]
        public WsColombia.recibeInfoSXMLRequestBody Body;
        
        public recibeInfoSXMLRequest()
        {
        }
        
        public recibeInfoSXMLRequest(WsColombia.recibeInfoSXMLRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class recibeInfoSXMLRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string info;
        
        public recibeInfoSXMLRequestBody()
        {
        }
        
        public recibeInfoSXMLRequestBody(string info)
        {
            this.info = info;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class recibeInfoSXMLResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="recibeInfoSXMLResponse", Namespace="http://tempuri.org/", Order=0)]
        public WsColombia.recibeInfoSXMLResponseBody Body;
        
        public recibeInfoSXMLResponse()
        {
        }
        
        public recibeInfoSXMLResponse(WsColombia.recibeInfoSXMLResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class recibeInfoSXMLResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.XmlElement recibeInfoSXMLResult;
        
        public recibeInfoSXMLResponseBody()
        {
        }
        
        public recibeInfoSXMLResponseBody(System.Xml.XmlElement recibeInfoSXMLResult)
        {
            this.recibeInfoSXMLResult = recibeInfoSXMLResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RecibeJsonRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RecibeJson", Namespace="http://tempuri.org/", Order=0)]
        public WsColombia.RecibeJsonRequestBody Body;
        
        public RecibeJsonRequest()
        {
        }
        
        public RecibeJsonRequest(WsColombia.RecibeJsonRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class RecibeJsonRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string JsonR;
        
        public RecibeJsonRequestBody()
        {
        }
        
        public RecibeJsonRequestBody(string JsonR)
        {
            this.JsonR = JsonR;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RecibeJsonResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RecibeJsonResponse", Namespace="http://tempuri.org/", Order=0)]
        public WsColombia.RecibeJsonResponseBody Body;
        
        public RecibeJsonResponse()
        {
        }
        
        public RecibeJsonResponse(WsColombia.RecibeJsonResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class RecibeJsonResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.XmlElement RecibeJsonResult;
        
        public RecibeJsonResponseBody()
        {
        }
        
        public RecibeJsonResponseBody(System.Xml.XmlElement RecibeJsonResult)
        {
            this.RecibeJsonResult = RecibeJsonResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public interface XdServiceSoapChannel : WsColombia.XdServiceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public partial class XdServiceSoapClient : System.ServiceModel.ClientBase<WsColombia.XdServiceSoap>, WsColombia.XdServiceSoap
    {
        
        /// <summary>
        /// Implemente este método parcial para configurar el punto de conexión de servicio.
        /// </summary>
        /// <param name="serviceEndpoint">El punto de conexión para configurar</param>
        /// <param name="clientCredentials">Credenciales de cliente</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public XdServiceSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(XdServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), XdServiceSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public XdServiceSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(XdServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public XdServiceSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(XdServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public XdServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WsColombia.recibeInfoResponse> WsColombia.XdServiceSoap.recibeInfoAsync(WsColombia.recibeInfoRequest request)
        {
            return base.Channel.recibeInfoAsync(request);
        }
        
        public System.Threading.Tasks.Task<WsColombia.recibeInfoResponse> recibeInfoAsync(WsColombia.ArrayOfString Trama, string Extencion, string Emisor, string Pais, string Usuario, string Contraseña, string TipoProceso, string RG, WsColombia.ArrayOfAnyType[] AditionalFiles)
        {
            WsColombia.recibeInfoRequest inValue = new WsColombia.recibeInfoRequest();
            inValue.Body = new WsColombia.recibeInfoRequestBody();
            inValue.Body.Trama = Trama;
            inValue.Body.Extencion = Extencion;
            inValue.Body.Emisor = Emisor;
            inValue.Body.Pais = Pais;
            inValue.Body.Usuario = Usuario;
            inValue.Body.Contraseña = Contraseña;
            inValue.Body.TipoProceso = TipoProceso;
            inValue.Body.RG = RG;
            inValue.Body.AditionalFiles = AditionalFiles;
            return ((WsColombia.XdServiceSoap)(this)).recibeInfoAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WsColombia.recibeInfoSResponse> WsColombia.XdServiceSoap.recibeInfoSAsync(WsColombia.recibeInfoSRequest request)
        {
            return base.Channel.recibeInfoSAsync(request);
        }
        
        public System.Threading.Tasks.Task<WsColombia.recibeInfoSResponse> recibeInfoSAsync(string info, string RG)
        {
            WsColombia.recibeInfoSRequest inValue = new WsColombia.recibeInfoSRequest();
            inValue.Body = new WsColombia.recibeInfoSRequestBody();
            inValue.Body.info = info;
            inValue.Body.RG = RG;
            return ((WsColombia.XdServiceSoap)(this)).recibeInfoSAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WsColombia.recibeInfoSXMLResponse> WsColombia.XdServiceSoap.recibeInfoSXMLAsync(WsColombia.recibeInfoSXMLRequest request)
        {
            return base.Channel.recibeInfoSXMLAsync(request);
        }
        
        public System.Threading.Tasks.Task<WsColombia.recibeInfoSXMLResponse> recibeInfoSXMLAsync(string info)
        {
            WsColombia.recibeInfoSXMLRequest inValue = new WsColombia.recibeInfoSXMLRequest();
            inValue.Body = new WsColombia.recibeInfoSXMLRequestBody();
            inValue.Body.info = info;
            return ((WsColombia.XdServiceSoap)(this)).recibeInfoSXMLAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WsColombia.RecibeJsonResponse> WsColombia.XdServiceSoap.RecibeJsonAsync(WsColombia.RecibeJsonRequest request)
        {
            return base.Channel.RecibeJsonAsync(request);
        }
        
        public System.Threading.Tasks.Task<WsColombia.RecibeJsonResponse> RecibeJsonAsync(string JsonR)
        {
            WsColombia.RecibeJsonRequest inValue = new WsColombia.RecibeJsonRequest();
            inValue.Body = new WsColombia.RecibeJsonRequestBody();
            inValue.Body.JsonR = JsonR;
            return ((WsColombia.XdServiceSoap)(this)).RecibeJsonAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.XdServiceSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                result.ReceiveTimeout = TimeSpan.FromMinutes(3); 
                result.SendTimeout = TimeSpan.FromMinutes(3);
                result.OpenTimeout = TimeSpan.FromMinutes(3);
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.XdServiceSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpsTransportBindingElement httpsBindingElement = new System.ServiceModel.Channels.HttpsTransportBindingElement();
                httpsBindingElement.AllowCookies = true;
                httpsBindingElement.MaxBufferSize = int.MaxValue;
                httpsBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.ReceiveTimeout = TimeSpan.FromMinutes(3);
                result.SendTimeout = TimeSpan.FromMinutes(3);
                result.OpenTimeout = TimeSpan.FromMinutes(3);

                result.Elements.Add(httpsBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.XdServiceSoap))
            {
                return new System.ServiceModel.EndpointAddress("https://democolombiav2.ipsofactu.co/core/XdService.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.XdServiceSoap12))
            {
                return new System.ServiceModel.EndpointAddress("https://democolombiav2.ipsofactu.co/core/XdService.asmx");
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            XdServiceSoap,
            
            XdServiceSoap12,
        }
    }
}
