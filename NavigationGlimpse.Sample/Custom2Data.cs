using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NavigationGlimpse.Sample
{
    public class Custom2Data
    {
        public Hashtable data = new Hashtable();

        public Custom2Data()
        {
            data[new object()] = new object(); 
        }
    }
}