using System;
using UnityEngine;

namespace it.amalfi.Pearl
{
    public static class EnumExtend
    {
        public static T GetRandom<T>() where T : struct, IConvertible
        {
            Debug.Assert(typeof(T).IsEnum);

            int aux = Enum.GetNames(typeof(T)).Length;
            aux = UnityEngine.Random.Range(0, aux);
            return (T)Enum.ToObject(typeof(T), aux);
        }

        public static T ParseEnum<T>(string value)
        {
            Debug.Assert(typeof(T).IsEnum);

            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static int Lenght<T>() where T : struct, IConvertible
        {
            Debug.Assert(typeof(T).IsEnum);

            return Enum.GetNames(typeof(T)).Length;
        }

        public static T GetInverse<T>(T value) where T : struct, IConvertible
        {
            Debug.Assert(typeof(T).IsEnum && Enum.GetNames(typeof(T)).Length == 2);

            byte index = Convert.ToByte(value);
            index = MathfExtend.AddValueInCircle(index, 1, 2);
            return (T)Enum.ToObject(typeof(T), index);
        }
    }

}