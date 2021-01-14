using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Domains.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class ModelAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public ModelAttribute(string model = "model")
        {
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
