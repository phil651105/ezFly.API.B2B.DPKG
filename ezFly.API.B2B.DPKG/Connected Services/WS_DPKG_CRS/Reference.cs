//------------------------------------------------------------------------------
// <自動產生>
//     這段程式碼是由工具產生的。
//     //
//     變更此檔案可能會導致不正確的行為，而且若已重新產生
//     程式碼，則會遺失變更。
// </自動產生>
//------------------------------------------------------------------------------

namespace WS_DPKG_CRS
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WS_DPKG_CRS.CrsServiceSoap")]
    public interface CrsServiceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAv", ReplyAction="*")]
        System.Threading.Tasks.Task<WS_DPKG_CRS.GetAvResponse> GetAvAsync(WS_DPKG_CRS.GetAvRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CreatePNR", ReplyAction="*")]
        System.Threading.Tasks.Task<WS_DPKG_CRS.CreatePNRResponse> CreatePNRAsync(WS_DPKG_CRS.CreatePNRRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/OpenTkt", ReplyAction="*")]
        System.Threading.Tasks.Task<WS_DPKG_CRS.OpenTktResponse> OpenTktAsync(WS_DPKG_CRS.OpenTktRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DisplayPNR", ReplyAction="*")]
        System.Threading.Tasks.Task<WS_DPKG_CRS.DisplayPNRResponse> DisplayPNRAsync(WS_DPKG_CRS.DisplayPNRRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CancelPNR", ReplyAction="*")]
        System.Threading.Tasks.Task<WS_DPKG_CRS.CancelPNRResponse> CancelPNRAsync(WS_DPKG_CRS.CancelPNRRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CancelETKT", ReplyAction="*")]
        System.Threading.Tasks.Task<WS_DPKG_CRS.CancelETKTResponse> CancelETKTAsync(WS_DPKG_CRS.CancelETKTRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAvRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAv", Namespace="http://tempuri.org/", Order=0)]
        public WS_DPKG_CRS.GetAvRequestBody Body;
        
        public GetAvRequest()
        {
        }
        
        public GetAvRequest(WS_DPKG_CRS.GetAvRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetAvRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string xml;
        
        public GetAvRequestBody()
        {
        }
        
        public GetAvRequestBody(string xml)
        {
            this.xml = xml;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAvResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAvResponse", Namespace="http://tempuri.org/", Order=0)]
        public WS_DPKG_CRS.GetAvResponseBody Body;
        
        public GetAvResponse()
        {
        }
        
        public GetAvResponse(WS_DPKG_CRS.GetAvResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetAvResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetAvResult;
        
        public GetAvResponseBody()
        {
        }
        
        public GetAvResponseBody(string GetAvResult)
        {
            this.GetAvResult = GetAvResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CreatePNRRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CreatePNR", Namespace="http://tempuri.org/", Order=0)]
        public WS_DPKG_CRS.CreatePNRRequestBody Body;
        
        public CreatePNRRequest()
        {
        }
        
        public CreatePNRRequest(WS_DPKG_CRS.CreatePNRRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CreatePNRRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string xml;
        
        public CreatePNRRequestBody()
        {
        }
        
        public CreatePNRRequestBody(string xml)
        {
            this.xml = xml;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CreatePNRResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CreatePNRResponse", Namespace="http://tempuri.org/", Order=0)]
        public WS_DPKG_CRS.CreatePNRResponseBody Body;
        
        public CreatePNRResponse()
        {
        }
        
        public CreatePNRResponse(WS_DPKG_CRS.CreatePNRResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CreatePNRResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string CreatePNRResult;
        
        public CreatePNRResponseBody()
        {
        }
        
        public CreatePNRResponseBody(string CreatePNRResult)
        {
            this.CreatePNRResult = CreatePNRResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class OpenTktRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="OpenTkt", Namespace="http://tempuri.org/", Order=0)]
        public WS_DPKG_CRS.OpenTktRequestBody Body;
        
        public OpenTktRequest()
        {
        }
        
        public OpenTktRequest(WS_DPKG_CRS.OpenTktRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class OpenTktRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string xml;
        
        public OpenTktRequestBody()
        {
        }
        
        public OpenTktRequestBody(string xml)
        {
            this.xml = xml;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class OpenTktResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="OpenTktResponse", Namespace="http://tempuri.org/", Order=0)]
        public WS_DPKG_CRS.OpenTktResponseBody Body;
        
        public OpenTktResponse()
        {
        }
        
        public OpenTktResponse(WS_DPKG_CRS.OpenTktResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class OpenTktResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string OpenTktResult;
        
        public OpenTktResponseBody()
        {
        }
        
        public OpenTktResponseBody(string OpenTktResult)
        {
            this.OpenTktResult = OpenTktResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DisplayPNRRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="DisplayPNR", Namespace="http://tempuri.org/", Order=0)]
        public WS_DPKG_CRS.DisplayPNRRequestBody Body;
        
        public DisplayPNRRequest()
        {
        }
        
        public DisplayPNRRequest(WS_DPKG_CRS.DisplayPNRRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class DisplayPNRRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string xml;
        
        public DisplayPNRRequestBody()
        {
        }
        
        public DisplayPNRRequestBody(string xml)
        {
            this.xml = xml;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DisplayPNRResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="DisplayPNRResponse", Namespace="http://tempuri.org/", Order=0)]
        public WS_DPKG_CRS.DisplayPNRResponseBody Body;
        
        public DisplayPNRResponse()
        {
        }
        
        public DisplayPNRResponse(WS_DPKG_CRS.DisplayPNRResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class DisplayPNRResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string DisplayPNRResult;
        
        public DisplayPNRResponseBody()
        {
        }
        
        public DisplayPNRResponseBody(string DisplayPNRResult)
        {
            this.DisplayPNRResult = DisplayPNRResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CancelPNRRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CancelPNR", Namespace="http://tempuri.org/", Order=0)]
        public WS_DPKG_CRS.CancelPNRRequestBody Body;
        
        public CancelPNRRequest()
        {
        }
        
        public CancelPNRRequest(WS_DPKG_CRS.CancelPNRRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CancelPNRRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string xml;
        
        public CancelPNRRequestBody()
        {
        }
        
        public CancelPNRRequestBody(string xml)
        {
            this.xml = xml;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CancelPNRResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CancelPNRResponse", Namespace="http://tempuri.org/", Order=0)]
        public WS_DPKG_CRS.CancelPNRResponseBody Body;
        
        public CancelPNRResponse()
        {
        }
        
        public CancelPNRResponse(WS_DPKG_CRS.CancelPNRResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CancelPNRResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string CancelPNRResult;
        
        public CancelPNRResponseBody()
        {
        }
        
        public CancelPNRResponseBody(string CancelPNRResult)
        {
            this.CancelPNRResult = CancelPNRResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CancelETKTRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CancelETKT", Namespace="http://tempuri.org/", Order=0)]
        public WS_DPKG_CRS.CancelETKTRequestBody Body;
        
        public CancelETKTRequest()
        {
        }
        
        public CancelETKTRequest(WS_DPKG_CRS.CancelETKTRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CancelETKTRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string xml;
        
        public CancelETKTRequestBody()
        {
        }
        
        public CancelETKTRequestBody(string xml)
        {
            this.xml = xml;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CancelETKTResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CancelETKTResponse", Namespace="http://tempuri.org/", Order=0)]
        public WS_DPKG_CRS.CancelETKTResponseBody Body;
        
        public CancelETKTResponse()
        {
        }
        
        public CancelETKTResponse(WS_DPKG_CRS.CancelETKTResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CancelETKTResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string CancelETKTResult;
        
        public CancelETKTResponseBody()
        {
        }
        
        public CancelETKTResponseBody(string CancelETKTResult)
        {
            this.CancelETKTResult = CancelETKTResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public interface CrsServiceSoapChannel : WS_DPKG_CRS.CrsServiceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public partial class CrsServiceSoapClient : System.ServiceModel.ClientBase<WS_DPKG_CRS.CrsServiceSoap>, WS_DPKG_CRS.CrsServiceSoap
    {
        
    /// <summary>
    /// 實作此部分方法來設定服務端點。
    /// </summary>
    /// <param name="serviceEndpoint">要設定的端點</param>
    /// <param name="clientCredentials">用戶端認證</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public CrsServiceSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(CrsServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), CrsServiceSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CrsServiceSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(CrsServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CrsServiceSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(CrsServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CrsServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WS_DPKG_CRS.GetAvResponse> WS_DPKG_CRS.CrsServiceSoap.GetAvAsync(WS_DPKG_CRS.GetAvRequest request)
        {
            return base.Channel.GetAvAsync(request);
        }
        
        public System.Threading.Tasks.Task<WS_DPKG_CRS.GetAvResponse> GetAvAsync(string xml)
        {
            WS_DPKG_CRS.GetAvRequest inValue = new WS_DPKG_CRS.GetAvRequest();
            inValue.Body = new WS_DPKG_CRS.GetAvRequestBody();
            inValue.Body.xml = xml;
            return ((WS_DPKG_CRS.CrsServiceSoap)(this)).GetAvAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WS_DPKG_CRS.CreatePNRResponse> WS_DPKG_CRS.CrsServiceSoap.CreatePNRAsync(WS_DPKG_CRS.CreatePNRRequest request)
        {
            return base.Channel.CreatePNRAsync(request);
        }
        
        public System.Threading.Tasks.Task<WS_DPKG_CRS.CreatePNRResponse> CreatePNRAsync(string xml)
        {
            WS_DPKG_CRS.CreatePNRRequest inValue = new WS_DPKG_CRS.CreatePNRRequest();
            inValue.Body = new WS_DPKG_CRS.CreatePNRRequestBody();
            inValue.Body.xml = xml;
            return ((WS_DPKG_CRS.CrsServiceSoap)(this)).CreatePNRAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WS_DPKG_CRS.OpenTktResponse> WS_DPKG_CRS.CrsServiceSoap.OpenTktAsync(WS_DPKG_CRS.OpenTktRequest request)
        {
            return base.Channel.OpenTktAsync(request);
        }
        
        public System.Threading.Tasks.Task<WS_DPKG_CRS.OpenTktResponse> OpenTktAsync(string xml)
        {
            WS_DPKG_CRS.OpenTktRequest inValue = new WS_DPKG_CRS.OpenTktRequest();
            inValue.Body = new WS_DPKG_CRS.OpenTktRequestBody();
            inValue.Body.xml = xml;
            return ((WS_DPKG_CRS.CrsServiceSoap)(this)).OpenTktAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WS_DPKG_CRS.DisplayPNRResponse> WS_DPKG_CRS.CrsServiceSoap.DisplayPNRAsync(WS_DPKG_CRS.DisplayPNRRequest request)
        {
            return base.Channel.DisplayPNRAsync(request);
        }
        
        public System.Threading.Tasks.Task<WS_DPKG_CRS.DisplayPNRResponse> DisplayPNRAsync(string xml)
        {
            WS_DPKG_CRS.DisplayPNRRequest inValue = new WS_DPKG_CRS.DisplayPNRRequest();
            inValue.Body = new WS_DPKG_CRS.DisplayPNRRequestBody();
            inValue.Body.xml = xml;
            return ((WS_DPKG_CRS.CrsServiceSoap)(this)).DisplayPNRAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WS_DPKG_CRS.CancelPNRResponse> WS_DPKG_CRS.CrsServiceSoap.CancelPNRAsync(WS_DPKG_CRS.CancelPNRRequest request)
        {
            return base.Channel.CancelPNRAsync(request);
        }
        
        public System.Threading.Tasks.Task<WS_DPKG_CRS.CancelPNRResponse> CancelPNRAsync(string xml)
        {
            WS_DPKG_CRS.CancelPNRRequest inValue = new WS_DPKG_CRS.CancelPNRRequest();
            inValue.Body = new WS_DPKG_CRS.CancelPNRRequestBody();
            inValue.Body.xml = xml;
            return ((WS_DPKG_CRS.CrsServiceSoap)(this)).CancelPNRAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WS_DPKG_CRS.CancelETKTResponse> WS_DPKG_CRS.CrsServiceSoap.CancelETKTAsync(WS_DPKG_CRS.CancelETKTRequest request)
        {
            return base.Channel.CancelETKTAsync(request);
        }
        
        public System.Threading.Tasks.Task<WS_DPKG_CRS.CancelETKTResponse> CancelETKTAsync(string xml)
        {
            WS_DPKG_CRS.CancelETKTRequest inValue = new WS_DPKG_CRS.CancelETKTRequest();
            inValue.Body = new WS_DPKG_CRS.CancelETKTRequestBody();
            inValue.Body.xml = xml;
            return ((WS_DPKG_CRS.CrsServiceSoap)(this)).CancelETKTAsync(inValue);
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
            if ((endpointConfiguration == EndpointConfiguration.CrsServiceSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.CrsServiceSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("找不到名為 \'{0}\' 的端點。", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.CrsServiceSoap))
            {
                return new System.ServiceModel.EndpointAddress("http://ews.ezfly.com/EZFLY.DPKG.CRS.WS/CrsService.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.CrsServiceSoap12))
            {
                return new System.ServiceModel.EndpointAddress("http://ews.ezfly.com/EZFLY.DPKG.CRS.WS/CrsService.asmx");
            }
            throw new System.InvalidOperationException(string.Format("找不到名為 \'{0}\' 的端點。", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            CrsServiceSoap,
            
            CrsServiceSoap12,
        }
    }
}
