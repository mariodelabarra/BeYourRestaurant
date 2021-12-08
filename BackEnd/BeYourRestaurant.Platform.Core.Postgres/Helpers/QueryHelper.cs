using BeYourRestaurant.Platform.Core.Domain;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BeYourRestaurant.Platform.Core.Postgres.Helpers
{
    public static class QueryHelper<T> where T : BaseEntity
    {
        private static IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();

        private static IEnumerable<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name);
        }

        /// <summary>
        /// Generates the Insert query for all the properties of the specified entity
        /// </summary>
        /// <param name="tableName">Name of the table entity</param>
        /// <returns>Insert query for all properties of the entity</returns>
        public static string GenerateInsertQuery(string tableName)
        {
            var insertQuery = new StringBuilder($"INSERT INTO {tableName} ");

            insertQuery.Append("(");

            var properties = GenerateListOfProperties(GetProperties)
                .Where(property => property != "Id").ToList();

            properties.ForEach(prop => 
            { 
                insertQuery.Append($@"""{prop}"","); 
            });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");

            return insertQuery.ToString();
        }

        /// <summary>
        /// Generates the Update query for all the properties of the specified entity
        /// </summary>
        /// <param name="tableName">Name of the table entity</param>
        /// <returns>Update query for all properties of the entity</returns>
        public static string GenerateUpdateQuery(string tableName)
        {
            var updateQuery = new StringBuilder($"UPDATE {tableName} SET ");
            var properties = GenerateListOfProperties(GetProperties).ToList();

            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
            updateQuery.Append(" WHERE Id=@Id");

            return updateQuery.ToString();
        }
    }
}
