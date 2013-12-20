using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DataContracts
{
    [DataContract]
    public class UserExistsFault
    {
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string ErrorDescription { get; set; }

        public UserExistsFault(int code, string desc)
        {
            ErrorCode = code;
            ErrorDescription = desc;
        }
    }
}