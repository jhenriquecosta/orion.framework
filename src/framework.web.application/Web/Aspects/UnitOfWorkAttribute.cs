using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Orion.Framework.Aspects.Base;
using Orion.Framework.DataLayer.NHibernate.Helpers;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;

namespace Orion.Framework.DataLayer.Web.Aspects
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAttribute : InterceptorBase
    {


        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            //get type
            var type = context.ServiceMethod.DeclaringType.GenericTypeArguments[0];
            var _unitofWork = (IUnitOfWork)NHibernateHelper.Instance.GetUnitOfWork(type);
            _unitofWork.BeginTransaction();
            await next(context);
            await _unitofWork.CommitAsync();
        }
    }
    

}
