using System;
using System.Collections.Generic;

namespace FeatureDemo.Core.Helpers
{
    public class MultiValueDictionary<TKey, TValue> : List<KeyValuePair<TKey, TValue>>
    {
        public MultiValueDictionary()
        {
        }

        public MultiValueDictionary(IEnumerable<KeyValuePair<TKey, TValue>> keyValues)
        {
            this.AddRange(keyValues);    
        }

        public void Add(TKey key, TValue value)
        {
            this.Add(new KeyValuePair<TKey, TValue>(key, value));
        }
    }
}
