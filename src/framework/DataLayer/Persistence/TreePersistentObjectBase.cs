using System;
using Orion.Framework.Domains.Trees;

namespace Orion.Framework.DataLayer.Persistence {
    /// <summary>
    /// 
    /// </summary>
    public abstract class TreePersistentObjectBase : TreePersistentObjectBase<Guid, Guid?> {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TParentId"></typeparam>
    public abstract class TreePersistentObjectBase<TKey, TParentId> : PersistentObjectBase<TKey>, IParentId<TParentId>, IPath, IEnabled, ISortId {
        /// <summary>
        /// 父标识
        /// </summary>
        public TParentId ParentId { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public virtual string Path { get; set; }

        /// <summary>
        /// 级数
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int? SortId { get; set; }
    }
}
