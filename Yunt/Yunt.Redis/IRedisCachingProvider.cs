using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Redis
{
   public interface IRedisCachingProvider
    {

        #region 常用方法
        int DB { get; set; }
        /// <summary>
        ///     返回哈希表key中域的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns>哈希表中域的数量 当key不存在时，返回0</returns>
        //bool Auth();

        #endregion

        /// <summary>
        ///     返回哈希表key中域的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns>哈希表中域的数量 当key不存在时，返回0</returns>
        int HLen(string key);

        /// <summary>
        ///     为给定key设置生存时间
        ///     当key过期时，它会被自动删除。
        ///     在Redis中，带有生存时间的key被称作“易失的”(volatile)。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="time"></param>
        /// <returns>
        ///     设置成功返回1。
        ///     当key不存在或者不能为key设置生存时间时(比如在低于2.1.3中你尝试更新key的生存时间)，返回0。
        /// </returns>
        int Expire(string key, long time);

        /// <summary>
        ///    EXPIREAT key timestamp

        ///  EXPIREAT 的作用和 EXPIRE 类似，都用于为 key 设置生存时间。

        ///不同在于 EXPIREAT 命令接受的时间参数是 UNIX 时间戳(unix timestamp)。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="time"></param>
        /// <returns>
        ///     设置成功返回1。
        ///     当key不存在或者不能为key设置生存时间时(比如在低于2.1.3中你尝试更新key的生存时间)，返回0。
        /// </returns>
        int ExpireAt(string key, long time);

        /// <summary>
        ///     返回给定key的剩余生存时间(time to live)(以秒为单位)。
        /// </summary>
        /// <param name="key"></param>
        /// <returns>key的剩余生存时间(以秒为单位)。当key不存在或没有设置生存时间时，返回-1 。</returns>
        int TTL(string key);

        /// <summary>
        ///     返回当前服器时间
        /// </summary>
        /// <returns></returns>
        List<string> Time();

        /// <summary>
        ///     从当前数据库中随机返回(不删除)一个key。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>当数据库不为空时，返回一个key。当数据库为空时，返回nil。</returns>
        T RandomKey<T>();

        void Delete(IList<string> keys);

        /// <summary>
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        int Delete(params string[] keys);

        /// <summary>
        ///     查找符合给定模式的key。
        /// </summary>
        /// <param name="match"></param>
        /// <returns>符合给定模式的key列表。</returns>
        [Obsolete("KEYS的速度非常快，但在一个大的数据库中使用它仍然可能造成性能问题，如果你需要从一个数据集中查找特定的key，你最好还是用集合(Set)。")]
        List<string> Keys(string match);

        /// <summary>
        ///     返回列表key的长度。
        /// </summary>
        /// <param name="key"></param>
        /// <returns>列表key的长度。</returns>
        int ListLength(string key);

        /// <summary>
        ///     移除并返回列表key的头元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dtype"></param>
        /// <returns>列表的头元素</returns>
        T LPop<T>(string key, DataType dtype);

        /// <summary>
        ///     移除并返回列表key的尾元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dtype"></param>
        /// <returns>列表的尾元素。</returns>
        T RPOP<T>(string key, DataType dtype);

        /// <summary>
        ///     将一个或多个值value插入到列表key的表头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dtype"></param>
        /// <returns>执行LPUSH命令后，列表的长度</returns>
        int LPUSH(string key, object value, DataType dtype);
        /// <summary>
        ///      将一个或多个值value插入到列表key的表头
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        int Lpush<T>(string key, List<T> members, DataType type);
        /// <summary>
        ///     将列表key下标为index的元素的值设置为value。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="dtype"></param>
        /// <returns>操作成功返回ok，否则返回错误信息。</returns>
        string SetListItem(string key, int index, object value, DataType dtype);

        /// <summary>
        ///     将一个或多个值value插入到列表key的表尾。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dtype"></param>
        /// <returns></returns>
        int RPush(string key, object value, DataType dtype);

        /// <summary>
        ///     返回列表key中指定区间内的元素，区间以偏移量start和stop指定。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="dtype"></param>
        /// <returns></returns>
        IList<T> ListRange<T>(string key, int start, int end, DataType dtype);

        /// <summary>
        ///     返回列表key中指定区间内的元素，区间以偏移量start和stop指定。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dtype"></param>
        /// <returns></returns>
        IList<T> ListRange<T>(string key, DataType dtype);

        /// <summary>
        ///   移除列表中与参数 value 相等的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        int Lrem<T>(string key, T member, DataType type);

        /// <summary>
        ///   移除列表中与参数 value 相等的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        int Lrem<T>(string key, IEnumerable<T> members, DataType type);

        /// <summary>
        ///     返回列表key中，下标为index的元素。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <param name="dtype"></param>
        /// <returns></returns>
        T GetListItem<T>(string key, int index, DataType dtype);

        /// <summary>
        ///     返回哈希表key中，一个或多个给定域的值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <param name="type"></param>
        /// <returns>一个包含多个给定域的关联值的表，表值的排列顺序和给定域参数的请求顺序一样。</returns>
        IList<T> HashGetFields<T>(string key, NameType[] fields, DataType type);

        /// <summary>
        ///     返回哈希表key中给定域field的值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns>给定域的值。</returns>
        T HashGet<T>(string key, string name, DataType type);


        /// <summary>
        ///     返回哈希表key中，所有的域和值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns>以列表形式返回哈希表的域和域的值。 若key不存在，返回空列表。</returns>
        List<Field> HashGetAll<T>(string key, DataType type);
        
        /// <summary>
        ///     检查给定key是否存在。
        /// </summary>
        /// <param name="key"></param>
        /// <returns>若key存在，返回1，否则返回0。</returns>
        int Exists(string key);

        /// <summary>
        ///     查看哈希表key中，给定域field是否存在。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="type"></param>
        /// <returns>
        ///     如果哈希表含有给定域，返回1。
        ///     如果哈希表不含有给定域，或key不存在，返回0。
        /// </returns>
        int HashFieldExists(string key, string field, DataType type);

        /// <summary>
        ///     返回哈希表key中的所有域。(不开放适用)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<string> HashGetAllFields(string key, DataType type);

        /// <summary>
        ///     返回哈希表key中的所有值(key-T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<T> HashGetAllValues<T>(string key, DataType type);

        IEnumerable<T> HashGetAllValues2<T>(string key, DataType type);

        /// <summary>
        ///     返回哈希表key中的所有值（key-List<T>）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<T> HashGetAllValuesT<T>(string key, DataType type);

        /// <summary>
        ///     返回所有(一个或多个)给定key的值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="key"></param>
        /// <param name="key1"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<object> Get<T, T1>(string key, string key1, DataType type);

        /// <summary>
        ///     返回所有(一个或多个)给定key的值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="key"></param>
        /// <param name="key1"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<object> Get<T, T1, T2>(string key, string key1, string key2, DataType type);


        /// <summary>
        ///     返回所有(一个或多个)给定key的值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="key"></param>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <param name="key3"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<object> Get<T, T1, T2, T3>(string key, string key1, string key2, string key3, DataType type);


        /// <summary>
        ///     返回所有(一个或多个)给定key的值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="key"></param>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <param name="key3"></param>
        /// <param name="key4"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IList<object> Get<T, T1, T2, T3, T4>(string key, string key1, string key2, string key3, string key4,
            DataType type);

        /// <summary>
        ///     返回所有(一个或多个)给定key的值。
        /// </summary>
        /// <param name="types"></param>
        /// <param name="keys"></param>
        /// <param name="dtype"></param>
        /// <returns></returns>
        IList<object> Get(Type[] types, string[] keys, DataType dtype);
        /// <summary>
        ///     返回key所关联的字符串值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        ///     返回key所关联的字符串值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        T Get<T>(string key, DataType type);

        /// <summary>
        ///     将给定key的值设为value，并返回key的旧值。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        string GetSet(string key, object value);

        /// <summary>
        ///     将给定key的值设为value，并返回key的旧值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        T GetSet<T>(string key, object value, DataType type);

        /// <summary>
        ///     同时将多个field - value(域-值)对设置到哈希表key中。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string SetFields(string key, string name, object value, DataType type);

        /// <summary>
        ///     将哈希表key中的域field的值设为value。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        int HashSetFieldValue(string key, string field, object value, DataType type);

        /// <summary>
        ///     将哈希表key中的域field的值设置为value，当且仅当域field不存在。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <param name="type"></param>
        /// <returns>设置成功，返回1。如果给定域已经存在且没有操作被执行，返回0。</returns>
        int HashSetFieldValueNx(string key, Field item, DataType type);

        /// <summary>
        ///     同时设置一个或多个key-value对。
        /// </summary>
        /// <param name="kValues"></param>
        /// <param name="dtype"></param>
        void MSet(Field[] kValues, DataType dtype);

        /// <summary>
        ///     将key中储存的数字值增一。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int Incr(string key);

        /// <summary>
        ///     将key所储存的值加上增量increment。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        long Incrby(string key, long increment);

        /// <summary>
        ///     将key中储存的数字值减一。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int Decr(string key);

        /// <summary>
        ///     将key所储存的值减去减量decrement。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="decrement"></param>
        /// <returns></returns>
        long DecrBy(string key, long decrement);

        /// <summary>
        ///     将字符串值value关联到key。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        void Set(string key, object value, DataType type);

        /// <summary>
        ///     将字符串值value关联到key。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="seconds"></param>
        /// <param name="milliseconds"></param>
        /// <param name="existSet"></param>
        /// <param name="type"></param>
        void Set(string key, object value, long? seconds, long? milliseconds, bool? existSet, DataType type);

        /// <summary>
        ///     将key改名为newkey。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool Rename(string key, object value, DataType type);

        /// <summary>
        ///     将一个或多个member元素及其score值加入到有序集key当中。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="sorts"></param>
        /// <param name="dtype"></param>
        /// <returns>被成功添加的新成员的数量，不包括那些被更新的、已经存在的成员。</returns>
        int Zadd(string key, IEnumerable<SortField> sorts, DataType dtype);

        /// <summary>
        ///     返回有序集key的基数。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int Zcard(string key);

        /// <summary>
        ///     返回有序集key中，score值在min和max之间(默认包括score值等于min或max)的成员。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        int Zcount(string key, int min, int max);

        /// <summary>
        ///     返回有序集key中，指定区间内的成员。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        HashSet<string> Zrange(string key, int start, int stop, DataType type);

        /// <summary>
        ///     返回有序集key中，所有score值介于min和max之间(包括等于min或max)的成员。有序集成员按score值递增(从小到大)次序排列。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="isIncludemin"></param>
        /// <param name="max"></param>
        /// <param name="isIncludemax"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        HashSet<T> ZrangeByScore<T>(string key, long min, bool isIncludemin, int max, bool isIncludemax,
            DataType type);

        /// <summary>
        ///     返回有序集key中，指定区间内的成员。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        HashSet<string> Zrevrange(string key, int start, int stop, DataType type);

        /// <summary>
        ///     返回有序集key中成员member的排名。其中有序集成员按score值递增(从小到大)顺序排列。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        int Zrank(string key, string member);

        /// <summary>
        ///     返回有序集key中成员member的排名。其中有序集成员按score值递减(从大到小)排序。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        int Zrevrank(string key, string member);

        /// <summary>
        ///     移除有序集key中的一个或多个成员，不存在的成员将被忽略。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        void Zrem(string key, int member);

        /// <summary>
        ///     返回有序集key中，成员member的score值。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        int Zscore(string key, string member);

        /// <summary>
        ///     将一个或多个member元素加入到集合key当中，已经存在于集合的member元素将被忽略。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        int Sadd<T>(string key, List<T> members, DataType type);

        /// <summary>
        /// 将member元素加入到集合key当中，已经存在于集合的member元素将被忽略。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        int Sadd<T>(string key, T member, DataType type);

        /// <summary>
        ///     返回集合key的基数(集合中元素的数量)。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int Scard(string key);

        /// <summary>
        ///     判断member元素是否是集合key的成员。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool Sismember<T>(string key, T member, DataType type);

        /// <summary>
        ///     返回集合key中的所有成员。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<T> Smember<T>(string key, DataType type);

        /// <summary>
        ///     移除集合key中的一个或多个member元素，不存在的member元素会被忽略。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        int Srem<T>(string key, T member, DataType type);

        /// <summary>
        ///     移除集合key中多个member元素，不存在的member元素会被忽略。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        int Srem<T>(string key, IEnumerable<T> members, DataType type);

        /// <summary>
        ///     返回或保存给定列表、集合、有序集合key中经过排序的元素。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="bYpattern"></param>
        /// <param name="geTpattern"></param>
        /// <param name="alpha"></param>
        /// <param name="storeDestination"></param>
        /// <param name="orderby"></param>
        /// <param name="type"></param>
        /// <param name="dtype"></param>
        /// <returns></returns>
        IList<object> Sort(string key, int? offset, int? count, string bYpattern, string geTpattern, bool alpha,
            string storeDestination,
            SortOrderType orderby, Type type, DataType dtype);


        /// <summary>
        ///     将一个或多个值value插入到列表key的表头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dtype"></param>
        /// <returns>执行LPUSH命令后，列表的长度</returns>
        Task LpushAsync(string key, object value, DataType dtype);

        /// <summary>
        ///     将一个或多个值value插入到列表key的表头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dtype"></param>
        /// <returns>执行LPUSH命令后，列表的长度</returns>
        Task LpushAsync<T>(string key, T value, DataType dtype);

        /// <summary>
        ///      将一个或多个值value插入到列表key的表头
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <param name="type"></param>
        /// <returns></returns>
       Task LpushAsync<T>(string key, List<T> members, DataType type);

        /// <summary>
        ///   移除列表中与参数 value 相等的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task LremAsync<T>(string key, T member, DataType type);

        /// <summary>
        ///   移除列表中与参数 value 相等的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task LremAsync<T>(string key, IEnumerable<T> members, DataType type);

        #region 订阅与发布

        /// <summary>
        /// 发布消息到指定频道
        /// </summary>
        /// <param name="channel">频道</param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        object Publish(string channel, string message);
         void Subscribe(string channelName);
   

         void PSubscribe(string channelName);

         void UnSubscribe(string channelName);

         void UnPSubscribe(string channelName);



        #endregion
    }
}
