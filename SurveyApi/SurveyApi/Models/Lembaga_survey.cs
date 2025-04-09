using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace SurveyApi.Models
{
    public class Lembaga_survey
    {

        //[JsonIgnore]
        [SwaggerIgnore]
        public int ID_LEMBAGA { get; set; }

        public string NAMA_LEMBAGA { get; set; }

        public string ALAMAT { get; set; }

        public string NO_TELEPON { get; set; }

        public string EMAIL { get; set; }

        public string KETERANGAN { get; set; }

        public int THN_PENELITIAN { get; set; }

        public string FLAG { get; set; }

        public DateTime TGL_SURVEY { get; set; }

        public string KODE_KABUPATEN { get; set; }

        [NotMapped]
        public string? NAMA_KABUPATEN { get; set; }

    }

    


}
