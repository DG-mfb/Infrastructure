using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.Data.Connection;

namespace ORMMetadataContextProvider.Tests.Mock
{
    internal class ConnectionStringProviderMock : IConnectionStringProvider
    {
        public SqlConnectionStringBuilder GetConnectionString()
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = "DG-MFB\\SQLEXPRESS_2016",
                InitialCatalog = "SpMetadataTest",
                IntegratedSecurity = true
            };
        }
    }
}
