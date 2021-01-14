using System;
using System.Text;
using Orion.Framework.Helpers;

namespace Orion.Framework.Randoms {
    /// <summary>
    /// 
    /// </summary>
    public class RandomBuilder : IRandomBuilder {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="generator"></param>
        public RandomBuilder( IRandomNumberGenerator generator = null ) {
            _random = generator ?? new RandomNumberGenerator();
        }

        /// <summary>
        ///  
        /// </summary>
        private readonly IRandomNumberGenerator _random;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxLength"></param>
        /// <param name="text">，</param>
        public string GenerateString( int maxLength, string text = null ) {
            if( text == null )
                text = Const.Letters + Const.Numbers;
            var result = new StringBuilder();
            var length = GetRandomLength( maxLength );
            for( int i = 0; i < length; i++ )
                result.Append( GetRandomChar( text ) );
            return result.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        private int GetRandomLength( int maxLength ) {
            return _random.Generate( 1, maxLength );
        }

        /// <summary>
        /// 
        /// </summary>
        private string GetRandomChar( string text ) {
            return text[_random.Generate( 1, text.Length )].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxLength"></param>
        public string GenerateLetters( int maxLength ) {
            return GenerateString( maxLength, Const.Letters );
        }

        /// <summary>
        /// 生成随机汉字
        /// </summary>
        /// <param name="maxLength"></param>
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxLength"></param>
        public string GenerateNumbers( int maxLength ) {
            return GenerateString( maxLength, Const.Numbers );
        }

        /// <summary>
        /// 
        /// </summary>
        public bool GenerateBool() {
            var random = _random.Generate( 1, 100 );
            if( random % 2 == 0 )
                return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxValue"></param>
        public int GenerateInt( int maxValue ) {
            return _random.Generate( 0, maxValue + 1 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="beginYear"></param>
        /// <param name="endYear"></param>
        public DateTime GenerateDate( int beginYear = 1980, int endYear = 2080 ) {
            var year = _random.Generate( beginYear, endYear );
            var month = _random.Generate( 1, 13 );
            var day = _random.Generate( 1, 29 );
            var hour = _random.Generate( 1, 24 );
            var minute = _random.Generate( 1, 60 );
            var second = _random.Generate( 1, 60 );
            return new DateTime( year, month, day, hour, minute, second );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        public TEnum GenerateEnum<TEnum>() {
            var list = Orion.Framework.Helpers.HelperEnum.GetItems<TEnum>();
            int index = _random.Generate( 0, list.Count );
            return Orion.Framework.Helpers.HelperEnum.Parse<TEnum>( list[index].Value );
        }
    }
}
