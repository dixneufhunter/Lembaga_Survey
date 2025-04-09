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
    public class Adm_Wil_Kabupaten
    {

        //[JsonIgnore]
        [SwaggerIgnore]
        public int ID_KAB { get; set; }

        public string KODE_KABUPATEN { get; set; }

        public string NAMA_KABUPATEN { get; set; }

    }

    
}
