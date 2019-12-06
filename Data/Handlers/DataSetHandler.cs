using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Handlers
{

    public class DataSetHandler
    {
        private readonly Repository<DataSet> _dataSetRepo;

        public DataSetHandler()
        {
            _dataSetRepo = new Repository<DataSet>();
        }

        // Create a single dataset
        public async Task CreateDataSet(DataSet dataSet)
        {
            await _dataSetRepo.Add(dataSet);
        }

        // Add a list of datasets to save transactions to the DB
        public async Task CreateDataSets(List<DataSet> dataSets)
        {
            await _dataSetRepo.AddMany(dataSets);
        }

        // Get all DataSets in the database
        public async Task<List<DataSet>> GetDataSets()
        {
            return await _dataSetRepo.GetAll();
        }

        // Get a single DataSet based on the dataSetId
        public async Task<DataSet> GetDataSet(Guid dataSetId)
        {
            return await _dataSetRepo.Get(dataSetId);
        }

        // Get a specific dataset based on the integrationSettingId as well as the timestamp given from Azure
        public async Task<DataSet> GetDataSetByIntegrationSettingIdAndTimestamp(Guid integrationSettingId, DateTimeOffset dateTime)
        {
            return await _dataSetRepo.Get(x => x.IntegrationSettingId == integrationSettingId && x.XValue == dateTime);
        }

        // Get newest dataset based on their IntegrationSettingId and a specific from-time
        public async Task<DataSet> GetNewestDataSetByIntegrationSettingIdFromDateTime(Guid integrationSettingId, DateTime dateFrom)
        {
            List<DataSet> dataSets = await _dataSetRepo.GetMany(x => x.IntegrationSettingId == integrationSettingId && x.XValue > dateFrom);
            return dataSets.OrderByDescending(x => x.XValue).FirstOrDefault();
        }

        // Get dataset based on their IntegrationSettingId
        public async Task<List<DataSet>> GetDataSetsByIntegrationSettingId(Guid integrationSettingId)
        {
            return await _dataSetRepo.GetMany(x => x.IntegrationSettingId == integrationSettingId);
        }

        public async Task<List<DataSet>> GetDataSetsFromAGivenTimePeriod(Guid integrationSettingId, DateTime fromDate, DateTime toDate)
        {
            return await _dataSetRepo.GetMany(x => x.IntegrationSettingId == integrationSettingId && x.DateCreated >= fromDate && x.DateCreated <= toDate );
        }
        //Gets the newest dataset, based on DateCreated
        public async Task<DataSet> GetNewestDataSetByIntegrationSettingId(Guid integrationSettingId)
        {
            List<DataSet> dataSets = await _dataSetRepo.GetMany(x => x.IntegrationSettingId == integrationSettingId);
            return dataSets.OrderByDescending(x => x.XValue).FirstOrDefault();
        }

        public async Task<List<DataSet>> GetCertainAmountOfDataSets(Guid integrationSettingId, int dataSetAmount)
        {
            List<DataSet> dataSets = await _dataSetRepo.GetMany(x => x.IntegrationSettingId == integrationSettingId);
            return dataSets.OrderBy(x => x.XValue).TakeLast(dataSetAmount).ToList();
        }

        // Update a DataSet object
        public async Task UpdateDataSet(DataSet dataSet)
        {
            await _dataSetRepo.Update(dataSet);
        }

        // Hard delete a dataset based on the DataSetId
        public async Task DeleteDataSet(DataSet dataSet)
        {
            await _dataSetRepo.Delete(dataSet);
        }
    }
}