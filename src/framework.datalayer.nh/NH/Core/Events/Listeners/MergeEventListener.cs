using NHibernate.Event;
using NHibernate.Event.Default;
using NHibernate.Persister.Entity;
using System.Threading;
using System.Threading.Tasks;
using Zeus.Domains;

namespace Zeus.NHibernate.Events.Listeners
{
    /// <summary>
    /// nHibernate Save Or Update Event Listener
    /// </summary>
    public class MergeEventListener : DefaultMergeEventListener
    {
        /// <summary>
        /// Event listener helper
        /// </summary>
        private readonly EventListenerHelper eventListenerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveOrUpdateEventListener" /> class.
        /// </summary>
        /// <param name="eventListenerHelper">The event listener helper.</param>
        public MergeEventListener(EventListenerHelper eventListenerHelper)
        {
            this.eventListenerHelper = eventListenerHelper;
        }

        /// <summary>
        /// Perfome merge
        /// </summary>
        /// <param name="evt">The save or update event.</param>
        /// <returns>
        /// The id used to save the entity; may be null depending on the
        /// type of id generator used and the requiresImmediateIdAccess value
        /// </returns>
        public override void OnMerge(MergeEvent evt)
        {
          
            eventListenerHelper.OnModify(evt.Entity);
            base.OnMerge(evt);
        }
        public override async Task OnMergeAsync(MergeEvent evt,CancellationToken cancellationToken)
        {
            eventListenerHelper.OnModify(evt.Original);
            await base.OnMergeAsync(evt,cancellationToken);
        }
    }

        
}