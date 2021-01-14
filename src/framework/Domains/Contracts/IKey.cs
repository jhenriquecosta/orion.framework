namespace Orion.Framework.Domains 
{
   
    public interface IKey<out TKey>
    {
    
        TKey Id { get; }
    }
}
