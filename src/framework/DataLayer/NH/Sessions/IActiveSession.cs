using Zeus.DataLayer.Sql.Builders;
using Zeus.Dependency;

namespace Zeus.NHibernate
{


    public interface IActiveSession
    {
    }

    public interface IActiveSession<T> : IActiveSession
    { 
        T Current { get; set; }
    }




}