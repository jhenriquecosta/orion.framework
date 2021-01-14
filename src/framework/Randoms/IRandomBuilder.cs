using System;

namespace Orion.Framework.Randoms {
    /// <summary>
    /// 
    /// </summary>
    public interface IRandomBuilder {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxLength"></param>
        /// <param name="text">，</param>
        string GenerateString( int maxLength, string text = null );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxLength"></param>
        string GenerateLetters( int maxLength );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxLength"></param>
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxLength"></param>
        string GenerateNumbers( int maxLength );
        /// <summary>
        /// 
        /// </summary>
        bool GenerateBool();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxValue"></param>
        int GenerateInt( int maxValue );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginYear"></param>
        /// <param name="endYear"></param>
        DateTime GenerateDate( int beginYear = 1980, int endYear = 2080 );
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        TEnum GenerateEnum<TEnum>();
    }
}
