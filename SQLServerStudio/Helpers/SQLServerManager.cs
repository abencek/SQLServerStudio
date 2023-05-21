using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SQLServerStudio
{
    /// <summary>
    /// Helper class that runs SQL scripts using SQL server
    /// </summary>
    public class SQLServerManager : IAsyncDisposable, IDisposable
    {
        #region Private Members

        /// <summary>
        /// Connection to SQL server instance
        /// </summary>
        private readonly SqlConnection _connection;

        #endregion


        #region Public Properties

        /// <summary>
        /// Exception messages returned by SQL server during scrip execution
        /// </summary>
        public string ExecutionErrors { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="serverName">Name of SQL server instance</param>
        public SQLServerManager(string serverName)
        {
            //Initialize connection to the database
            _connection = new SqlConnection()
            {
                ConnectionString = $"Server={serverName};Integrated Security=SSPI;Connection Timeout=5;Encrypt=False",
                FireInfoMessageEventOnUserErrors = true
            };
            //Subscribe InfoMessage event to get execution errors from SQL Server
            _connection.InfoMessage += new SqlInfoMessageEventHandler(Connection_InfoMessageHandler);
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Executes SQL script
        /// </summary>
        /// <param name="sql">SQL script</param>
        /// <returns>Instance of <see cref="Task"/></returns>
        public async Task<List<DataTable>> ExecuteSQLAsync(string sql)
        {

            //Open connection to the server
            if (_connection.State != ConnectionState.Open)
            {
                try
                {
                    await _connection.OpenAsync();
                }
                catch
                {
                    throw new DataException("SQL server error! Server was not found or was not accessible!");
                }
            }              

            //Clear old error messages
            ExecutionErrors = String.Empty;

            //Create SQL command (script is executed by sp_executesql procedure)
            using SqlCommand cmd = new("sp_executesql", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@stmt", SqlDbType.NVarChar);
            cmd.Parameters["@stmt"].Value = sql;

            //Time delay before execution
            await Task.Delay(2000);

            //Run SQL command
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            //Check for results
            if (reader.HasRows == true)
            {
                //Read results
                return await GetReaderResults(reader);
            }
            else
            {
                //No results (for example in case of action query)
                return null;
            }
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Converts returned data into data tables
        /// </summary>
        /// <param name="reader">Reader instance with data from SQL server</param>
        /// <returns>Instance of <see cref="Task"/></returns>
        private async Task<List<DataTable>> GetReaderResults(SqlDataReader reader)
        {
            List<DataTable> results = new();

            //Loop all tables and get data
            do
            {
                DataTable tableData = new();

                //Get column names and data types from schema table
                DataTable tableSchema = await reader.GetSchemaTableAsync();

                foreach (DataRow schemaRow in tableSchema.Rows)
                {
                    string columnName = schemaRow.ItemArray
                        .GetValue(tableSchema.Columns["ColumnName"].Ordinal).ToString();
                    string columnType = schemaRow.ItemArray
                        .GetValue(tableSchema.Columns["DataType"].Ordinal).ToString();

                    //If table has more columns with the same name, append index number to the column name 
                    if (tableData.Columns.Contains(columnName))
                    {
                        int columnNum = 1;

                        while (tableData.Columns.Contains(columnName + columnNum.ToString()))
                        {
                            columnNum++;
                        }
                        columnName += columnNum.ToString();
                    }

                    //Change datatype bool to number to prevent display column as checkbox by UI
                    if (columnType == "System.Boolean")
                    {
                        tableData.Columns.Add(columnName, Type.GetType("System.Byte"));
                    }
                    //Else add data type returned by schema
                    else
                    {
                        tableData.Columns.Add(columnName, Type.GetType(columnType));
                    }
                }

                //Load data to newly created table
                tableData.Load(reader);

                //Add table to collection of tables
                results.Add(tableData);

            } while (!reader.IsClosed);

            return results;
        }

        #endregion


        #region Event Handlers

        /// <summary>
        /// Monitor errors from SQL server
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">Event data</param>
        private void Connection_InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {
            //Get error number
            int errNum = e.Errors[0].Number;
            //Do not handle drop table(3701) or eliminate null(8153) exceptions
            if (errNum != 3701 && errNum != 8153) 
            {
                //Append new error message
                ExecutionErrors += e.Errors[0].Message + "\n";
            }
        }

        #endregion


        #region Resources Cleanup

        /// <summary>
        /// Close connection to server asynchronously
        /// </summary>
        /// <returns>Instance of <see cref="ValueTask"/></returns>
        public async ValueTask DisposeAsync()
        {
            if (_connection is not null)
                await _connection.CloseAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Close connection to server synchronously
        /// </summary>
        public void Dispose()
        {
            if (_connection is not null)
                _connection.Close();
        }

        #endregion

    }
}
