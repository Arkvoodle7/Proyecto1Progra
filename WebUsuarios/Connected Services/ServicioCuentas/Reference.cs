﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebUsuarios.ServicioCuentas {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Cuentas", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class Cuentas : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NumeroCuentaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NombreUsuarioField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TipoCuentaField;
        
        private decimal SaldoField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string NumeroCuenta {
            get {
                return this.NumeroCuentaField;
            }
            set {
                if ((object.ReferenceEquals(this.NumeroCuentaField, value) != true)) {
                    this.NumeroCuentaField = value;
                    this.RaisePropertyChanged("NumeroCuenta");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string NombreUsuario {
            get {
                return this.NombreUsuarioField;
            }
            set {
                if ((object.ReferenceEquals(this.NombreUsuarioField, value) != true)) {
                    this.NombreUsuarioField = value;
                    this.RaisePropertyChanged("NombreUsuario");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string TipoCuenta {
            get {
                return this.TipoCuentaField;
            }
            set {
                if ((object.ReferenceEquals(this.TipoCuentaField, value) != true)) {
                    this.TipoCuentaField = value;
                    this.RaisePropertyChanged("TipoCuenta");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public decimal Saldo {
            get {
                return this.SaldoField;
            }
            set {
                if ((this.SaldoField.Equals(value) != true)) {
                    this.SaldoField = value;
                    this.RaisePropertyChanged("Saldo");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicioCuentas.WebServiceAD_CuentasSoap")]
    public interface WebServiceAD_CuentasSoap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento numero_cuenta del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CrearCuetna", ReplyAction="*")]
        WebUsuarios.ServicioCuentas.CrearCuetnaResponse CrearCuetna(WebUsuarios.ServicioCuentas.CrearCuetnaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CrearCuetna", ReplyAction="*")]
        System.Threading.Tasks.Task<WebUsuarios.ServicioCuentas.CrearCuetnaResponse> CrearCuetnaAsync(WebUsuarios.ServicioCuentas.CrearCuetnaRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento nombre_usuario del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarCuentas", ReplyAction="*")]
        WebUsuarios.ServicioCuentas.ListarCuentasResponse ListarCuentas(WebUsuarios.ServicioCuentas.ListarCuentasRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarCuentas", ReplyAction="*")]
        System.Threading.Tasks.Task<WebUsuarios.ServicioCuentas.ListarCuentasResponse> ListarCuentasAsync(WebUsuarios.ServicioCuentas.ListarCuentasRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento nombreUsuario del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ObtenerTelefonoUsuario", ReplyAction="*")]
        WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioResponse ObtenerTelefonoUsuario(WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ObtenerTelefonoUsuario", ReplyAction="*")]
        System.Threading.Tasks.Task<WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioResponse> ObtenerTelefonoUsuarioAsync(WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CrearCuetnaRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CrearCuetna", Namespace="http://tempuri.org/", Order=0)]
        public WebUsuarios.ServicioCuentas.CrearCuetnaRequestBody Body;
        
        public CrearCuetnaRequest() {
        }
        
        public CrearCuetnaRequest(WebUsuarios.ServicioCuentas.CrearCuetnaRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CrearCuetnaRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string numero_cuenta;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string nombre_usuario;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string tipo_cuenta;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public decimal saldo;
        
        public CrearCuetnaRequestBody() {
        }
        
        public CrearCuetnaRequestBody(string numero_cuenta, string nombre_usuario, string tipo_cuenta, decimal saldo) {
            this.numero_cuenta = numero_cuenta;
            this.nombre_usuario = nombre_usuario;
            this.tipo_cuenta = tipo_cuenta;
            this.saldo = saldo;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CrearCuetnaResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CrearCuetnaResponse", Namespace="http://tempuri.org/", Order=0)]
        public WebUsuarios.ServicioCuentas.CrearCuetnaResponseBody Body;
        
        public CrearCuetnaResponse() {
        }
        
        public CrearCuetnaResponse(WebUsuarios.ServicioCuentas.CrearCuetnaResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class CrearCuetnaResponseBody {
        
        public CrearCuetnaResponseBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ListarCuentasRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ListarCuentas", Namespace="http://tempuri.org/", Order=0)]
        public WebUsuarios.ServicioCuentas.ListarCuentasRequestBody Body;
        
        public ListarCuentasRequest() {
        }
        
        public ListarCuentasRequest(WebUsuarios.ServicioCuentas.ListarCuentasRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ListarCuentasRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string nombre_usuario;
        
        public ListarCuentasRequestBody() {
        }
        
        public ListarCuentasRequestBody(string nombre_usuario) {
            this.nombre_usuario = nombre_usuario;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ListarCuentasResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ListarCuentasResponse", Namespace="http://tempuri.org/", Order=0)]
        public WebUsuarios.ServicioCuentas.ListarCuentasResponseBody Body;
        
        public ListarCuentasResponse() {
        }
        
        public ListarCuentasResponse(WebUsuarios.ServicioCuentas.ListarCuentasResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ListarCuentasResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public WebUsuarios.ServicioCuentas.Cuentas[] ListarCuentasResult;
        
        public ListarCuentasResponseBody() {
        }
        
        public ListarCuentasResponseBody(WebUsuarios.ServicioCuentas.Cuentas[] ListarCuentasResult) {
            this.ListarCuentasResult = ListarCuentasResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ObtenerTelefonoUsuarioRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ObtenerTelefonoUsuario", Namespace="http://tempuri.org/", Order=0)]
        public WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioRequestBody Body;
        
        public ObtenerTelefonoUsuarioRequest() {
        }
        
        public ObtenerTelefonoUsuarioRequest(WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ObtenerTelefonoUsuarioRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string nombreUsuario;
        
        public ObtenerTelefonoUsuarioRequestBody() {
        }
        
        public ObtenerTelefonoUsuarioRequestBody(string nombreUsuario) {
            this.nombreUsuario = nombreUsuario;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ObtenerTelefonoUsuarioResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ObtenerTelefonoUsuarioResponse", Namespace="http://tempuri.org/", Order=0)]
        public WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioResponseBody Body;
        
        public ObtenerTelefonoUsuarioResponse() {
        }
        
        public ObtenerTelefonoUsuarioResponse(WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ObtenerTelefonoUsuarioResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ObtenerTelefonoUsuarioResult;
        
        public ObtenerTelefonoUsuarioResponseBody() {
        }
        
        public ObtenerTelefonoUsuarioResponseBody(string ObtenerTelefonoUsuarioResult) {
            this.ObtenerTelefonoUsuarioResult = ObtenerTelefonoUsuarioResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WebServiceAD_CuentasSoapChannel : WebUsuarios.ServicioCuentas.WebServiceAD_CuentasSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebServiceAD_CuentasSoapClient : System.ServiceModel.ClientBase<WebUsuarios.ServicioCuentas.WebServiceAD_CuentasSoap>, WebUsuarios.ServicioCuentas.WebServiceAD_CuentasSoap {
        
        public WebServiceAD_CuentasSoapClient() {
        }
        
        public WebServiceAD_CuentasSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebServiceAD_CuentasSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServiceAD_CuentasSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServiceAD_CuentasSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WebUsuarios.ServicioCuentas.CrearCuetnaResponse WebUsuarios.ServicioCuentas.WebServiceAD_CuentasSoap.CrearCuetna(WebUsuarios.ServicioCuentas.CrearCuetnaRequest request) {
            return base.Channel.CrearCuetna(request);
        }
        
        public void CrearCuetna(string numero_cuenta, string nombre_usuario, string tipo_cuenta, decimal saldo) {
            WebUsuarios.ServicioCuentas.CrearCuetnaRequest inValue = new WebUsuarios.ServicioCuentas.CrearCuetnaRequest();
            inValue.Body = new WebUsuarios.ServicioCuentas.CrearCuetnaRequestBody();
            inValue.Body.numero_cuenta = numero_cuenta;
            inValue.Body.nombre_usuario = nombre_usuario;
            inValue.Body.tipo_cuenta = tipo_cuenta;
            inValue.Body.saldo = saldo;
            WebUsuarios.ServicioCuentas.CrearCuetnaResponse retVal = ((WebUsuarios.ServicioCuentas.WebServiceAD_CuentasSoap)(this)).CrearCuetna(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WebUsuarios.ServicioCuentas.CrearCuetnaResponse> WebUsuarios.ServicioCuentas.WebServiceAD_CuentasSoap.CrearCuetnaAsync(WebUsuarios.ServicioCuentas.CrearCuetnaRequest request) {
            return base.Channel.CrearCuetnaAsync(request);
        }
        
        public System.Threading.Tasks.Task<WebUsuarios.ServicioCuentas.CrearCuetnaResponse> CrearCuetnaAsync(string numero_cuenta, string nombre_usuario, string tipo_cuenta, decimal saldo) {
            WebUsuarios.ServicioCuentas.CrearCuetnaRequest inValue = new WebUsuarios.ServicioCuentas.CrearCuetnaRequest();
            inValue.Body = new WebUsuarios.ServicioCuentas.CrearCuetnaRequestBody();
            inValue.Body.numero_cuenta = numero_cuenta;
            inValue.Body.nombre_usuario = nombre_usuario;
            inValue.Body.tipo_cuenta = tipo_cuenta;
            inValue.Body.saldo = saldo;
            return ((WebUsuarios.ServicioCuentas.WebServiceAD_CuentasSoap)(this)).CrearCuetnaAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WebUsuarios.ServicioCuentas.ListarCuentasResponse WebUsuarios.ServicioCuentas.WebServiceAD_CuentasSoap.ListarCuentas(WebUsuarios.ServicioCuentas.ListarCuentasRequest request) {
            return base.Channel.ListarCuentas(request);
        }
        
        public WebUsuarios.ServicioCuentas.Cuentas[] ListarCuentas(string nombre_usuario) {
            WebUsuarios.ServicioCuentas.ListarCuentasRequest inValue = new WebUsuarios.ServicioCuentas.ListarCuentasRequest();
            inValue.Body = new WebUsuarios.ServicioCuentas.ListarCuentasRequestBody();
            inValue.Body.nombre_usuario = nombre_usuario;
            WebUsuarios.ServicioCuentas.ListarCuentasResponse retVal = ((WebUsuarios.ServicioCuentas.WebServiceAD_CuentasSoap)(this)).ListarCuentas(inValue);
            return retVal.Body.ListarCuentasResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WebUsuarios.ServicioCuentas.ListarCuentasResponse> WebUsuarios.ServicioCuentas.WebServiceAD_CuentasSoap.ListarCuentasAsync(WebUsuarios.ServicioCuentas.ListarCuentasRequest request) {
            return base.Channel.ListarCuentasAsync(request);
        }
        
        public System.Threading.Tasks.Task<WebUsuarios.ServicioCuentas.ListarCuentasResponse> ListarCuentasAsync(string nombre_usuario) {
            WebUsuarios.ServicioCuentas.ListarCuentasRequest inValue = new WebUsuarios.ServicioCuentas.ListarCuentasRequest();
            inValue.Body = new WebUsuarios.ServicioCuentas.ListarCuentasRequestBody();
            inValue.Body.nombre_usuario = nombre_usuario;
            return ((WebUsuarios.ServicioCuentas.WebServiceAD_CuentasSoap)(this)).ListarCuentasAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioResponse WebUsuarios.ServicioCuentas.WebServiceAD_CuentasSoap.ObtenerTelefonoUsuario(WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioRequest request) {
            return base.Channel.ObtenerTelefonoUsuario(request);
        }
        
        public string ObtenerTelefonoUsuario(string nombreUsuario) {
            WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioRequest inValue = new WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioRequest();
            inValue.Body = new WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioRequestBody();
            inValue.Body.nombreUsuario = nombreUsuario;
            WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioResponse retVal = ((WebUsuarios.ServicioCuentas.WebServiceAD_CuentasSoap)(this)).ObtenerTelefonoUsuario(inValue);
            return retVal.Body.ObtenerTelefonoUsuarioResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioResponse> WebUsuarios.ServicioCuentas.WebServiceAD_CuentasSoap.ObtenerTelefonoUsuarioAsync(WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioRequest request) {
            return base.Channel.ObtenerTelefonoUsuarioAsync(request);
        }
        
        public System.Threading.Tasks.Task<WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioResponse> ObtenerTelefonoUsuarioAsync(string nombreUsuario) {
            WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioRequest inValue = new WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioRequest();
            inValue.Body = new WebUsuarios.ServicioCuentas.ObtenerTelefonoUsuarioRequestBody();
            inValue.Body.nombreUsuario = nombreUsuario;
            return ((WebUsuarios.ServicioCuentas.WebServiceAD_CuentasSoap)(this)).ObtenerTelefonoUsuarioAsync(inValue);
        }
    }
}
