using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orion.Framework.DataLayer.NH.QueryObjects
{ 
    public class Page<TResult> where TResult : class
    {
        #region "PUBLIC MEMBERS"
        public readonly short Number;

        public readonly short Size;

        public readonly Int32 TotalNumberItems;

        public readonly IEnumerable<TResult> Data;
        #endregion

        #region "CONSTRUCTORS"
        internal Page(short pNumber, short pSize, Int32 pTotalNumberItems, IEnumerable<TResult> pData)
        {
            Number = pNumber;
            Size = pSize;
            TotalNumberItems = pTotalNumberItems;
            Data = pData;
        }
        #endregion

    }
}