using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Orion.Framework.Infrastructurelications.Trees;

namespace Orion.Framework.Applications.Dtos

{
   
    public class TreeDto<TNode> : TreeDto where TNode : TreeDto<TNode> {
       
        public TreeDto() {
            Children = new List<TNode>();
        }

        
        public List<TNode> Children { get; set; }
    }


    public class TreeDto : TreeDtoBase {
       
        public virtual string Text { get; set; }
       
        [Display( Name = "Icon" )]
        public string Icon { get; set; }
       
        public bool? DisableCheckbox { get; set; }
      
        public bool? Selectable { get; set; } = true;
       
        public bool? Checked { get; set; }
       
        public bool? Selected { get; set; }
       
        public bool? Leaf { get; set; }
    }
}
