using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Orion.Framework.Aspects.Base;

namespace Orion.Framework.DataLayer.NHibernate.Aspects
{

    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionalAttribute : InterceptorBase
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            await Task.CompletedTask;
        }
    }
}