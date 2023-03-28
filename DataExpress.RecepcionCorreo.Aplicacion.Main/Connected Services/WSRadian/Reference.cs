﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WSRadian
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WSRadian.websRadianSoap")]
    public interface websRadianSoap
    {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento CUDE del espacio de nombres http://tempuri.org/ no está marcado para convertirse en valor nulo
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Eventos", ReplyAction="*")]
        WSRadian.EventosResponse Eventos(WSRadian.EventosRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Eventos", ReplyAction="*")]
        System.Threading.Tasks.Task<WSRadian.EventosResponse> EventosAsync(WSRadian.EventosRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class EventosRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Eventos", Namespace="http://tempuri.org/", Order=0)]
        public WSRadian.EventosRequestBody Body;
        
        public EventosRequest()
        {
        }
        
        public EventosRequest(WSRadian.EventosRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class EventosRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string CUDE;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string NIT;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string codAcuse;
        
        public EventosRequestBody()
        {
        }
        
        public EventosRequestBody(string CUDE, string NIT, string codAcuse)
        {
            this.CUDE = CUDE;
            this.NIT = NIT;
            this.codAcuse = codAcuse;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class EventosResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="EventosResponse", Namespace="http://tempuri.org/", Order=0)]
        public WSRadian.EventosResponseBody Body;
        
        public EventosResponse()
        {
        }
        
        public EventosResponse(WSRadian.EventosResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class EventosResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string EventosResult;
        
        public EventosResponseBody()
        {
        }
        
        public EventosResponseBody(string EventosResult)
        {
            this.EventosResult = EventosResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public interface websRadianSoapChannel : WSRadian.websRadianSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public partial class websRadianSoapClient : System.ServiceModel.ClientBase<WSRadian.websRadianSoap>, WSRadian.websRadianSoap
    {
        
        /// <summary>
        /// Implemente este método parcial para configurar el punto de conexión de servicio.
        /// </summary>
        /// <param name="serviceEndpoint">El punto de conexión para configurar</param>
        /// <param name="clientCredentials">Credenciales de cliente</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public websRadianSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(websRadianSoapClient.GetBindingForEndpoint(endpointConfiguration), websRadianSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public websRadianSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(websRadianSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public websRadianSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(websRadianSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public websRadianSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WSRadian.EventosResponse WSRadian.websRadianSoap.Eventos(WSRadian.EventosRequest request)
        {
            return base.Channel.Eventos(request);
        }
        
        public string Eventos(string CUDE, string NIT, string codAcuse)
        {
            WSRadian.EventosRequest inValue = new WSRadian.EventosRequest();
            inValue.Body = new WSRadian.EventosRequestBody();
            inValue.Body.CUDE = CUDE;
            inValue.Body.NIT = NIT;
            inValue.Body.codAcuse = codAcuse;
            WSRadian.EventosResponse retVal = ((WSRadian.websRadianSoap)(this)).Eventos(inValue);
            return retVal.Body.EventosResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WSRadian.EventosResponse> WSRadian.websRadianSoap.EventosAsync(WSRadian.EventosRequest request)
        {
            return base.Channel.EventosAsync(request);
        }
        
        public System.Threading.Tasks.Task<WSRadian.EventosResponse> EventosAsync(string CUDE, string NIT, string codAcuse)
        {
            WSRadian.EventosRequest inValue = new WSRadian.EventosRequest();
            inValue.Body = new WSRadian.EventosRequestBody();
            inValue.Body.CUDE = CUDE;
            inValue.Body.NIT = NIT;
            inValue.Body.codAcuse = codAcuse;
            return ((WSRadian.websRadianSoap)(this)).EventosAsync(inValue);
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
            if ((endpointConfiguration == EndpointConfiguration.websRadianSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.websRadianSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpsTransportBindingElement httpsBindingElement = new System.ServiceModel.Channels.HttpsTransportBindingElement();
                httpsBindingElement.AllowCookies = true;
                httpsBindingElement.MaxBufferSize = int.MaxValue;
                httpsBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpsBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.websRadianSoap))
            {
                return new System.ServiceModel.EndpointAddress("https://democolombiav2.ipsofactu.co/core/XdService.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.websRadianSoap12))
            {
                return new System.ServiceModel.EndpointAddress("https://democolombiav2.ipsofactu.co/core/XdService.asmx");
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            websRadianSoap,
            
            websRadianSoap12,
        }
    }
}
