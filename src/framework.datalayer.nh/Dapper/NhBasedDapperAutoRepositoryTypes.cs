using Orion.Framework.DataLayer.Dapper.Repositories;

namespace Orion.Framework.DataLayer.Dapper
{
    public class NhBasedDapperAutoRepositoryTypes
    {
        static NhBasedDapperAutoRepositoryTypes()
        {
            Default = new DapperAutoRepositoryTypeAttribute(
                typeof(IDapperRepository<>),
                typeof(IDapperRepository<,>),
                typeof(DapperNhRepositoryBase<,>),
                typeof(DapperNhRepositoryBase<,,>)
            );
        }

        public static DapperAutoRepositoryTypeAttribute Default { get; private set; }
    }
}