using Orion.Framework.Exceptions;
using Orion.Framework.Objects;
using Orion.Framework.Sessions;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;
using Orion.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IDisposable = System.IDisposable;


namespace Orion.Framework.DataLayer.UnitOfWorks
{
    /// <inheritdoc />
    /// <summary>
    ///     Base for all Unit Of Work classes.
    /// </summary>
    [DebuggerDisplay("Id = {" + nameof(Id) + "}")]
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        private readonly List<DataFilterConfiguration> _filters;

        /// <summary>
        ///     A reference to the exception if this unit of work failed.
        /// </summary>
        private Exception _exception;

        /// <summary>
        ///     Is <see cref="Begin" /> method called before?
        /// </summary>
        private bool _isBeginCalledBefore;

        /// <summary>
        ///     Is <see cref="Complete" /> method called before?
        /// </summary>
        private bool _isCompleteCalledBefore;

        /// <summary>
        ///     Is this unit of work successfully completed.
        /// </summary>
        private bool _succeed;

        /// <summary>
        ///     Constructor.
        /// </summary>
        protected UnitOfWorkBase(IUnitOfWorkDefaultOptions defaultOptions,IUnitOfWorkFilterExecuter filterExecuter)
        {
            FilterExecuter = filterExecuter;
            DefaultOptions = defaultOptions;
    
            Id = Guid.NewGuid().ToString("N");
            _filters = defaultOptions.Filters.ToList();
            AppSession = NullSession.Instance;
           
        }

        /// <summary>
        ///     Gets default UOW options.
        /// </summary>
        protected IUnitOfWorkDefaultOptions DefaultOptions { get; }

      
        /// <summary>
        ///     Reference to current  session.
        /// </summary>
        public ISession AppSession { protected get; set; }

        protected IUnitOfWorkFilterExecuter FilterExecuter { get; }

        public string Id { get; }

      
        public IUnitOfWork Outer { get; set; }

        public event EventHandler Completed;

        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        public event EventHandler Disposed;

        public UnitOfWorkOptions Options { get; private set; }

        public IReadOnlyList<DataFilterConfiguration> Filters => _filters.ToImmutableList();

        /// <inheritdoc />
        /// <summary>
        ///     Gets a value indicates that this unit of work is disposed or not.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <inheritdoc />
        public void Begin(UnitOfWorkOptions options)
        {
            Check.NotNull(options, nameof(options));

            PreventMultipleBegin();
            Options = options; //TODO: Do not set options like that, instead make a copy?

            SetFilters(options.FilterOverrides);

            BeginUow();
        }

        /// <inheritdoc />
        public abstract void SaveChanges();

        /// <inheritdoc />
        public abstract Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <inheritdoc />
        public IDisposable DisableFilter(params string[] filterNames)
        {
            //TODO: Check if filters exists?

            var disabledFilters = new List<string>();

            foreach (string filterName in filterNames)
            {
                int filterIndex = GetFilterIndex(filterName);
                if (_filters[filterIndex].IsEnabled)
                {
                    disabledFilters.Add(filterName);
                    _filters[filterIndex] = new DataFilterConfiguration(_filters[filterIndex], false);
                }
            }

            disabledFilters.ForEach(ApplyDisableFilter);

            return new DisposeAction(() => EnableFilter(disabledFilters.ToArray()));
        }

        /// <inheritdoc />
        public IDisposable EnableFilter(params string[] filterNames)
        {
            //TODO: Check if filters exists?

            var enabledFilters = new List<string>();

            foreach (string filterName in filterNames)
            {
                int filterIndex = GetFilterIndex(filterName);
                if (!_filters[filterIndex].IsEnabled)
                {
                    enabledFilters.Add(filterName);
                    _filters[filterIndex] = new DataFilterConfiguration(_filters[filterIndex], true);
                }
            }

            enabledFilters.ForEach(ApplyEnableFilter);

            return new DisposeAction(() => DisableFilter(enabledFilters.ToArray()));
        }

        /// <inheritdoc />
        public bool IsFilterEnabled(string filterName)
        {
            return GetFilter(filterName).IsEnabled;
        }

