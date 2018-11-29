#region Code File Comment

// SOLUTION   ： 物联网V3
// PROJECT    ： Yunt.Redis
// FILENAME   ： ExtensionsMethod.cs
// AUTHOR     ： soft-zh
// CREATE TIME： 2018-3-14 9:42
// COPYRIGHT  ： 版权所有 (C) *** *** 2018~2020

#endregion

#region using namespace

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Yunt.Redis
{
    public static class BeetleRedisExtensionsMethod
    {
        public static string GetString(this ArraySegment<byte> data)
        {
            return Utils.GetString(data);
        }

        public static T GetProtobuf<T>(this ArraySegment<byte> data)
        {
            return (T) Utils.GetProtobuf(data, typeof (T));
        }

        public static object GetProtobuf(this ArraySegment<byte> data, Type type)
        {
            return Utils.GetProtobuf(data, type);
        }

        public static IList<object> FieldValueToList(this IEnumerable<Field> items)
        {
            var result = new List<object>();
            foreach (var item in items)
            {
                result.Add(item.Value);
            }
            return result;
        }
    }

    public static class BeetleRedisGetExtensionsMethod
    {
        public static RedisKey RedisString(this IEnumerable<string> key)
        {
            return new StringKey(key.ToArray());
        }

        public static RedisKey RedisProtobuf(this IEnumerable<string> key)
        {
            return new ProtobufKey(key.ToArray());
        }

        public static RedisKey RedisString(this string key)
        {
            return new StringKey(key);
        }

        public static RedisKey RedisProtobuf(this string key)
        {
            return new ProtobufKey(key);
        }
    }
}