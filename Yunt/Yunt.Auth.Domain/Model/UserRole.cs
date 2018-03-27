using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;
using Yunt.Auth.Domain.BaseModel;

namespace Yunt.Auth.Domain.Model
{ 
    /// <summary>
  /// 角色
  /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public class UserRole : AggregateRoot
    {

        ///// <summary>
        ///// 角色编号
        ///// </summary> 
        //[DataMember]
        //[DisplayName("角色编号")]
        //[ProtoMember(1)]
        //public string UserRoleId { get; set; }

        /// <summary>
        /// 描述
        /// </summary> 
        [DataMember]
        [DisplayName("描述")]
        [ProtoMember(1)]
        public string Desc { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        [DisplayName("备注")]
        [ProtoMember(2)]
        public string Remark { get; set; }

    }
}
