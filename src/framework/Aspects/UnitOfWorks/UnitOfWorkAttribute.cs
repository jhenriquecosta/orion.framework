using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.AspectScope;
using Microsoft.Extensions.DependencyInjection;
using Orion.Framework.Aspects.Base;
using Orion.Framework.DataLayer.UnitOfWorks;

namespace Orion.Framework.Infrastructurelications.Aspects 
{


    #region OBSOLETE
    //public class UnitOfWorkAttribute : InterceptorBase, IScopeInterceptor
    //{

    //    public Scope Scope { get; set; } = Scope.Aspect;


    //    public override async Task Invoke(AspectContext context, AspectDelegate next)
    //    {
    //        //before... execute begin transactions
    //        var manager = context.ServiceProvider.GetService<IUnitOfWorkBase>();
    //        if (manager == null) return;
    //        try
    //        {
    //          //  manager.BeginTransaction();
    //        }
    //        catch (Exception ex)
    //        {
    //            throw;
    //        }
    //        //execute the method
    //        try
    //        {
    //            await next(context);
    //        }
    //        catch (Exception ex)
    //        {
    //         //   await manager.RollbackAsync();
    //            throw;
    //        }
    //        finally
    //        {
    //            await manager.CommitAsync();
    //            if (context.Implementation is ICommitAfter service)
    //                service.CommitAfter();
    //        }
    //    }
    //}
    #endregion
}
