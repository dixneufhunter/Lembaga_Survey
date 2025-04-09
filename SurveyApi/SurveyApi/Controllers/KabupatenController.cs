using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SurveyApi.Models;
using Newtonsoft.Json;
using SurveyApi.DTO.Response;
using Microsoft.EntityFrameworkCore;

namespace SurveyApi.Controllers
{


    //[Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class KabupatenController : ControllerBase
    {

        private readonly ApplicationDbContext_Survey _dbContext;

        public KabupatenController(ApplicationDbContext_Survey dbContext) {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adm_Wil_Kabupaten>>> GetKabupaten()
        {
           
            var data = await _dbContext.adm_wil_kabupaten.ToListAsync();

            return data;

        }

        [HttpGet]
        public ActionResult<IEnumerable<Adm_Wil_Kabupaten>> GetKabupaten_DDL()
        {
            
            IList<Adm_Wil_Kabupaten> kabobj = _dbContext.adm_wil_kabupaten.Select(x => new Adm_Wil_Kabupaten()
            {
                KODE_KABUPATEN = x.KODE_KABUPATEN,
                NAMA_KABUPATEN = x.NAMA_KABUPATEN
            }).ToList<Adm_Wil_Kabupaten>();

            return Ok(kabobj);

        }

        [HttpPost]
        public async Task<IActionResult> CreateKabupaten([FromBody] Adm_Wil_Kabupaten newKabupaten) {
            if (newKabupaten == null) {
                return BadRequest("Data tidak valid");
            }

            try
            {
                if (!KabupatenExists(newKabupaten.KODE_KABUPATEN))
                {
                    return Content("Data Kode Kabupaten:" + newKabupaten.KODE_KABUPATEN + "Sudah Ada");
                }

                _dbContext.adm_wil_kabupaten.Add(newKabupaten);
                await _dbContext.SaveChangesAsync();
                return Ok(newKabupaten);
            }
            catch (DbUpdateException ex) {
                return BadRequest("Gagal menyimpan data : " + ex.Message);
            }
        }

        private bool KabupatenExists(string id) {
            return _dbContext.adm_wil_kabupaten.Any(kabupaten => kabupaten.KODE_KABUPATEN == id);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditKabupatenData(string id, Adm_Wil_Kabupaten kabupatenDataEdit) {
            if (id != kabupatenDataEdit.KODE_KABUPATEN) {
                return BadRequest();
            }

            _dbContext.Entry(kabupatenDataEdit).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException) {
                if (!KabupatenExists(id))
                {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKabupaten(int id) {
            var kabupaten_ = await _dbContext.adm_wil_kabupaten.FindAsync(id);

            if (kabupaten_ == null) {
                return NotFound();
            }

            _dbContext.adm_wil_kabupaten.Remove(kabupaten_);

            await _dbContext.SaveChangesAsync();

            return Ok("Data kabupaten berhasil dihapus");
        }

        [HttpGet("id")]
        public async Task<ActionResult<Adm_Wil_Kabupaten>> GetDetailKabupaten(int id) {
            var kabupaten_ = await _dbContext.adm_wil_kabupaten.FindAsync(id);

            if (kabupaten_ == null) {

                return NotFound("Data kabupaten tidak ditemukan");
            }

            return kabupaten_;
        }

       

        [HttpGet("kode")]
        public ActionResult<IEnumerable<Adm_Wil_Kabupaten>> GetDetailKabupatenKode(string kode)
        {

           
            var kabupaten_ = _dbContext.adm_wil_kabupaten.Where(x => x.KODE_KABUPATEN == kode).ToList();

            if (kabupaten_ == null)
            {

                return NotFound("Data kabupaten tidak ditemukan");
            }

            return kabupaten_;

        }






    }
}
