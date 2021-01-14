using System;

namespace Orion.Framework.Domains
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
    /// <summary>
    /// An entity can implement this interface if <see cref="DeletionTime"/> of this entity must be stored.
    /// <see cref="DeletionTime"/> is automatically set when deleting <see cref="Entity"/>.
    /// </summary>
    public interface IHasDeletionTime : ISoftDelete
    {
        /// <summary>
        /// Deletion time of this entity.
        /// </summary>
        DateTime? DeletedOn { get; set; }
    }
    /// <summary>
    /// This interface is implemented by entities which wanted to store deletion information (who and when deleted).
    /// </summary>
    public interface IDeletionAudited : IHasDeletionTime
    {
        /// <summary>
        /// Which user deleted this entity?
        /// </summary>
        string DeletedUser { get; set; }
    }

    public interface IDeletedAudited : IDeletionAudited
    {
       
    }
    

}
