using System;

namespace Orion.Framework.UI.Attributes {
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Property )]
    public class ModelAttribute : Attribute {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public ModelAttribute( string model = "model" ) {
            Model = model;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// ，
        /// </summary>
        public bool Ignore { get; set; }
    }
}
