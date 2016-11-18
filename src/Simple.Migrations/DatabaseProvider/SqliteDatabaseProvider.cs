﻿using System;
using System.Data.Common;

namespace SimpleMigrations.DatabaseProvider
{
    /// <summary>
    /// Class which can read from / write to a version table in an SQLite database
    /// </summary>
    public class SqliteDatabaseProvider : DatabaseProviderBase
    {
        public override void AcquireDatabaseLock(DbConnection connection)
        {
        }

        public override void ReleaseDatabaseLock(DbConnection connection)
        {
        }

        /// <summary>
        /// Returns SQL to create the version table
        /// </summary>
        /// <returns>SQL to create the version table</returns>
        public override string GetCreateVersionTableSql()
        {
            return $@"CREATE TABLE IF NOT EXISTS {this.TableName} (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Version INTEGER NOT NULL,
                    AppliedOn DATETIME NOT NULL,
                    Description TEXT NOT NULL
                )";
        }

        /// <summary>
        /// Returns SQL to fetch the current version from the version table
        /// </summary>
        /// <returns>SQL to fetch the current version from the version table</returns>
        public override string GetCurrentVersionSql()
        {
            return $@"SELECT Version FROM {this.TableName} ORDER BY Id DESC LIMIT 1";
        }

        /// <summary>
        /// Returns SQL to update the current version in the version table
        /// </summary>
        /// <returns>SQL to update the current version in the version table</returns>
        public override string GetSetVersionSql()
        {
            return $@"INSERT INTO {this.TableName} (Version, AppliedOn, Description) VALUES (@Version, datetime('now', 'localtime'), @Description)";
        }
    }
}
