using System;

namespace Orion.Framework.Domains.Auditing {
  
    public interface ICreatorAudited : ICreatorAudited<Guid?> {
    }

  
    public interface ICreatorAudited<TKey> : ICreationAudited<TKey>, ICreator {
    }
}
