namespace Orion.Framework.Domains {
  
    public interface ICompareChange<in T> where T : IDomainObject {

        ChangeValueCollection GetChanges( T other );
    }
}
