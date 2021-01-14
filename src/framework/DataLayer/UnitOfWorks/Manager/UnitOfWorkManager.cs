using Orion.Framework.Dependency;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;
using System.Linq;
using System.Transactions;

namespace Orion.Framework.DataLayer.UnitOfWorks
{
    /// <summary>
    ///     Unit of work manager.
    /// </summary>
    /// 

    internal class UnitOfWorkManager : IUnitOfWorkManager
    {
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;
        private readonly IUnitOfWorkDefaultOptions _defaultOptions;
        private IScope _childScope;

        public UnitOfWorkManager(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider,IUnitOfWorkDefaultOptions defaultOptions)
        {
            //_scopeResolver = scopedResolver;
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
            _defaultOptions = defaultOptions;
            // System.Console.WriteLine("Chamando UnitOfWorkManager");
        }

        public IActiveUnitOfWork Current => _currentUnitOfWorkProvider.Current;

        public IUnitOfWorkCompleteHandle Begin()
        {
            return Begin(new UnitOfWorkOptions());
        }

        public IUnitOfWorkCompleteHandle Begin(TransactionScopeOption scope)
        {
            return Begin(new UnitOfWorkOptions { Scope = scope });
        }

        public IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options)
        {
           
            _childScope = Ioc.BeginScope();

            options.FillDefaultsForNonProvidedOptions(_defaultOptions);

            IUnitOfWork outerUow = _currentUnitOfWorkProvider.Current;

            if (options.Scope == TransactionScopeOption.Required && outerUow != null)
            {
                return new InnerUnitOfWorkCompleteHandle();
            }

            var uow = _childScope.Create<IUnitOfWork>();  //AppHelper.GetService<IUnitOfWork>();

            uow.Completed += (sender, args) => { _currentUnitOfWorkProvider.Current = null; };

            uow.Failed += (sender, args) => { _currentUnitOfWorkProvider.Current = null; };

            uow.Disposed += (sender, args) => { _childScope.Dispose(); };

            //Inherit filters from outer UOW
            if (outerUow != null)
            {
                options.FillOuterUowFiltersForNonProvidedOptions(outerUow.Filters.ToList());
            }

            uow.Begin(options);      
            _currentUnitOfWorkProvider.Current = uow;
        //    System.Console.WriteLine(uow.GetType().Name);
            return uow;
        }
    }
}
