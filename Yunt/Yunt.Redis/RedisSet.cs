#region Code File Comment

// SOLUTION   ： 物联网V3
// PROJECT    ： Yunt.Redis
// FILENAME   ： RedisSet.cs
// AUTHOR     ： soft-zh
// CREATE TIME： 2018-3-14 9:42
// COPYRIGHT  ： 版权所有 (C) *** *** 2018~2020

#endregion

#region using namespace

using System.Collections.Generic;


#endregion

namespace Yunt.Redis
{
    public abstract class RedisSet<T>
    {
        private readonly string mDataKey;

        private readonly DataType mDataType;

        public RedisSet(string key, DataType dataType)
        {
            mDataType = dataType;
            mDataKey = key;
        }

        public int Sadd<T>(T member)
        {
            return Sadds<T>(new List<T>() {member});
        }

        public int Sadds<T>(List<T> members,  RedisCachingProvider db = null)
        {
            db =  RedisCachingProvider.GetClient(db);
            return db.Sadd((string) mDataKey, members, mDataType);
        }

        public int Scard( RedisCachingProvider db = null)
        {
            db =  RedisCachingProvider.GetClient(db);
            return db.Scard((string) mDataKey);
        }

        public bool Sismember<T>(T member,  RedisCachingProvider db = null)
        {
            db =  RedisCachingProvider.GetClient(db);
            return db.Sismember((string) mDataKey, member, mDataType);
        }

        public List<T> Smember<T>( RedisCachingProvider db = null)
        {
            db =  RedisCachingProvider.GetClient(db);
            return db.Smember<T>((string) mDataKey, mDataType);
        }

        public int Srem<T>(T member,  RedisCachingProvider db = null)
        {
            db =  RedisCachingProvider.GetClient(db);
            return db.Srem((string) mDataKey, member, mDataType);
        }
    }

    public class StringSet : RedisSet<string>
    {
        public StringSet(string key)
            : base(key, DataType.String)
        {
        }

        public static implicit operator StringSet(string key)
        {
            return new StringSet(key);
        }
    }

    public class ProtobufSet<T> : RedisSet<T>
    {
        public ProtobufSet(string key)
            : base(key, DataType.Protobuf)
        {
        }

        public static implicit operator ProtobufSet<T>(string key)
        {
            return new ProtobufSet<T>(key);
        }
    }

    public class JsonSet<T> : RedisSet<T>
    {
        public JsonSet(string key)
            : base(key, DataType.Json)
        {
        }

        public static implicit operator JsonSet<T>(string key)
        {
            return new JsonSet<T>(key);
        }
    }
}