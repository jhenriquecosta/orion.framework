using System;

namespace Orion.Framework.Domains
{

    /// <summary>
    /// An entity can implement this interface if <see cref="LastModificationTime"/> of this entity must be stored.
    /// <see cref="LastModificationTime"/> is automatically set when updating <see cref="Entity"/>.
    /// </summary>
    public interface IHasModificationTime
    {
        /// <summary>
        /// The last modified time for this entity.
        /// </summary>
        DateTime? ChangedOn { get; set; }
    }
    /// <summary>
    /// This interface is implemented by entities that is wanted to store modification information (who and when modified lastly).
    /// Properties are automatically set when updating the <see cref="IEntity"/>.
    /// </summary>
    public interface IModificationAudited : IHasModificationTime
    {
        /// <summary>
        /// Last modifier user for this entity.
        /// </summary>
        string ChangedUser { get; set; }
    }
    public interface IChangedAudited : IModificationAudited
    {
       
    }
   
}
