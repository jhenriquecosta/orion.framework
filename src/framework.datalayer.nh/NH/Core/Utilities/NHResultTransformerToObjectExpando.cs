using NHibernate.Transform;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Orion.Framework.DataLayer.NH.Helpers
{
    public static class NHResultTransformerToObjectExpando
    {
        public static readonly IResultTransformer ExpandoObject;

        static NHResultTransformerToObjectExpando()
        {
            ExpandoObject = new ExpandoObjectResultSetTransformer();
        }
    }
    public class ExpandoObjectResultSetTransformer : IResultTransformer
    {
        public IList TransformList(IList collection)
        {
            return collection;
        }
        public object TransformTuple(object[] tuple, string[] aliases)
        {
            var expando = new ExpandoObject();
            var dictionary = (IDictionary<string, object>)expando;
            for (int i = 0; i < tuple.Length; i++)
            {
                string alias = aliases[i];
                if (alias != null)
                {
                    dictionary[alias] = tuple[i];
                }
            }
            return expando;
        }
    }
}
