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
        private readonly Repository<DataSet> dataSetRepo;

        public DataSetHandler()
        {
            dataSetRepo = new Repository<DataSet>();
        }

        public async Task CreateDataSet(DataSet DataSet)
        {
            await dataSetRepo.Add(DataSet);
        }

        // Get all DataSets in the database
        public async Task<List<DataSet>> GetDataSets()
        {
            return await dataSetRepo.GetAll();
        }

        // Get a single DataSet based on the dataSetId
        public async Task<DataSet> GetDataSet(Guid DataSetId)
        {
            return await dataSetRepo.Get(DataSetId);
        }

        // Get dataset based on their IntegrationSettingId
        public async Task<List<DataSet>> GetDataSetsByIntegrationSettingId(Guid integrationSettingId)
        {
            return await dataSetRepo.GetMany(x => x.IntegrationSettingId == integrationSettingId);
        }

        public async Task<List<DataSet>> GetDataSetsFromAGivenTimePeriod(Guid integrationSettingId, DateTime fromDate, DateTime toDate)
        {
            return await dataSetRepo.GetMany(x => x.IntegrationSettingId == integrationSettingId && x.DateCreated >= fromDate && x.DateCreated <= toDate );
        }

        // Update a DataSet object
        public async Task UpdateDataSet(DataSet dataSet)
        {
            await dataSetRepo.Update(dataSet);
        }

        // Hard delete a dataset based on the DataSetId
        public async Task DeleteDataSet(Guid DataSetId)
        {
            await dataSetRepo.Delete(DataSetId);
        }

    }
}
