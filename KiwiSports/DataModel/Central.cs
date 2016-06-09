using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwiSports.DataModel
{
    public class Central
    {
        private static string errorMessage { get; set; }
        public static string getErrorMessage()
        {
            return "Well, this is embarrassing, We happened to encounter a minor error while we were working. Apologies!";
        }
    }
}
