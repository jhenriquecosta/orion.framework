﻿using System.Collections.Generic;

namespace Orion.Framework.Web.Mvc.Razors {
    /// <summary>
    /// 路由分析器
    /// </summary>
    public interface IRouteAnalyzer {
        /// <summary>
        /// 获取所有路由信息
        /// </summary>
        IEnumerable<RouteInformation> GetAllRouteInformations();
    }
}
