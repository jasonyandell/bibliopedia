﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.21006.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ParCit.ParsCitService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://citeseerx.org/algorithms/parscit/wsdl", ConfigurationName="ParsCitService.ParsCitPT")]
    public interface ParsCitPT {
        
        [System.ServiceModel.OperationContractAttribute()]
        void extractCitations();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ParsCitPTChannel : ParCit.ParsCitService.ParsCitPT, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ParsCitPTClient : System.ServiceModel.ClientBase<ParCit.ParsCitService.ParsCitPT>, ParCit.ParsCitService.ParsCitPT {
        
        public ParsCitPTClient() {
        }
        
        public ParsCitPTClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ParsCitPTClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ParsCitPTClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ParsCitPTClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void extractCitations() {
            base.Channel.extractCitations();
        }
    }
}
