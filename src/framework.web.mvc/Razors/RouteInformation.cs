namespace Orion.Framework.Web.Mvc.Razors {
    /// <summary>
    /// 
    /// </summary>
    public class RouteInformation {
        /// <summary>
        /// 
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Invocation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TemplatePath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPartialView { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool IsPageRoute { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool Ignore { get; set; } = false;
    }
}
