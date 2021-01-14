using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Orion.Framework.Domains.Attributes;

namespace Orion.Framework.Domains
{
    
    public interface IRootEntity : IBaseEntity
    {

    }


    public interface IRootEntity<out TKey> : IBaseEntity<TKey>, IRootEntity
    {
    }


    public interface IRootEntity<in TEntity, out TKey> : IBaseEntity<TEntity, TKey>, IRootEntity<TKey> where TEntity : IBaseEntity
    {
    }

    public abstract class RootEntity : RootEntity<IRootEntity>,IRootEntity
    {

    }
    public abstract class RootEntity<TEntity> : RootEntity<TEntity, int> where TEntity : IRootEntity
    {

    }
 

    public abstract class RootEntity<TEntity,TKey> : BaseEntity<TEntity, TKey>, IRootEntity<TEntity, TKey> where TEntity : IRootEntity
    {  
        //[ModelField(Ignore = true)]
        //public virtual int Version { get; set; }

        [Required(ErrorMessage = "Informe Um Nome")]
        [MaxLength(80, ErrorMessage = "Maximo de 80 caracteres para este campo")]
        [ModelField(AutoFit  = true)]
        public string Nome { get; set; }
        public override string ToString()
        {
            return Nome;
        }
    }


   
   


}
