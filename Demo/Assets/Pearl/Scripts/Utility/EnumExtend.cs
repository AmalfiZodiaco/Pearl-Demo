using System;
using UnityEngine;

namespace Pearl
{
    public static class EnumExtend
    {
        private static int auxInteger;

        public static T GetRandom<T>() where T : struct, IConvertible
        {
            Debug.Assert(typeof(T).IsEnum);

            auxInteger = UnityEngine.Random.Range(0, Length<T>());
            return (T)Enum.ToObject(typeof(T), auxInteger);
        }

        public static T ParseEnum<T>(string value)
        {
            Debug.Assert(typeof(T).IsEnum);

            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static int Length<T>() where T : struct, IConvertible
        {
            Debug.Assert(typeof(T).IsEnum);

            return Enum.GetNames(typeof(T)).Length;
        }

        public static T GetInverse<T>(T value) where T : struct, IConvertible
        {
            Debug.Assert(typeof(T).IsEnum && Enum.GetNames(typeof(T)).Length == 2);

            auxInteger = Convert.ToInt32(value);
            auxInteger = MathfExtend.AddValueInCircle(auxInteger, 1, 2);
            return (T)Enum.ToObject(typeof(T), auxInteger);
        }
    }

}