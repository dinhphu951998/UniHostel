using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniHostel.Models
{
    public class Utils
    {

        public static string getRandomID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }
    }
}