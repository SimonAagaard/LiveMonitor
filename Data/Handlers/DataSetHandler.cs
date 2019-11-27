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

        public async Task CreateDataSet(DataSet dataSet)
        {
            await _dataSetRepo.Add(dataSet);
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

        public async Task<DataSet> GetDataSetByIntegrationSettingAndTimestamp(Guid integrationSettingId, DateTime dateTime)
        {
            return await _dataSetRepo.Get(x => x.IntegrationSettingId == integrationSettingId && x.XValue == dateTime);
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