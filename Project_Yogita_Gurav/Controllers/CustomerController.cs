using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Yogita_Gurav.Models;
using Newtonsoft.Json;
using System.Text;

namespace Project_Yogita_Gurav.Controllers
{
    public class CustomerController : Controller
    {
        // GET: CustomerController
        public async Task<ActionResult> Index()
        {
            List<Customer> customerData = new List<Customer>();
                
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://getinvoices.azurewebsites.net/api/Customers"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customerData = JsonConvert.DeserializeObject<List<Customer>>(apiResponse);

                }
            }
            return View(customerData);

            
        }

        public async Task<ActionResult> Details(int id)
        {
            Customer customerData = new Customer();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://getinvoices.azurewebsites.net/api/Customer/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customerData = JsonConvert.DeserializeObject<Customer>(apiResponse);

                }
            }
            return View(customerData);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// To create new customer.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                //var id = collection.Select(a => a.Id);
                Customer addcustomer = new Customer();
                using (var httpClient = new HttpClient())
                {
                
                    StringContent content = new StringContent(JsonConvert.SerializeObject(addcustomer), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://getinvoices.azurewebsites.net/api/Customer", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            addcustomer = JsonConvert.DeserializeObject<Customer>(apiResponse);
                        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            ViewBag.Result = apiResponse;
                            return View();
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// To update the selected customer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: CustomerController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
             Customer customerData = new Customer();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://getinvoices.azurewebsites.net/api/Customer/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customerData = JsonConvert.DeserializeObject<Customer>(apiResponse);

                }
            }
            return View(customerData);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, IFormCollection collection)
        {
            try
            {
                Customer inputCustomerData = new Customer();
                using (var httpClient = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(collection["Id"].ToString()), "Id");
                    content.Add(new StringContent(collection["firstname"].ToString()), "firstname");
                    content.Add(new StringContent(collection["lastname"].ToString()), "lastname");
                    content.Add(new StringContent(collection["country_name"].ToString()), "country_name");

                    using (var response = await httpClient.PostAsync("https://getinvoices.azurewebsites.net/api/Customer/" +id , content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        inputCustomerData = JsonConvert.DeserializeObject<Customer>(apiResponse);
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            Customer customerData = new Customer();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://getinvoices.azurewebsites.net/api/Customer/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customerData = JsonConvert.DeserializeObject<Customer>(apiResponse);

                }
            }
            return View(customerData);
        }
        /// <summary>
        /// To delete selected customer.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, IFormCollection collection)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    id = Convert.ToInt32(collection["hidId"]);
                    using (var response = await httpClient.DeleteAsync("https://getinvoices.azurewebsites.net/api/Customer/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
