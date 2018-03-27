using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace Yunt.Auth.Repository.EF.Models.IdModel
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public class UserIdFactories : BaseModel
    {
        /// <summary>
        /// 用户序号
        /// </summary>

        [DataMember]
        [ProtoMember(1)]
        public int UserIndex { get; set; }
    }
}
