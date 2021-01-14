using Orion.Framework.Helpers;

namespace Orion.Framework.Randoms {
    /// <summary>
    /// 
    /// </summary>
    public class GuidRandomGenerator : IRandomGenerator {
        /// <summary>
        /// 
        /// </summary>
        public string Generate() {
            return Id.Guid();
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly IRandomGenerator Instance = new GuidRandomGenerator();
    }
}