        /// <inheritdoc />
        public IDisposable SetFilterParameter(string filterName, string parameterName, object value)
        {
            int filterIndex = GetFilterIndex(filterName);

            var newfilter = new DataFilterConfiguration(_filters[filterIndex]);

            //Store old value
            object oldValue = null;
            bool hasOldValue = newfilter.FilterParameters.ContainsKey(parameterName);
            if (hasOldValue)
            {
                oldValue = newfilter.FilterParameters[parameterName];
            }

            newfilter.FilterParameters[parameterName] = value;

            _filters[filterIndex] = newfilter;

            ApplyFilterParameterValue(filterName, parameterName, value);

            return new DisposeAction(() =>
            {
                //Restore old value
                if (hasOldValue)
                {
                    SetFilterParameter(filterName, parameterName, oldValue);
                }
            });
        }

        /// <inheritdoc />
        public void Complete()
        {
            PreventMultipleComplete();
            try
            {
                CompleteUow();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <inheritdoc />
        public async Task CompleteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            PreventMultipleComplete();
            try
            {
                await CompleteUowAsync(cancellationToken);
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (!_isBeginCalledBefore || IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (!_succeed)
            {
                OnFailed(_exception);
            }

            DisposeUow();
            OnDisposed();
            DisposeDelegates();
        }

        /// <summary>
        ///     Can be implemented by derived classes to start UOW.
        /// </summary>
        protected virtual void BeginUow()
        {
        }

        /// <summary>
        ///     Should be implemented by derived classes to complete UOW.
        /// </summary>
        protected abstract void CompleteUow();

        /// <summary>
        ///     Should be implemented by derived classes to complete UOW.
        /// </summary>
        protected abstract Task CompleteUowAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Should be implemented by derived classes to dispose UOW.
        /// </summary>
        protected abstract void DisposeUow();

        protected virtual void ApplyDisableFilter(string filterName)
        {
            FilterExecuter.ApplyDisableFilter(this, filterName);
        }

        protected virtual void ApplyEnableFilter(string filterName)
        {
            FilterExecuter.ApplyEnableFilter(this, filterName);
        }

        protected virtual void ApplyFilterParameterValue(string filterName, string parameterName, object value)
        {
            FilterExecuter.ApplyFilterParameterValue(this, filterName, parameterName, value);
        }

      

        /// <summary>
        ///     Called to trigger <see cref="Completed" /> event.
        /// </summary>
        protected virtual void OnCompleted()
        {
            Completed.InvokeSafely(this);
        }

        /// <summary>
        ///     Called to trigger <see cref="Failed" /> event.
        /// </summary>
        /// <param name="exception">Exception that cause failure</param>
        protected virtual void OnFailed(Exception exception)
        {
            Failed.InvokeSafely(this, new UnitOfWorkFailedEventArgs(exception));
        }

        /// <summary>
        ///     Called to trigger <see cref="Disposed" /> event.
        /// </summary>
        protected virtual void OnDisposed()
        {
            Disposed.InvokeSafely(this);
        }

        private void PreventMultipleBegin()
        {
            if (_isBeginCalledBefore)
            {
                throw new Warning("This unit of work has started before. Can not call Start method more than once.");
            }

            _isBeginCalledBefore = true;
        }

        private void PreventMultipleComplete()
        {
            if (_isCompleteCalledBefore)
            {
                throw new Warning("Complete is called before!");
            }

            _isCompleteCalledBefore = true;
        }

        private void SetFilters(List<DataFilterConfiguration> filterOverrides)
        {
            for (var i = 0; i < _filters.Count; i++)
            {
                DataFilterConfiguration filterOverride = filterOverrides.FirstOrDefault(f => f.FilterName == _filters[i].FilterName);
                if (filterOverride != null)
                {
                    _filters[i] = filterOverride;
                }
            }
        }

        private DataFilterConfiguration GetFilter(string filterName)
        {
            DataFilterConfiguration filter = _filters.FirstOrDefault(f => f.FilterName == filterName);
            if (filter == null)
            {
                throw new Warning("Unknown filter name: " + filterName + ". Be sure this filter is registered before.");
            }

            return filter;
        }

        private int GetFilterIndex(string filterName)
        {
            int filterIndex = _filters.FindIndex(f => f.FilterName == filterName);
            if (filterIndex < 0)
            {
                throw new Warning("Unknown filter name: " + filterName + ". Be sure this filter is registered before.");
            }

            return filterIndex;
        }

        public override string ToString()
        {
            return $"[UnitOfWork {Id}]";
        }

        private void DisposeDelegates()
        {
            if (Failed != null) { Failed -= Failed; }
            if (Completed != null) { Completed -= Completed; }
            if (Disposed != null) { Disposed -= Disposed; }
        }
    }
}
