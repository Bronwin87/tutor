﻿using DataSetExplorer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSetExplorer.DataSetBuilder.Model.Repository
{
    public interface IDataSetInstanceRepository
    {
        DataSetInstance GetDataSetInstance(int id);
        IEnumerable<DataSetInstance> GetInstancesAnnotatedByAnnotator(int projectId, int? annotatorId);
        void Update(DataSetInstance instance);
    }
}
