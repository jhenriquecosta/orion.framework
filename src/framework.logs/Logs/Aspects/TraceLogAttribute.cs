namespace Orion.Framework.Logs.Aspects {
    /// <summary>
    /// 
    /// </summary>
    public class TraceLogAttribute : LogAttributeBase {
        /// <summary>
        /// </summary>
        protected override bool Enabled( ILog log ) {
            return log.IsTraceEnabled;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void WriteLog( ILog log ) {
            log.Trace();
        }
    }
}
