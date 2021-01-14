using System;

namespace Orion.Framework.Domains.Auditing {
  
    public interface IModifierAudited : IModifierAudited<int?> {
    }

   
    public interface IModifierAudited<TKey> : IModificationAudited<TKey>, IModifier {
    }
}
