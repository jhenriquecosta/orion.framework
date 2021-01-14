namespace Orion.Framework.MultiTenancy.DataLayer.CoreDAO
{
    public interface IDbSessionCleanup
    {
        void CloseDbConnections();
    }
}
