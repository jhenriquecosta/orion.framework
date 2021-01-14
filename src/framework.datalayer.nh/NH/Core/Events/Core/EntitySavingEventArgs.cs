using System;
using NHibernate;
using Zeus.Domains;

// ReSharper disable CheckNamespace
namespace Zeus.NHibernate.Events
// ReSharper restore CheckNamespace
{
    public class EntitySavingEventArgs : EventArgs
    {
        public IEntity Entity { get; set; }

        public ISession Session { get; set; }

        public EntitySavingEventArgs(IEntity entity, ISession session)
        {
            Entity = entity;
            Session = session;
        }
    }
}
