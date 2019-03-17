using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LuminiHire.Models
{
    public class UniItem
    {
        public long UNITID { get; set; }
        public string INSTNM { get; set; }
        public double PCT_ASIAN { get; set; }
        public double PCT_BLACK { get; set; }
        public double PCT_WHITE { get; set; }   
        public double PCT_HISPANIC { get; set; }
        public double PCT_BORN_US { get; set; }
        public double POVERTY_RATE { get; set; }
    }
}
