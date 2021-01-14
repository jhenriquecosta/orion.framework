using System;
using System.Reflection;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Zeus.Aspects.Base;
using Zeus.DataLayer.UnitOfWorks;
using Zeus.Domains.Repositories;

namespace Zeus.DataLayer.NHibernate.Aspects
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAttribute : InterceptorBase
    {
        IUnitOfWork _unitOfWork;
        public UnitOfWorkAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var session = _unitOfWork.Session;

            //var manager = context.ServiceProvider.GetService<IUnitOfWorkManager>();

           // var unitOfWork= context.ServiceProvider.GetService<IUnitOfWorkBase>();
           // var serviceProvider = context.ServiceProvider.GetService<ISingleSessionFactoryConfigurer>(); 

           // if (NHUnitOfWork.Current != null || !CallByAttribute(context.ServiceMethod))
           // {
           //     await next(context);
           //     return;
           // }
           // try
           // {
           //     NHUnitOfWork.Current = new NHUnitOfWork(serviceProvider);
           //     NHUnitOfWork.Current.BeginTransaction();

           ////     unitOfWork.BeginTransaction();
           //     try
           //     {
           //         await next(context);
           //         await unitOfWork.CommitAsync();
           //     }
           //     catch
           //     {
           //         try
           //         {
           ////             await unitOfWork.RollbackAsync();
           //         }
           //         catch(Exception exception)
           //         {
           //             throw;
           //         }

           //         throw;
           //     }
           // }
           // finally
           // {
           //     NHUnitOfWork.Current = null;
           // }
            //var val = context.ReturnValue;
            //context.ReturnValue = val + 1;
        }
        private static bool CallByAttribute(MethodInfo methodInfo)
        {
            if (UoWHelper.HasUnitOfWorkAttribute(methodInfo))
            {
                return true;
            }
            if (UoWHelper.IsRepositoryMethod(methodInfo))
            {
                return true;
            }

            return false;
    
        }
    }


    internal static class UoWHelper
    {
        public static bool IsRepositoryMethod(MethodInfo methodInfo)
        {
            return IsRepositoryClass(methodInfo.DeclaringType);
        }

        public static bool IsRepositoryClass(Type type)
        {
            return typeof(IRepository<>).IsAssignableFrom(type);
        }

        public static bool HasUnitOfWorkAttribute(MethodInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }
    }

}
