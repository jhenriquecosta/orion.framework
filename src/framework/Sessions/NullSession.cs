namespace Orion.Framework.Sessions
{
    
    public class NullSession : ISession
    {
      
        public bool IsAuthenticated => false;
        public string UserId => string.Empty;
        public int? OrganizationCode => 0;
        public static readonly ISession Instance = new NullSession();
    }
}