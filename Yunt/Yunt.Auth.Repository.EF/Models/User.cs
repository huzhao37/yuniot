using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace Yunt.Auth.Repository.EF.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public  class User : BaseModel
    {
        /// <summary>
        /// 用户编号
        /// </summary> 
        [DataMember]
        [DisplayName("用户编号")]
        [ProtoMember(1)]
        public string UserId { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary> 
        [DataMember]
        [DisplayName("角色编号")]
        [ProtoMember(2)]
        public int UserRoleId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [DataMember]
        [DisplayName("用户名称")]
        [ProtoMember(3)]
        public string UserName { get; set; }
        /// <summary>
        /// 登录账户
        /// </summary>
        [DataMember]
        [DisplayName("登录账户")]
        [ProtoMember(4)]
        public string LoginAccount { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [DataMember]
        [DisplayName("登录密码")]
        [ProtoMember(5)]
        public string LoginPwd { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        [DisplayName("邮箱")]
        [ProtoMember(6)]
        public string Mail { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [DataMember]
        [DisplayName("手机号")]
        [ProtoMember(7)]
        public string MobileNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        [DisplayName("备注")]
        [ProtoMember(8)]
        public string Remark { get; set; }

    }
}
