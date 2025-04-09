using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;
using ConsumeWebAPI.Models;
using Newtonsoft.Json;

namespace ConsumeWebAPI.Controllers
{
    public class SurveyController : Controller
    {
        /*Uri baseAddress = new Uri("https://localhost:5001/api");*/
        Uri baseAddress = new Uri("http://localhost:5001/api");
        private readonly HttpClient _client;

        public SurveyController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Lembaga_SurveyViewModel> surveyList = new List<Lembaga_SurveyViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Survey/GetSurveys").Result;

            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                surveyList = JsonConvert.DeserializeObject<List<Lembaga_SurveyViewModel>>(data);
                
            }

            return View(surveyList);
        }

        [HttpGet]
        public IActionResult Create()
        {
           
            List<KabupatenViewModel> KabupatenList = new List<KabupatenViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Kabupaten/GetKabupaten_DDL").Result;

            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                KabupatenList = JsonConvert.DeserializeObject<List<KabupatenViewModel>>(data);
            }
            ViewData["KabupatenList"] = KabupatenList;

            return View();
        }


        [HttpPost]
        public IActionResult Create(Lembaga_SurveyViewModel model)
        {

            try
            {
                if (model.Status == true)
                {
                    model.FLAG = "Y";
                }
                else
                {
                    model.FLAG = "N";
                }
                
                string data = JsonConvert.SerializeObject(model);

                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Survey/CreateSurvey", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Survey Created.";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {

                Lembaga_SurveyViewModel survey_ = new Lembaga_SurveyViewModel();
                HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Survey/GetDetailSurvey/id?id=" + id).Result;

                if (respone.IsSuccessStatusCode)
                {
                    string data = respone.Content.ReadAsStringAsync().Result;
                    survey_ = JsonConvert.DeserializeObject<Lembaga_SurveyViewModel>(data);
                    if (survey_.FLAG == "Y")
                    {
                        survey_.Status = true;
                    }
                    else
                    {
                        survey_.Status = false;
                    }
                }

                List<KabupatenViewModel> KabupatenList = new List<KabupatenViewModel>();
                HttpResponseMessage responeKab = _client.GetAsync(_client.BaseAddress + "/Kabupaten/GetKabupaten_DDL").Result;

                if (responeKab.IsSuccessStatusCode)
                {
                    string data = responeKab.Content.ReadAsStringAsync().Result;
                    KabupatenList = JsonConvert.DeserializeObject<List<KabupatenViewModel>>(data);
                }
                ViewData["KabupatenList"] = KabupatenList;


                return View(survey_);
            }
            catch (Exception ex)
            {

                //throw;
                TempData["errorMessage"] = ex.Message;
                return View();
            }

            
        }

        [HttpPost]
        public IActionResult Edit(Lembaga_SurveyViewModel model)
        {

            try
            {
                if (model.Status == true)
                {
                    model.FLAG = "Y";
                }
                else
                {
                    model.FLAG = "N";
                }

                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Survey/EditSurveyData/" + model.ID_LEMBAGA, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Survey details updated.";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Lembaga_SurveyViewModel survey_ = new Lembaga_SurveyViewModel();
                HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Survey/GetDetailSurvey/id?id=" + id).Result;

                if (respone.IsSuccessStatusCode)
                {
                    string data = respone.Content.ReadAsStringAsync().Result;
                    survey_ = JsonConvert.DeserializeObject<Lembaga_SurveyViewModel>(data);

                    if (survey_.FLAG == "Y")
                    {
                        TempData["Status"] = "Aktif";
                    }
                    else
                    {
                        TempData["Status"] = "Tidak Aktif";
                    }
                }

                #region TDK DIPAKAI
                //List<KabupatenViewModel> KabupatenList = new List<KabupatenViewModel>();
                //HttpResponseMessage responeKab = _client.GetAsync(_client.BaseAddress + "/Kabupaten/GetDetailKabupatenKode/kode?kode=" + survey_.KODE_KABUPATEN).Result;

                //if (responeKab.IsSuccessStatusCode)
                //{
                //    string data = responeKab.Content.ReadAsStringAsync().Result;
                //    KabupatenList = JsonConvert.DeserializeObject<List<KabupatenViewModel>>(data);

                //}

                //var dt_Kab = KabupatenList.FirstOrDefault();
                //if (dt_Kab == null)
                //{

                //}

                //if (survey_.KODE_KABUPATEN == dt_Kab.KODE_KABUPATEN)
                //{
                //    TempData["Nm_Kab"] = dt_Kab.NAMA_KABUPATEN;

                //}
                //else
                //{
                //    TempData["Nm_Kab"] = null;
                //}
                #endregion


                return View(survey_);
            }
            catch (Exception ex)
            {

                //throw;
                TempData["errorMessage"] = ex.Message;
                return View();
            }


        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {

            try
            {
            
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Survey/DeleteSurvey/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Survey details deleted.";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                Lembaga_SurveyViewModel survey_ = new Lembaga_SurveyViewModel();
                HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Survey/GetDetailSurvey/id?id=" + id).Result;

                if (respone.IsSuccessStatusCode)
                {
                    string data = respone.Content.ReadAsStringAsync().Result;
                    survey_ = JsonConvert.DeserializeObject<Lembaga_SurveyViewModel>(data);

                    if (survey_.FLAG == "Y")
                    {
                        TempData["Status"] = "Aktif";
                    }
                    else
                    {
                        TempData["Status"] = "Tidak Aktif";
                    }
                }

                #region TIDAK DIPAKAI
                //List<KabupatenViewModel> KabupatenList = new List<KabupatenViewModel>();
                //HttpResponseMessage responeKab = _client.GetAsync(_client.BaseAddress + "/Kabupaten/GetDetailKabupatenKode/kode?kode=" + survey_.KODE_KABUPATEN).Result;

                //if (responeKab.IsSuccessStatusCode)
                //{
                //    string data = responeKab.Content.ReadAsStringAsync().Result;
                //    KabupatenList = JsonConvert.DeserializeObject<List<KabupatenViewModel>>(data);

                //}

                //var dt_Kab = KabupatenList.FirstOrDefault();
                //if (dt_Kab == null)
                //{

                //}

                //if (survey_.KODE_KABUPATEN == dt_Kab.KODE_KABUPATEN)
                //{
                //    TempData["Nm_Kab"] = dt_Kab.NAMA_KABUPATEN; 

                //}
                //else
                //{
                //    TempData["Nm_Kab"] = null;
                //}
                #endregion


                return View(survey_);
            }
            catch (Exception ex)
            {

                //throw;
                TempData["errorMessage"] = ex.Message;
                return View();
            }


        }


    }
}
