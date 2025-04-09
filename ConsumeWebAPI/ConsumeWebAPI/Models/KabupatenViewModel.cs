using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;

namespace ConsumeWebAPI.Models
{
    public class KabupatenViewModel
    {
        [DisplayName("Id Kabupaten")]
        public int ID_KAB { get; set; }
        [Required]

        [DisplayName("Kode Kabupaten")]
        public string KODE_KABUPATEN { get; set; }

        [DisplayName("Nama Kabupaten")]
        public string NAMA_KABUPATEN { get; set; }

        public List<KabupatenViewModel> KabList { get; set; }


    }
}
