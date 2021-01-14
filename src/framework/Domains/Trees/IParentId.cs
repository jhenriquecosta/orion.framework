namespace Orion.Framework.Domains.Trees {
  
    public interface IParentId<TParentId> {
       
        TParentId ParentId { get; set; }
    }
}