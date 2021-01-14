using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Orion.Framework.Domains.Attributes;

namespace Orion.Framework.Domains
{
    
    public interface ITreeEntity 
    {
         
          int? Ancestral { get; set; }
          int Level { get; set; }
          string Path { get; set; }
          void InitPath(string path, int level);
    }
    public interface ITreeRootEntity : IBaseEntity, IRootEntity, ITreeEntity
    {
    }


    public interface ITreeRootEntity<out TKey> : IBaseEntity<TKey>, IRootEntity,ITreeRootEntity
    {
    }


    public interface ITreeRootEntity<in TEntity, out TKey> : IBaseEntity<TEntity, TKey>, ITreeRootEntity<TKey> where TEntity : IBaseEntity
    {
    }

    public abstract class TreeRootEntity : TreeRootEntity<ITreeRootEntity>, ITreeRootEntity
    {

    }
    public abstract class TreeRootEntity<TEntity> : TreeRootEntity<TEntity, int> where TEntity : ITreeRootEntity
    {

    }
 

    public abstract class TreeRootEntity<TEntity,TKey> : RootEntity<TEntity, TKey>, ITreeRootEntity<TEntity, TKey> where TEntity : ITreeRootEntity
    {
       
        public virtual int? Ancestral { get; set; }

        public virtual int Level { get;  set; }
        public virtual string Path { get; set; }

        public virtual void InitPath(string path, int level)
        {
            Path = path;
            Level = level;
        }

        public override string ToString()
        {
            return Nome;
        }
    }

   
   


}
