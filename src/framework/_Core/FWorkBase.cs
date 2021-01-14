using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Orion.Framework.DataLayer.UnitOfWorks;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;
using Orion.Framework.Exceptions;
using Orion.Framework.Logs;
using Orion.Framework.Logs.Core;
using Orion.Framework.Objects;
using Orion.Framework.Sessions;

namespace Orion.Framework
{
    /// <summary>
    ///     This class can be used as a base class for services.
    ///     It has some useful objects property-injected and has some basic methods
    ///     most of services may need to.
    /// </summary>
    public abstract class FWorkBaseComponent
    {
        private IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        ///     Constructor.
        /// </summary>

        protected FWorkBaseComponent()
        {
            Log = NullLog.Instance;
            AppSession = NullSession.Instance;
        }

        public ILog Log { get; set; }


        public ISession AppSession { get; set; }

        /// <summary>
        ///     Reference to <see cref="IUnitOfWorkManager" />.
        /// </summary>
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new Warning("Must set UnitOfWorkManager before use it.");
                }

                return _unitOfWorkManager;
            }
            set => _unitOfWorkManager = value;
        }

        /// <summary>
        ///     Gets current unit of work.
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork => UnitOfWorkManager.Current;
        protected async Task UseUow(Func<Task> func, Action<UnitOfWorkOptions> optsAction = null, CancellationToken cancellationToken = default)
        {
            var options = new UnitOfWorkOptions();

            optsAction?.Invoke(options);

            using (IUnitOfWorkCompleteHandle uow = UnitOfWorkManager.Begin(options))
            {
                await func();

                await uow.CompleteAsync(cancellationToken);
            }
        }
        protected async Task<TResponse> UseUow<TResponse>(Func<Task<TResponse>> func, Action<UnitOfWorkOptions> optsAction = null, CancellationToken cancellationToken = default)
        {
            var options = new UnitOfWorkOptions();
            optsAction?.Invoke(options);

            TResponse response;
            using (IUnitOfWorkCompleteHandle uow = UnitOfWorkManager.Begin(options))
            {
                response = await func();

                await uow.CompleteAsync(cancellationToken);
            }

            return response;
        }

        protected void OnUowCompleted(Action action)
        {
            CurrentUnitOfWork.Completed += (sender, args) =>
            {
                try
                {
                    action();
                }
                catch (Exception exception)
                {
                    Log.Fatal(exception.Message);
                }
            };
        }
        protected T The<T>()
        {
            return Ioc.Create<T>();
        }
        protected System.IDisposable MeasurePerformance(string methodNameOrText)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Log.Info(methodNameOrText + " starting...");

            return new DisposeAction(() =>
            {
                stopwatch.Stop();
                Log.Info($"{methodNameOrText} takes {stopwatch.Elapsed.Minutes} minutes, {stopwatch.Elapsed.Seconds} seconds, {stopwatch.Elapsed.Milliseconds} miliseconds.");
            });
        }


    }
}