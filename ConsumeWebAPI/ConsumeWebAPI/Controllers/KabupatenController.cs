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
    public class KabupatenController : Controller
    {
        /*Uri baseAddress = new Uri("https://localhost:5001/api");*/
        Uri baseAddress = new Uri("http://localhost:5001/api");
        private readonly HttpClient _client;

        public KabupatenController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<KabupatenViewModel> kabupatenList = new List<KabupatenViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Kabupaten/GetKabupaten").Result;

            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                kabupatenList = JsonConvert.DeserializeObject<List<KabupatenViewModel>>(data);
                
            }
                        
            return View(kabupatenList);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public IActionResult Create(KabupatenViewModel model)
        {

            try
            {
               
                string data = JsonConvert.SerializeObject(model);

                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Kabupaten/CreateKabupaten", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Kabupaten Created.";
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

                KabupatenViewModel kabupaten_ = new KabupatenViewModel();
                HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Kabupaten/GetDetailKabupaten/id?id=" + id).Result;

                if (respone.IsSuccessStatusCode)
                {
                    string data = respone.Content.ReadAsStringAsync().Result;
                    kabupaten_ = JsonConvert.DeserializeObject<KabupatenViewModel>(data);
                   
                }

                return View(kabupaten_);
            }
            catch (Exception ex)
            {

                //throw;
                TempData["errorMessage"] = ex.Message;
                return View();
            }

            
        }

        [HttpPost]
        public IActionResult Edit(KabupatenViewModel model)
        {

            try
            {
                
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Kabupaten/EditKabupatenData/" + model.ID_KAB, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Kabupaten details updated.";
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
                KabupatenViewModel kabupaten_ = new KabupatenViewModel();
                HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Kabupaten/GetDetailKabupaten/id?id=" + id).Result;

                if (respone.IsSuccessStatusCode)
                {
                    string data = respone.Content.ReadAsStringAsync().Result;
                    kabupaten_ = JsonConvert.DeserializeObject<KabupatenViewModel>(data);

                   
                }
                return View(kabupaten_);
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
            
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Kabupaten/DeleteKabupaten/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Kabupaten details deleted.";
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
                KabupatenViewModel kabupaten_ = new KabupatenViewModel();
                HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Kabupaten/GetDetailKabupaten/id?id=" + id).Result;

                if (respone.IsSuccessStatusCode)
                {
                    string data = respone.Content.ReadAsStringAsync().Result;
                    kabupaten_ = JsonConvert.DeserializeObject<KabupatenViewModel>(data);
                                        
                }
                return View(kabupaten_);
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
