using System;
using System.Net;
using System.Runtime.Serialization;

namespace ModernInfoTechInventory.ErrorHelper
{
    [Serializable]
    [DataContract]
    public class ApiException : Exception, IApiExceptions
    {
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string ErrorDescription { get; set; }
        [DataMember]
        public HttpStatusCode HttpStatus { get; set; }
        string reasonPhrase = "ApiException";

        [DataMember]
        public string ReasonPhrase
        {
            get { return this.reasonPhrase; }
            set { this.reasonPhrase = value; }
        }
    }
}