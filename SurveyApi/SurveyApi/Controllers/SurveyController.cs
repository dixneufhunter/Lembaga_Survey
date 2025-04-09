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

    public class SurveyController : ControllerBase
    {

        private readonly ApplicationDbContext_Survey _dbContext;

        public SurveyController(ApplicationDbContext_Survey dbContext) {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lembaga_survey>>> GetSurveys()
        {

            //var dataSurvey = await _dbContext.lembaga_survey.ToListAsync();

            // Just IQueryable shortucts
            var kabupatenList = _dbContext.adm_wil_kabupaten;

            var dataSurvey = await _dbContext.lembaga_survey
                .Select(x => new Lembaga_survey
                {
                    ID_LEMBAGA = x.ID_LEMBAGA,
                    NAMA_LEMBAGA = x.NAMA_LEMBAGA,
                    ALAMAT = x.ALAMAT,
                    NO_TELEPON = x.NO_TELEPON,
                    EMAIL = x.EMAIL,
                    KETERANGAN = x.KETERANGAN,
                    THN_PENELITIAN = x.THN_PENELITIAN,
                    FLAG = x.FLAG,
                    TGL_SURVEY = x.TGL_SURVEY,
                    KODE_KABUPATEN = x.KODE_KABUPATEN,
                    NAMA_KABUPATEN = kabupatenList.Where(y => y.KODE_KABUPATEN == x.KODE_KABUPATEN).Select(y => y.NAMA_KABUPATEN).FirstOrDefault()
                    

                }).ToListAsync();

            return dataSurvey;

        }


        [HttpPost]
        public async Task<IActionResult> CreateSurvey([FromBody] Lembaga_survey newSurvey) {
            if (newSurvey == null) {
                return BadRequest("Data tidak valid");
            }

            try
            {
                _dbContext.lembaga_survey.Add(newSurvey);
                await _dbContext.SaveChangesAsync();
                return Ok(newSurvey);
            }
            catch (DbUpdateException ex) {
                return BadRequest("Gagal menyimpan data : " + ex.Message);
            }
        }

        private bool SurveyExists(int id) {
            return _dbContext.lembaga_survey.Any(survey => survey.ID_LEMBAGA == id);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditSurveyData(int id, Lembaga_survey surveyDataEdit) {
            if (id != surveyDataEdit.ID_LEMBAGA) {
                return BadRequest();
            }

            _dbContext.Entry(surveyDataEdit).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException) {
                if (!SurveyExists(id))
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
        public async Task<IActionResult> DeleteSurvey(int id) {
            var survey_ = await _dbContext.lembaga_survey.FindAsync(id);

            if (survey_ == null) {
                return NotFound();
            }

            _dbContext.lembaga_survey.Remove(survey_);

            await _dbContext.SaveChangesAsync();

            return Ok("Data survey berhasil dihapus");
        }

        [HttpGet("id")]
        public async Task<ActionResult<Lembaga_survey>> GetDetailSurvey(int id) {
            //var survey_ = await _dbContext.lembaga_survey.FindAsync(id);

            // Just IQueryable shortucts
            var kabupatenList = _dbContext.adm_wil_kabupaten;

            var survey_ = await _dbContext.lembaga_survey
                .Select(x => new Lembaga_survey
                {
                    ID_LEMBAGA = x.ID_LEMBAGA,
                    NAMA_LEMBAGA = x.NAMA_LEMBAGA,
                    ALAMAT = x.ALAMAT,
                    NO_TELEPON = x.NO_TELEPON,
                    EMAIL = x.EMAIL,
                    KETERANGAN = x.KETERANGAN,
                    THN_PENELITIAN = x.THN_PENELITIAN,
                    FLAG = x.FLAG,
                    TGL_SURVEY = x.TGL_SURVEY,
                    KODE_KABUPATEN = x.KODE_KABUPATEN,
                    NAMA_KABUPATEN = kabupatenList.Where(y => y.KODE_KABUPATEN == x.KODE_KABUPATEN).Select(y => y.NAMA_KABUPATEN).FirstOrDefault()

                }).Where(z => z.ID_LEMBAGA == id).ToListAsync();

            if (survey_ == null)
            {

                return NotFound("Data survey tidak ditemukan");
            }

            var dt_survey = survey_.FirstOrDefault();

            return dt_survey;
        }


        
      

     
    }
}
