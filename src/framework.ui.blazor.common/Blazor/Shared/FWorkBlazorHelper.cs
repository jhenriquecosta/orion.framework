using Orion.Framework.Domains.ValueObjects;
using Orion.Framework.Utilities;
using Orion.Framework.Web.Applications.Services;
using System;
using System.Collections.Generic;

namespace Orion.Framework.Ui.Blazor.Shared
{
    public static class FWorkBlazorHelper
    {
        public static DateTime? GetCurrentDate()
        {
            return  DateTime.Now;
        }
        public static int GetCurrentYear()
        {
            return DateTime.Now.Year;
        }
        public static List<DataItemCombo> LookUp<TEntity>()
        {
            var lookup = AsyncUtil.RunSync(() =>  LookupService.GetLookUpAsync<TEntity>());
            return lookup;
        }
        
    }
}
