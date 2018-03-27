using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;
using Yunt.Auth.Domain.BaseModel;

namespace Yunt.Auth.Domain.Model.IdModel
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public class UserIdFactories : AggregateRoot
    {
        /// <summary>
        /// 用户序号
        /// </summary>

        [DataMember]
        [ProtoMember(1)]
        public int UserIndex { get; set; }
    }
}
