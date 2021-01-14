namespace Orion.Framework.Randoms {
    /// <summary>
    /// 
    /// </summary>
    public class RandomNumberGenerator : IRandomNumberGenerator {
        /// <summary>
        /// 
        /// </summary>
        private readonly System.Random _random;

        /// <summary>
        /// 
        /// </summary>
        public RandomNumberGenerator() {
            _random = new System.Random();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public int Generate( int min, int max ) {
            return _random.Next( min, max );
        }
    }
}
