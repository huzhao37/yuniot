using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunt.Xml.Domain.Model;
using Yunt.Common;
using Yunt.Xml.Domain.IRepository;
using Yunt.Xml.Domain.MiddleMap;

namespace Yunt.Xml.Domain.Services
{
    public interface IBytesParseRepository : IXmlRepositoryBase<Datatype>
    {

        DataGramModel Parser(byte[] buffer);

        /// <summary>
        /// 队列数据解析并操作;
        /// </summary>
        /// <param name="buffer">数据</param>
        ///  <param name="typeString">数据类型</param>
        /// <param name="operation">匿名委托方法</param>
       bool UniversalParser(byte[] buffer, string typeString, Func<DataGramModel, string, bool> operation);

        /// <summary>
        /// 解析原始字节
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        bool UniversalParser(byte[] buffer, Func<DataGramModel, string, bool> operation);
    }
}
