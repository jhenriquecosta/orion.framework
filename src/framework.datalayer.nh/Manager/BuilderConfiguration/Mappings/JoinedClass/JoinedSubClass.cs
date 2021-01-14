using System;
using System.Collections.Generic;
using System.Text;
using Zeus.Domains;
using Zeus.Validations;

namespace Zeus.NHibernate.Mappings.Components
{
    public class JoinedSubClass<T> where T : class
    {
        public void Init()
        {

        }

        public bool IsTransient()
        {
            throw new NotImplementedException();
        }

        public ValidationResultCollection Validate()
        {
            throw new NotImplementedException();
        }
    }
}
