using CarSales.ViewModels;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Configuration;

namespace CarSales.Models
{
    /// <summary>
    /// Interface Containing Operations for
    /// 1. Create Entity in Table => CreateEntity
    /// 2. Retrive Entities Based upon the Partition => GetEntities
    /// 3. Get Single Entity based upon partition Key and Row Key => GetEntity
    /// </summary>
    public interface ITableOperations
    {
        void CreateEntity(CarEntity entity);
        List<CarEntity> GetEntities(string filter);
        CarEntity GetEntity(string partitionKey, string rowKey);

    }

    public class TableOperations : ITableOperations
    {
        //Represent the Cloud Storage Account, this will be instantiated 
        //based on the appsettings
        CloudStorageAccount storageAccount;
        //The Table Service Client object used to 
        //perform operations on the Table
        CloudTableClient tableClient;

        /// <summary>
        /// COnstructor to Create Storage Account and the Table
        /// </summary>
        public TableOperations()
        {
            //Get the Storage Account from the conenction string
            storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["webjobstorage"]);
            //Create a Table Client Object
            tableClient = storageAccount.CreateCloudTableClient();

            //Create Table if it does not exist
            CloudTable table = tableClient.GetTableReference("CarEntityTable");
            table.CreateIfNotExists();
        }

        /// <summary>
        /// Method to Create Entity
        /// </summary>
        /// <param name="entity"></param>
        public void CreateEntity(CarEntity entity)
        {

            CloudTable table = tableClient.GetTableReference("CarEntityTable");
            //Create a TableOperation object used to insert Entity into Table
            TableOperation insertOperation = TableOperation.Insert(entity);
            //Execute an Insert Operation
            table.Execute(insertOperation);
        }
        /// <summary>
        /// Method to retrieve entities based on the PartitionKey
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<CarEntity> GetEntities(string filter)
        {
            List<CarEntity> Cars = new List<CarEntity>();
            CloudTable table = tableClient.GetTableReference("CarEntityTable");

            TableQuery<CarEntity> query = new TableQuery<CarEntity>()
            .Where(TableQuery.GenerateFilterCondition("Email", QueryComparisons.Equal, filter));


            foreach (var item in table.ExecuteQuery(query))
            {
                Cars.Add(new CarEntity()
                {
                    ProfileId = item.ProfileId,
                    FullName = item.FullName,
                    Model = item.Model,
                    Make = item.Make,
                    GearBox = item.GearBox,
                    ContactNo = item.ContactNo,
                    Email = item.Email,
                    Year = item.Year,
                    ImagePath = item.ImagePath,
                    BodyType = item.BodyType
                });
            }

            return Cars;
        }

        /// <summary>
        /// Method to get specific entity based on the Row Key and the Partition key
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        public CarEntity GetEntity(string partitionKey, string rowKey)
        {
            CarEntity entity = null;

            CloudTable table = tableClient.GetTableReference("CarEntityTable");

            TableOperation tableOperation = TableOperation.Retrieve<CarEntity>(partitionKey, rowKey);
            entity = table.Execute(tableOperation).Result as CarEntity;

            return entity;
        }

    }
}