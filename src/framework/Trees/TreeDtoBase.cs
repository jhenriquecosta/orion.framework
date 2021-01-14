using System.ComponentModel.DataAnnotations;
using Orion.Framework.Applications.Dtos;

namespace Orion.Framework.Infrastructurelications.Trees
{

    public abstract class TreeDtoBase : DtoBase, ITreeNode {
      
        public string ParentId { get; set; }
     
        [Display( Name = "" )]
        public string ParentName { get; set; }
      
        public string Path { get; set; }
    
        public int? Level { get; set; }
      
        [Display( Name = "" )]
        public bool? Enabled { get; set; } = true;
     
        [Display( Name = "" )]
        public int? SortId { get; set; }
      
        [Display( Name = "" )]
        public bool? Expanded { get; set; }
    }
}
