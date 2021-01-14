using System;

namespace Orion.Framework.Exceptions.Prompts {
    /// <summary>
    /// 
    /// </summary>
    public interface IExceptionPrompt {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        string GetPrompt( Exception exception );
    }
}
