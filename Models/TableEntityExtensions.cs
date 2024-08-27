using Azure.Data.Tables;
using System.Collections.Generic;

namespace testCLVD.Models
{
    public static class TableEntityExtensions
    {
        // Extension method to get a property value from TableEntity
        public static T GetPropertyValue<T>(this TableEntity entity, string propertyName)
        {
            if (entity.TryGetValue(propertyName, out var value) && value is T typedValue)
            {
                return typedValue;
            }
            return default;
        }
    }
}
