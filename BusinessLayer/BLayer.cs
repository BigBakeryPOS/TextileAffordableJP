using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DataLayer;
using CommonLayer;
using System.Globalization;

namespace BusinessLayer
{
    public class BLayer
    {
        #region User Defined Objects
        DBAccess dbObj = null;
        #endregion

        #region Constructors
        public BLayer()
        {
            dbObj = new DBAccess();
        }
        #endregion
    }
}
