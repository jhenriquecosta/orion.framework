using Orion.Framework.Applications.Dtos;

namespace Orion.Framework.Infrastructurelications.Trees
{

    public interface ITreeNode : IKey {
     
        string ParentId { get; set; }
     
       
        string Path { get; set; }
      
        int? Level { get; set; }
   
        bool? Expanded { get; set; }
    }
}
