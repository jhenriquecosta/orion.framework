using System;

namespace Orion.Framework.DataLayer.Queries.Trees {
   
    public interface ITreeQueryParameter<TParentId> : IQueryParameter {
      
        TParentId ParentId { get; set; }
       
        int? Level { get; set; }
     
        string Path { get; set; }
      
        bool? Enabled { get; set; }
       
        bool IsSearch();
    }

   
    public interface ITreeQueryParameter : ITreeQueryParameter<Guid?> {
    }
}
