using System.Collections.Generic;

namespace Pearl
{
    /// <summary>
    /// This class extends the dictonary
    /// </summary>
    public static class DictonaryExtend
    {
        #region Public Methods
        /// <summary>
        /// This method update a value with specific key, 
        /// if there isn't key, the method create new pair key-value
        /// </summary>
        /// <param name = "key">The key of value.</param>
        /// <param name = "value">The value.</param>
        public static void Update<T, F>(this Dictionary<T, F> dictonary, T key, F value)
        {
            if (dictonary.ContainsKey(key))
                dictonary[key] = value;
            else
                dictonary.Add(key, value);
        }

        #endregion
    }
}
