using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpressPrintingSystem.Model
{
    public class document
    {
        [Key]
        public int fileId { get; set; }

        [Required]
        public string name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }

        public long size { get; set; }

        public string type { get; set; }


    }
}