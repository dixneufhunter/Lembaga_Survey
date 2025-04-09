using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;

namespace ConsumeWebAPI.Models
{
    public class Lembaga_SurveyViewModel
    {
        [DisplayName("Id Lembaga")]
        public int ID_LEMBAGA { get; set; }
        [Required]

        [DisplayName("Nama Lembaga")]
        public string NAMA_LEMBAGA { get; set; }

        [DisplayName("Alamat")]
        public string ALAMAT { get; set; }

        [DisplayName("Nomor Telepon")]
        public string NO_TELEPON { get; set; }

        [DisplayName("Email")]
        public string EMAIL { get; set; }

        [DisplayName("Keterangan")]
        public string KETERANGAN { get; set; }

        [DisplayName("Tahun Penelitian")]
        public int THN_PENELITIAN { get; set; }

        [DisplayName("Flag")]
        public string FLAG { get; set; }
        //public IList<string> SelectedStatus { get; set; }
        //public IList<SelectListItem> AvailableStatus { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DisplayName("Tanggal Survey")]
        public DateTime TGL_SURVEY { get; set; }

        public Boolean Status { get; set; }

        [DisplayName("Nama Kabupaten")]
        public string KODE_KABUPATEN { get; set; }

        [DisplayName("Nama Kabupaten")]
        public string NAMA_KABUPATEN { get; set; }
       


    }
}
