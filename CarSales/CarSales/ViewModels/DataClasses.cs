using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.ViewModels
{
    public class CarEntity : TableEntity
    {
        public CarEntity()
        {

        }

        public CarEntity(int profid, string email)
        {
            this.RowKey = profid.ToString();
            this.PartitionKey = email;
        }


        public int ProfileId { get; set; }
        [Required(ErrorMessage = "FullName is Must")]
        public string FullName { get; set; }

        //Contatct Details
        [Required(ErrorMessage = "Please add ContactNo")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Please add Email")]
        public string Email { get; set; }

        //Car details
        [Required(ErrorMessage = "Please add Email")]
        public string Make { get; set; }
        [Required(ErrorMessage = "Please add Make")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Please add Body Type")]
        public string BodyType { get; set; }

        [Required(ErrorMessage = "Please add body Year")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Please add Year")]
        public string Engine { get; set; }
        [Required(ErrorMessage = "Please add Engine size")]
        public string GearBox { get; set; }
        [Required(ErrorMessage = "Please add Gearbox")]
        public string Colour { get; set; }
        [Required(ErrorMessage = "Please add Fuel type")]
        public string Fuel { get; set; }
        [Required(ErrorMessage = "Please add Milage")]
        public int Milage { get; set; }
        [Required(ErrorMessage = "Please add Num Doors")]
        public int Doors { get; set; }
        [Required(ErrorMessage = "Please add an image")]
        public string ImagePath { get; set; }


    }





    //public class BlobInfo
    //{

    //    public int ProfileId { get; set; }
    //    public Uri uriBlob { get; set; }

    //    public string Profession { get; set; }

    //    public string BLOBName
    //    {
    //        get
    //        {
    //            return uriBlob.Segments[uriBlob.Segments.Length - 1];
    //        }
    //    }
    //    public string BlobNameWithNoExtension
    //    {
    //        get
    //        {
    //            return Path.GetFileNameWithoutExtension(BLOBName);
    //        }
    //    }

    //}
}
