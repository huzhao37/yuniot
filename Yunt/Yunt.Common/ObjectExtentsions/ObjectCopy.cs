using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ProtoBuf;
namespace Yunt.Common.ObjectExtentsions
{
    public static class ObjectExtensions
    {
        public static object Copy(this object obj)
        {
            Object targetDeepCopyObj;
            Type targetType = obj.GetType();
            //值类型  
            if (targetType.IsValueType == true)
            {
                targetDeepCopyObj = obj;
            }
            //引用类型   
            else
            {
                targetDeepCopyObj = Activator.CreateInstance(targetType);   //创建引用对象   
                MemberInfo[] memberCollection = obj.GetType().GetMembers();

                foreach (MemberInfo member in memberCollection)
                {
                    if (member.MemberType == MemberTypes.Field)
                    {
                        FieldInfo field = (FieldInfo)member;
                        Object fieldValue = field.GetValue(obj);
                        if (fieldValue is ICloneable)
                        {
                            field.SetValue(targetDeepCopyObj, (fieldValue as ICloneable).Clone());
                        }
                        else
                        {
                            field.SetValue(targetDeepCopyObj, Copy(fieldValue));
                        }

                    }
                    else if (member.MemberType == MemberTypes.Property)
                    {
                        PropertyInfo myProperty = (PropertyInfo)member;
                        MethodInfo info = myProperty.GetSetMethod(false);
                        if (info != null)
                        {
                            object propertyValue = myProperty.GetValue(obj, null);
                            if (propertyValue is ICloneable)
                            {
                                myProperty.SetValue(targetDeepCopyObj, (propertyValue as ICloneable).Clone(), null);
                            }
                            else
                            {
                                myProperty.SetValue(targetDeepCopyObj, Copy(propertyValue), null);
                            }
                        }

                    }
                }
            }
            return targetDeepCopyObj;
        }
        /// <summary>
        /// 克隆复杂对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IEnumerable<T> Clone<T>(IEnumerable<T> obj)
        {
            //复制一份，在从redis中删除时使用
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, obj);
                ms.Position = 0;
                var cloneDtos =
                    Serializer.Deserialize<IEnumerable<T>>(ms);
                ms.Dispose();
                return cloneDtos;
            }
        }
        /// <summary>
        /// 克隆复杂对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Clone<T>(this T obj)
        {
            //复制一份，在从redis中删除时使用
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, obj);
                ms.Position = 0;
                var cloneDto =
                    Serializer.Deserialize<T>(ms);
                ms.Dispose();
                return cloneDto;
            }
        }


        /// <summary>
        /// 转换两个不同类型但是成员相同的对象
        /// </summary>
        /// <typeparam name="T">目标对象</typeparam>
        /// <param name="source">待转换对象</param>
        /// <returns></returns>
        public static T CopySameFieldsObject<T>(this Object source)
        {
            Type srcT = source.GetType();
            Type destT = typeof(T);

            // 构造一个要转换对象实例
            Object instance = destT.InvokeMember("", BindingFlags.CreateInstance, null, null, null);

            // 这里指定搜索所有公开和非公开的字段
            FieldInfo[] srcFields = srcT.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            // 将源头对象的每个字段的值分别赋值到转换对象里，因为假定字段都一样，这里就不做容错处理了
            foreach (FieldInfo field in srcFields)
            {
                destT.GetField(field.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).
                    SetValue(instance, field.GetValue(source));
            }

            return (T)instance;
        }
    }
    
}
