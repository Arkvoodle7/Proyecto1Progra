﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiciosWeb.WSCuentas {
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WSCuentas.WebServiceAD_CuentasSoap")]
    public interface WebServiceAD_CuentasSoap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento numero_cuenta del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CrearCuetna", ReplyAction="*")]
        ServiciosWeb.WSCuentas.CrearCuetnaResponse CrearCuetna(ServiciosWeb.WSCuentas.CrearCuetnaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CrearCuetna", ReplyAction="*")]
        System.Threading.Tasks.Task<ServiciosWeb.WSCuentas.CrearCuetnaResponse> CrearCuetnaAsync(ServiciosWeb.WSCuentas.CrearCuetnaRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento nombre_usuario del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarCuentas", ReplyAction="*")]
        ServiciosWeb.WSCuentas.ListarCuentasResponse ListarCuentas(ServiciosWeb.WSCuentas.ListarCuentasRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarCuentas", ReplyAction="*")]
        System.Threading.Tasks.Task<ServiciosWeb.WSCuentas.ListarCuentasResponse> ListarCuentasAsync(ServiciosWeb.WSCuentas.ListarCuentasRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento ListarTodasCuentasResult del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarTodasCuentas", ReplyAction="*")]
        ServiciosWeb.WSCuentas.ListarTodasCuentasResponse ListarTodasCuentas(ServiciosWeb.WSCuentas.ListarTodasCuentasRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ListarTodasCuentas", ReplyAction="*")]
        System.Threading.Tasks.Task<ServiciosWeb.WSCuentas.ListarTodasCuentasResponse> ListarTodasCuentasAsync(ServiciosWeb.WSCuentas.ListarTodasCuentasRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento cedula del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ObtenerNombreUsuarioPorCedula", ReplyAction="*")]
        ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaResponse ObtenerNombreUsuarioPorCedula(ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ObtenerNombreUsuarioPorCedula", ReplyAction="*")]
        System.Threading.Tasks.Task<ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaResponse> ObtenerNombreUsuarioPorCedulaAsync(ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CrearCuetnaRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CrearCuetna", Namespace="http://tempuri.org/", Order=0)]
        public ServiciosWeb.WSCuentas.CrearCuetnaRequestBody Body;
        
        public CrearCuetnaRequest() {
        }
        
        public CrearCuetnaRequest(ServiciosWeb.WSCuentas.CrearCuetnaRequestBody Body) {
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
        public ServiciosWeb.WSCuentas.CrearCuetnaResponseBody Body;
        
        public CrearCuetnaResponse() {
        }
        
        public CrearCuetnaResponse(ServiciosWeb.WSCuentas.CrearCuetnaResponseBody Body) {
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
        public ServiciosWeb.WSCuentas.ListarCuentasRequestBody Body;
        
        public ListarCuentasRequest() {
        }
        
        public ListarCuentasRequest(ServiciosWeb.WSCuentas.ListarCuentasRequestBody Body) {
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
        public ServiciosWeb.WSCuentas.ListarCuentasResponseBody Body;
        
        public ListarCuentasResponse() {
        }
        
        public ListarCuentasResponse(ServiciosWeb.WSCuentas.ListarCuentasResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ListarCuentasResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ServiciosWeb.WSCuentas.Cuentas[] ListarCuentasResult;
        
        public ListarCuentasResponseBody() {
        }
        
        public ListarCuentasResponseBody(ServiciosWeb.WSCuentas.Cuentas[] ListarCuentasResult) {
            this.ListarCuentasResult = ListarCuentasResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ListarTodasCuentasRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ListarTodasCuentas", Namespace="http://tempuri.org/", Order=0)]
        public ServiciosWeb.WSCuentas.ListarTodasCuentasRequestBody Body;
        
        public ListarTodasCuentasRequest() {
        }
        
        public ListarTodasCuentasRequest(ServiciosWeb.WSCuentas.ListarTodasCuentasRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class ListarTodasCuentasRequestBody {
        
        public ListarTodasCuentasRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ListarTodasCuentasResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ListarTodasCuentasResponse", Namespace="http://tempuri.org/", Order=0)]
        public ServiciosWeb.WSCuentas.ListarTodasCuentasResponseBody Body;
        
        public ListarTodasCuentasResponse() {
        }
        
        public ListarTodasCuentasResponse(ServiciosWeb.WSCuentas.ListarTodasCuentasResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ListarTodasCuentasResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ServiciosWeb.WSCuentas.Cuentas[] ListarTodasCuentasResult;
        
        public ListarTodasCuentasResponseBody() {
        }
        
        public ListarTodasCuentasResponseBody(ServiciosWeb.WSCuentas.Cuentas[] ListarTodasCuentasResult) {
            this.ListarTodasCuentasResult = ListarTodasCuentasResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ObtenerNombreUsuarioPorCedulaRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ObtenerNombreUsuarioPorCedula", Namespace="http://tempuri.org/", Order=0)]
        public ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaRequestBody Body;
        
        public ObtenerNombreUsuarioPorCedulaRequest() {
        }
        
        public ObtenerNombreUsuarioPorCedulaRequest(ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ObtenerNombreUsuarioPorCedulaRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string cedula;
        
        public ObtenerNombreUsuarioPorCedulaRequestBody() {
        }
        
        public ObtenerNombreUsuarioPorCedulaRequestBody(string cedula) {
            this.cedula = cedula;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ObtenerNombreUsuarioPorCedulaResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ObtenerNombreUsuarioPorCedulaResponse", Namespace="http://tempuri.org/", Order=0)]
        public ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaResponseBody Body;
        
        public ObtenerNombreUsuarioPorCedulaResponse() {
        }
        
        public ObtenerNombreUsuarioPorCedulaResponse(ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ObtenerNombreUsuarioPorCedulaResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ObtenerNombreUsuarioPorCedulaResult;
        
        public ObtenerNombreUsuarioPorCedulaResponseBody() {
        }
        
        public ObtenerNombreUsuarioPorCedulaResponseBody(string ObtenerNombreUsuarioPorCedulaResult) {
            this.ObtenerNombreUsuarioPorCedulaResult = ObtenerNombreUsuarioPorCedulaResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WebServiceAD_CuentasSoapChannel : ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebServiceAD_CuentasSoapClient : System.ServiceModel.ClientBase<ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap>, ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap {
        
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
        ServiciosWeb.WSCuentas.CrearCuetnaResponse ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap.CrearCuetna(ServiciosWeb.WSCuentas.CrearCuetnaRequest request) {
            return base.Channel.CrearCuetna(request);
        }
        
        public void CrearCuetna(string numero_cuenta, string nombre_usuario, string tipo_cuenta, decimal saldo) {
            ServiciosWeb.WSCuentas.CrearCuetnaRequest inValue = new ServiciosWeb.WSCuentas.CrearCuetnaRequest();
            inValue.Body = new ServiciosWeb.WSCuentas.CrearCuetnaRequestBody();
            inValue.Body.numero_cuenta = numero_cuenta;
            inValue.Body.nombre_usuario = nombre_usuario;
            inValue.Body.tipo_cuenta = tipo_cuenta;
            inValue.Body.saldo = saldo;
            ServiciosWeb.WSCuentas.CrearCuetnaResponse retVal = ((ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap)(this)).CrearCuetna(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiciosWeb.WSCuentas.CrearCuetnaResponse> ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap.CrearCuetnaAsync(ServiciosWeb.WSCuentas.CrearCuetnaRequest request) {
            return base.Channel.CrearCuetnaAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiciosWeb.WSCuentas.CrearCuetnaResponse> CrearCuetnaAsync(string numero_cuenta, string nombre_usuario, string tipo_cuenta, decimal saldo) {
            ServiciosWeb.WSCuentas.CrearCuetnaRequest inValue = new ServiciosWeb.WSCuentas.CrearCuetnaRequest();
            inValue.Body = new ServiciosWeb.WSCuentas.CrearCuetnaRequestBody();
            inValue.Body.numero_cuenta = numero_cuenta;
            inValue.Body.nombre_usuario = nombre_usuario;
            inValue.Body.tipo_cuenta = tipo_cuenta;
            inValue.Body.saldo = saldo;
            return ((ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap)(this)).CrearCuetnaAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ServiciosWeb.WSCuentas.ListarCuentasResponse ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap.ListarCuentas(ServiciosWeb.WSCuentas.ListarCuentasRequest request) {
            return base.Channel.ListarCuentas(request);
        }
        
        public ServiciosWeb.WSCuentas.Cuentas[] ListarCuentas(string nombre_usuario) {
            ServiciosWeb.WSCuentas.ListarCuentasRequest inValue = new ServiciosWeb.WSCuentas.ListarCuentasRequest();
            inValue.Body = new ServiciosWeb.WSCuentas.ListarCuentasRequestBody();
            inValue.Body.nombre_usuario = nombre_usuario;
            ServiciosWeb.WSCuentas.ListarCuentasResponse retVal = ((ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap)(this)).ListarCuentas(inValue);
            return retVal.Body.ListarCuentasResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiciosWeb.WSCuentas.ListarCuentasResponse> ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap.ListarCuentasAsync(ServiciosWeb.WSCuentas.ListarCuentasRequest request) {
            return base.Channel.ListarCuentasAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiciosWeb.WSCuentas.ListarCuentasResponse> ListarCuentasAsync(string nombre_usuario) {
            ServiciosWeb.WSCuentas.ListarCuentasRequest inValue = new ServiciosWeb.WSCuentas.ListarCuentasRequest();
            inValue.Body = new ServiciosWeb.WSCuentas.ListarCuentasRequestBody();
            inValue.Body.nombre_usuario = nombre_usuario;
            return ((ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap)(this)).ListarCuentasAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ServiciosWeb.WSCuentas.ListarTodasCuentasResponse ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap.ListarTodasCuentas(ServiciosWeb.WSCuentas.ListarTodasCuentasRequest request) {
            return base.Channel.ListarTodasCuentas(request);
        }
        
        public ServiciosWeb.WSCuentas.Cuentas[] ListarTodasCuentas() {
            ServiciosWeb.WSCuentas.ListarTodasCuentasRequest inValue = new ServiciosWeb.WSCuentas.ListarTodasCuentasRequest();
            inValue.Body = new ServiciosWeb.WSCuentas.ListarTodasCuentasRequestBody();
            ServiciosWeb.WSCuentas.ListarTodasCuentasResponse retVal = ((ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap)(this)).ListarTodasCuentas(inValue);
            return retVal.Body.ListarTodasCuentasResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiciosWeb.WSCuentas.ListarTodasCuentasResponse> ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap.ListarTodasCuentasAsync(ServiciosWeb.WSCuentas.ListarTodasCuentasRequest request) {
            return base.Channel.ListarTodasCuentasAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiciosWeb.WSCuentas.ListarTodasCuentasResponse> ListarTodasCuentasAsync() {
            ServiciosWeb.WSCuentas.ListarTodasCuentasRequest inValue = new ServiciosWeb.WSCuentas.ListarTodasCuentasRequest();
            inValue.Body = new ServiciosWeb.WSCuentas.ListarTodasCuentasRequestBody();
            return ((ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap)(this)).ListarTodasCuentasAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaResponse ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap.ObtenerNombreUsuarioPorCedula(ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaRequest request) {
            return base.Channel.ObtenerNombreUsuarioPorCedula(request);
        }
        
        public string ObtenerNombreUsuarioPorCedula(string cedula) {
            ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaRequest inValue = new ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaRequest();
            inValue.Body = new ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaRequestBody();
            inValue.Body.cedula = cedula;
            ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaResponse retVal = ((ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap)(this)).ObtenerNombreUsuarioPorCedula(inValue);
            return retVal.Body.ObtenerNombreUsuarioPorCedulaResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaResponse> ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap.ObtenerNombreUsuarioPorCedulaAsync(ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaRequest request) {
            return base.Channel.ObtenerNombreUsuarioPorCedulaAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaResponse> ObtenerNombreUsuarioPorCedulaAsync(string cedula) {
            ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaRequest inValue = new ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaRequest();
            inValue.Body = new ServiciosWeb.WSCuentas.ObtenerNombreUsuarioPorCedulaRequestBody();
            inValue.Body.cedula = cedula;
            return ((ServiciosWeb.WSCuentas.WebServiceAD_CuentasSoap)(this)).ObtenerNombreUsuarioPorCedulaAsync(inValue);
        }
    }
}
