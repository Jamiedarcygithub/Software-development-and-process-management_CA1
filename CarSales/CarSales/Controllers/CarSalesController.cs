using CarSales.Models;
using CarSales.ViewModels;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarSales.Controllers
{
    [Authorize]
    public class CarSalesController : Controller
    {
        // GET: CarSales
        public ActionResult Index()
        {
            return View();
        }


        
            BlobOperations blobOperations;
            TableOperations tableOperations;
            public CarSalesController()
            {
                blobOperations = new BlobOperations();
                tableOperations = new TableOperations();
            }
            // GET: ProfileManager


            public ActionResult AddCar()
            {
                var Car = new CarEntity();
                Car.ProfileId = new Random().Next(); //Generate the Profile Id Randomly
                Car.Email = User.Identity.Name; // The Login Email
                ViewBag.BodyType = new SelectList(new List<string>()
            {
               "Coupe","SUV","Saloon","HarchBack","4x4", "Van"
            });
                ViewBag.GearBox = new SelectList(new List<string>()
            {
               "Manual","Automatic"
            });

            ViewBag.Fuel = new SelectList(new List<string>()
            {
                "Desial", "Petrol"
            });
                return View(Car);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> AddCar(
                   CarEntity obj,
              HttpPostedFileBase careImageFile
                )
            {

                CloudBlockBlob carImagesBlob = null;
                #region Upload File In Blob Storage
                //Step 1: Uploaded File in BLob Storage
                if (careImageFile != null && careImageFile.ContentLength != 0)
                {
                    carImagesBlob = await blobOperations.UploadBlob(careImageFile);
                    obj.ImagePath = carImagesBlob.Uri.ToString();
                }
                //Ends Here 
                #endregion

                #region Save Information in Table Storage
                //Step 2: Save the Information in the Table Storage

                //Get the Original File Size
                obj.Email = User.Identity.Name; // The Login Email
                obj.RowKey = obj.ProfileId.ToString();
                obj.PartitionKey = obj.Email;
                //Save the File in the Table
                tableOperations.CreateEntity(obj);
                //Ends Here 
                #endregion



                return RedirectToAction("Index");
            }
        }


    }
