using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byteopia.Helpers
{
   public class Log
    {
       public static void Write(object data, Object caller = null)
       {
#if DEBUG
           Debug.WriteLine("{0} [{1}] : {2}", ( caller != null ) ? caller.GetType().FullName : "Debug", 
               DateTime.Now.ToString("mm/dd/yyyy"), data);
#endif
       }
    }
}
