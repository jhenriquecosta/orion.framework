namespace Orion.Framework.Domains.Repositories
{
   
    public interface IPager : IPagerBase {
        
        int GetPageCount();
       
        int GetSkipCount();
       
        string Order { get; set; }
      
        int GetStartNumber();
      
        int GetEndNumber();
    }
}
