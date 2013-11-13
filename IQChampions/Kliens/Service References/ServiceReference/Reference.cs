﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iqchampion_design.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://iqchampions.com", ConfigurationName="ServiceReference.IIQService")]
    public interface IIQService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/Login", ReplyAction="http://iqchampions.com/IIQService/LoginResponse")]
        bool Login(string Name, string Password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/Login", ReplyAction="http://iqchampions.com/IIQService/LoginResponse")]
        System.Threading.Tasks.Task<bool> LoginAsync(string Name, string Password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/Logout", ReplyAction="http://iqchampions.com/IIQService/LogoutResponse")]
        bool Logout(string Name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/Logout", ReplyAction="http://iqchampions.com/IIQService/LogoutResponse")]
        System.Threading.Tasks.Task<bool> LogoutAsync(string Name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/Ping", ReplyAction="http://iqchampions.com/IIQService/PingResponse")]
        bool Ping(string Name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/Ping", ReplyAction="http://iqchampions.com/IIQService/PingResponse")]
        System.Threading.Tasks.Task<bool> PingAsync(string Name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/SendAll", ReplyAction="http://iqchampions.com/IIQService/SendAllResponse")]
        void SendAll(string name, string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/SendAll", ReplyAction="http://iqchampions.com/IIQService/SendAllResponse")]
        System.Threading.Tasks.Task SendAllAsync(string name, string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/getAllMesages", ReplyAction="http://iqchampions.com/IIQService/getAllMesagesResponse")]
        string[] getAllMesages();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/getAllMesages", ReplyAction="http://iqchampions.com/IIQService/getAllMesagesResponse")]
        System.Threading.Tasks.Task<string[]> getAllMesagesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/SendPrivate", ReplyAction="http://iqchampions.com/IIQService/SendPrivateResponse")]
        void SendPrivate(string from, string to, string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/SendPrivate", ReplyAction="http://iqchampions.com/IIQService/SendPrivateResponse")]
        System.Threading.Tasks.Task SendPrivateAsync(string from, string to, string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/getPrivateMesages", ReplyAction="http://iqchampions.com/IIQService/getPrivateMesagesResponse")]
        string[] getPrivateMesages(string from);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/getPrivateMesages", ReplyAction="http://iqchampions.com/IIQService/getPrivateMesagesResponse")]
        System.Threading.Tasks.Task<string[]> getPrivateMesagesAsync(string from);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/SendRoom", ReplyAction="http://iqchampions.com/IIQService/SendRoomResponse")]
        void SendRoom(string name, string message, string szobanev);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/SendRoom", ReplyAction="http://iqchampions.com/IIQService/SendRoomResponse")]
        System.Threading.Tasks.Task SendRoomAsync(string name, string message, string szobanev);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/getRoomMesages", ReplyAction="http://iqchampions.com/IIQService/getRoomMesagesResponse")]
        string[] getRoomMesages(string szobanev);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/getRoomMesages", ReplyAction="http://iqchampions.com/IIQService/getRoomMesagesResponse")]
        System.Threading.Tasks.Task<string[]> getRoomMesagesAsync(string szobanev);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/GetUsers", ReplyAction="http://iqchampions.com/IIQService/GetUsersResponse")]
        string[] GetUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/GetUsers", ReplyAction="http://iqchampions.com/IIQService/GetUsersResponse")]
        System.Threading.Tasks.Task<string[]> GetUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/GetStatistic", ReplyAction="http://iqchampions.com/IIQService/GetStatisticResponse")]
        string[] GetStatistic(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/GetStatistic", ReplyAction="http://iqchampions.com/IIQService/GetStatisticResponse")]
        System.Threading.Tasks.Task<string[]> GetStatisticAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/UpdateStatistic", ReplyAction="http://iqchampions.com/IIQService/UpdateStatisticResponse")]
        bool UpdateStatistic(string name, int kerdeszam, int helyesvalasz);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/UpdateStatistic", ReplyAction="http://iqchampions.com/IIQService/UpdateStatisticResponse")]
        System.Threading.Tasks.Task<bool> UpdateStatisticAsync(string name, int kerdeszam, int helyesvalasz);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/Szobaletrehozas", ReplyAction="http://iqchampions.com/IIQService/SzobaletrehozasResponse")]
        void Szobaletrehozas();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/Szobaletrehozas", ReplyAction="http://iqchampions.com/IIQService/SzobaletrehozasResponse")]
        System.Threading.Tasks.Task SzobaletrehozasAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/Szobacsatlakozas", ReplyAction="http://iqchampions.com/IIQService/SzobacsatlakozasResponse")]
        bool Szobacsatlakozas(string usernev, string szobanev);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/Szobacsatlakozas", ReplyAction="http://iqchampions.com/IIQService/SzobacsatlakozasResponse")]
        System.Threading.Tasks.Task<bool> SzobacsatlakozasAsync(string usernev, string szobanev);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/Szobakilepes", ReplyAction="http://iqchampions.com/IIQService/SzobakilepesResponse")]
        void Szobakilepes(string username, string szobanev);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/Szobakilepes", ReplyAction="http://iqchampions.com/IIQService/SzobakilepesResponse")]
        System.Threading.Tasks.Task SzobakilepesAsync(string username, string szobanev);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/SzobaInditas", ReplyAction="http://iqchampions.com/IIQService/SzobaInditasResponse")]
        void SzobaInditas(string szobanev);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/SzobaInditas", ReplyAction="http://iqchampions.com/IIQService/SzobaInditasResponse")]
        System.Threading.Tasks.Task SzobaInditasAsync(string szobanev);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/SzobaTorles", ReplyAction="http://iqchampions.com/IIQService/SzobaTorlesResponse")]
        void SzobaTorles(string szobanev);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/SzobaTorles", ReplyAction="http://iqchampions.com/IIQService/SzobaTorlesResponse")]
        System.Threading.Tasks.Task SzobaTorlesAsync(string szobanev);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/SzobaPing", ReplyAction="http://iqchampions.com/IIQService/SzobaPingResponse")]
        void SzobaPing(string szobanev);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://iqchampions.com/IIQService/SzobaPing", ReplyAction="http://iqchampions.com/IIQService/SzobaPingResponse")]
        System.Threading.Tasks.Task SzobaPingAsync(string szobanev);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IIQServiceChannel : iqchampion_design.ServiceReference.IIQService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class IQServiceClient : System.ServiceModel.ClientBase<iqchampion_design.ServiceReference.IIQService>, iqchampion_design.ServiceReference.IIQService {
        
        public IQServiceClient() {
        }
        
        public IQServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public IQServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IQServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IQServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Login(string Name, string Password) {
            return base.Channel.Login(Name, Password);
        }
        
        public System.Threading.Tasks.Task<bool> LoginAsync(string Name, string Password) {
            return base.Channel.LoginAsync(Name, Password);
        }
        
        public bool Logout(string Name) {
            return base.Channel.Logout(Name);
        }
        
        public System.Threading.Tasks.Task<bool> LogoutAsync(string Name) {
            return base.Channel.LogoutAsync(Name);
        }
        
        public bool Ping(string Name) {
            return base.Channel.Ping(Name);
        }
        
        public System.Threading.Tasks.Task<bool> PingAsync(string Name) {
            return base.Channel.PingAsync(Name);
        }
        
        public void SendAll(string name, string message) {
            base.Channel.SendAll(name, message);
        }
        
        public System.Threading.Tasks.Task SendAllAsync(string name, string message) {
            return base.Channel.SendAllAsync(name, message);
        }
        
        public string[] getAllMesages() {
            return base.Channel.getAllMesages();
        }
        
        public System.Threading.Tasks.Task<string[]> getAllMesagesAsync() {
            return base.Channel.getAllMesagesAsync();
        }
        
        public void SendPrivate(string from, string to, string message) {
            base.Channel.SendPrivate(from, to, message);
        }
        
        public System.Threading.Tasks.Task SendPrivateAsync(string from, string to, string message) {
            return base.Channel.SendPrivateAsync(from, to, message);
        }
        
        public string[] getPrivateMesages(string from) {
            return base.Channel.getPrivateMesages(from);
        }
        
        public System.Threading.Tasks.Task<string[]> getPrivateMesagesAsync(string from) {
            return base.Channel.getPrivateMesagesAsync(from);
        }
        
        public void SendRoom(string name, string message, string szobanev) {
            base.Channel.SendRoom(name, message, szobanev);
        }
        
        public System.Threading.Tasks.Task SendRoomAsync(string name, string message, string szobanev) {
            return base.Channel.SendRoomAsync(name, message, szobanev);
        }
        
        public string[] getRoomMesages(string szobanev) {
            return base.Channel.getRoomMesages(szobanev);
        }
        
        public System.Threading.Tasks.Task<string[]> getRoomMesagesAsync(string szobanev) {
            return base.Channel.getRoomMesagesAsync(szobanev);
        }
        
        public string[] GetUsers() {
            return base.Channel.GetUsers();
        }
        
        public System.Threading.Tasks.Task<string[]> GetUsersAsync() {
            return base.Channel.GetUsersAsync();
        }
        
        public string[] GetStatistic(string username) {
            return base.Channel.GetStatistic(username);
        }
        
        public System.Threading.Tasks.Task<string[]> GetStatisticAsync(string username) {
            return base.Channel.GetStatisticAsync(username);
        }
        
        public bool UpdateStatistic(string name, int kerdeszam, int helyesvalasz) {
            return base.Channel.UpdateStatistic(name, kerdeszam, helyesvalasz);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateStatisticAsync(string name, int kerdeszam, int helyesvalasz) {
            return base.Channel.UpdateStatisticAsync(name, kerdeszam, helyesvalasz);
        }
        
        public void Szobaletrehozas() {
            base.Channel.Szobaletrehozas();
        }
        
        public System.Threading.Tasks.Task SzobaletrehozasAsync() {
            return base.Channel.SzobaletrehozasAsync();
        }
        
        public bool Szobacsatlakozas(string usernev, string szobanev) {
            return base.Channel.Szobacsatlakozas(usernev, szobanev);
        }
        
        public System.Threading.Tasks.Task<bool> SzobacsatlakozasAsync(string usernev, string szobanev) {
            return base.Channel.SzobacsatlakozasAsync(usernev, szobanev);
        }
        
        public void Szobakilepes(string username, string szobanev) {
            base.Channel.Szobakilepes(username, szobanev);
        }
        
        public System.Threading.Tasks.Task SzobakilepesAsync(string username, string szobanev) {
            return base.Channel.SzobakilepesAsync(username, szobanev);
        }
        
        public void SzobaInditas(string szobanev) {
            base.Channel.SzobaInditas(szobanev);
        }
        
        public System.Threading.Tasks.Task SzobaInditasAsync(string szobanev) {
            return base.Channel.SzobaInditasAsync(szobanev);
        }
        
        public void SzobaTorles(string szobanev) {
            base.Channel.SzobaTorles(szobanev);
        }
        
        public System.Threading.Tasks.Task SzobaTorlesAsync(string szobanev) {
            return base.Channel.SzobaTorlesAsync(szobanev);
        }
        
        public void SzobaPing(string szobanev) {
            base.Channel.SzobaPing(szobanev);
        }
        
        public System.Threading.Tasks.Task SzobaPingAsync(string szobanev) {
            return base.Channel.SzobaPingAsync(szobanev);
        }
    }
}
