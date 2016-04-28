using System.Collections.Concurrent;
using System.Collections.Generic;
using Structura.Shared.Utilities;

namespace Structura.WebApiOwinBoilerPlate.WebService.Api.TestRest
{
    /// <summary>
    /// Simple dictionary with data for demonstration purpose.
    /// </summary>
    public class SimpleTestRestValuesCollection : ConcurrentDictionary<int, string>
    {
        public SimpleTestRestValuesCollection(IEnumerable<KeyValuePair<int, string>> collection) : base(collection)
        {
        }

        public KeyValuePair<int, string> Create(string value)
        {
            var id = GetNewId();
            // do not update existing value
            AddOrUpdate(id, value, (key, existingValue) => existingValue);
            return new KeyValuePair<int, string>(id, value);
        }

        private int GetNewId()
        {
            var newId = Randomizer.NextInt(10, 999999);
            return ContainsKey(newId) ? GetNewId() : newId;
        }
    }
}