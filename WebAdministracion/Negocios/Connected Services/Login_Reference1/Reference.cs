﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Negocios.Login_Reference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LoginAdmin", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class LoginAdmin : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int resultadoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string mensajeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int resultado {
            get {
                return this.resultadoField;
            }
            set {
                if ((this.resultadoField.Equals(value) != true)) {
                    this.resultadoField = value;
                    this.RaisePropertyChanged("resultado");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string mensaje {
            get {
                return this.mensajeField;
            }
            set {
                if ((object.ReferenceEquals(this.mensajeField, value) != true)) {
                    this.mensajeField = value;
                    this.RaisePropertyChanged("mensaje");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Login_Reference1.WSA1Soap")]
    public interface WSA1Soap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento usuario del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Login_Administradores", ReplyAction="*")]
        Negocios.Login_Reference1.Login_AdministradoresResponse Login_Administradores(Negocios.Login_Reference1.Login_AdministradoresRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Login_Administradores", ReplyAction="*")]
        System.Threading.Tasks.Task<Negocios.Login_Reference1.Login_AdministradoresResponse> Login_AdministradoresAsync(Negocios.Login_Reference1.Login_AdministradoresRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class Login_AdministradoresRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Login_Administradores", Namespace="http://tempuri.org/", Order=0)]
        public Negocios.Login_Reference1.Login_AdministradoresRequestBody Body;
        
        public Login_AdministradoresRequest() {
        }
        
        public Login_AdministradoresRequest(Negocios.Login_Reference1.Login_AdministradoresRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class Login_AdministradoresRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string usuario;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string contra;
        
        public Login_AdministradoresRequestBody() {
        }
        
        public Login_AdministradoresRequestBody(string usuario, string contra) {
            this.usuario = usuario;
            this.contra = contra;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class Login_AdministradoresResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Login_AdministradoresResponse", Namespace="http://tempuri.org/", Order=0)]
        public Negocios.Login_Reference1.Login_AdministradoresResponseBody Body;
        
        public Login_AdministradoresResponse() {
        }
        
        public Login_AdministradoresResponse(Negocios.Login_Reference1.Login_AdministradoresResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class Login_AdministradoresResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public Negocios.Login_Reference1.LoginAdmin respuesta;
        
        public Login_AdministradoresResponseBody() {
        }
        
        public Login_AdministradoresResponseBody(Negocios.Login_Reference1.LoginAdmin respuesta) {
            this.respuesta = respuesta;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WSA1SoapChannel : Negocios.Login_Reference1.WSA1Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WSA1SoapClient : System.ServiceModel.ClientBase<Negocios.Login_Reference1.WSA1Soap>, Negocios.Login_Reference1.WSA1Soap {
        
        public WSA1SoapClient() {
        }
        
        public WSA1SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WSA1SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSA1SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSA1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Negocios.Login_Reference1.Login_AdministradoresResponse Negocios.Login_Reference1.WSA1Soap.Login_Administradores(Negocios.Login_Reference1.Login_AdministradoresRequest request) {
            return base.Channel.Login_Administradores(request);
        }
        
        public Negocios.Login_Reference1.LoginAdmin Login_Administradores(string usuario, string contra) {
            Negocios.Login_Reference1.Login_AdministradoresRequest inValue = new Negocios.Login_Reference1.Login_AdministradoresRequest();
            inValue.Body = new Negocios.Login_Reference1.Login_AdministradoresRequestBody();
            inValue.Body.usuario = usuario;
            inValue.Body.contra = contra;
            Negocios.Login_Reference1.Login_AdministradoresResponse retVal = ((Negocios.Login_Reference1.WSA1Soap)(this)).Login_Administradores(inValue);
            return retVal.Body.respuesta;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Negocios.Login_Reference1.Login_AdministradoresResponse> Negocios.Login_Reference1.WSA1Soap.Login_AdministradoresAsync(Negocios.Login_Reference1.Login_AdministradoresRequest request) {
            return base.Channel.Login_AdministradoresAsync(request);
        }
        
        public System.Threading.Tasks.Task<Negocios.Login_Reference1.Login_AdministradoresResponse> Login_AdministradoresAsync(string usuario, string contra) {
            Negocios.Login_Reference1.Login_AdministradoresRequest inValue = new Negocios.Login_Reference1.Login_AdministradoresRequest();
            inValue.Body = new Negocios.Login_Reference1.Login_AdministradoresRequestBody();
            inValue.Body.usuario = usuario;
            inValue.Body.contra = contra;
            return ((Negocios.Login_Reference1.WSA1Soap)(this)).Login_AdministradoresAsync(inValue);
        }
    }
}
