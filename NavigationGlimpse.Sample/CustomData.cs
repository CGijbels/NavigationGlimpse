using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NavigationGlimpse.Sample
{
    [TypeConverter(typeof(CustomDataTypeConverter))]
    public class CustomData
    {
        public Hashtable data = new Hashtable();
    }
}