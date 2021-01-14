using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Domains.Trees
{
    public interface ITreeEntityRoot
    {

        int? Ancestral { get; set; }
        int Level { get; set; }
        string Path { get; set; }
        void InitPath(string path, int level);
    }
}
