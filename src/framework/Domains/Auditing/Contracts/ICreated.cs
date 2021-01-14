using System;

namespace Orion.Framework.Domains
{

    /// <summary>
    /// An entity can implement this interface if <see cref="CreationTime"/> of this entity must be stored.
    /// <see cref="CreationTime"/> is automatically set when saving <see cref="Entity"/> to database.
    /// </summary>
    public interface IHasCreationTime
    {
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        DateTime? CreatedOn { get; set; }
    }
    /// <summary>
    ///     This interface is implemented by entities that is wanted to store creation information (who and when created).
    ///     Creation time and creator user are automatically set when saving <see cref="Entity" /> to database.
    /// </summary>
    public interface ICreationAudited : IHasCreationTime
    {
        /// <summary>
        ///     Id of the creator user of this entity.
        /// </summary>
        string CreatedUser { get; set; }
    }

    public interface ICreatedAudited : ICreationAudited
    {   
         
    }
    
}
