namespace Orion.Framework.Logs.Aspects {
    /// <summary>
    /// 
    /// </summary>
    public class DebugLogAttribute : LogAttributeBase {
        /// <summary>
        /// 
        /// </summary>
        protected override bool Enabled( ILog log ) {
            return log.IsDebugEnabled;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void WriteLog( ILog log ) {
            log.Debug();
        }
    }
}
