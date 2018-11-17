using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
    public static class HelperExtension
    {
        public static DateTime getCombinedDateTime(DateTime? Date, DateTime? Time)
        {
            DateTime combined = DateTime.Parse(Date.Value.ToShortDateString() + " " + Time.Value.ToShortTimeString());
            return combined;
        }
    }
}
