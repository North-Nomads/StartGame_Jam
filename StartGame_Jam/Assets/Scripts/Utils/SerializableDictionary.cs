using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
	[Serializable]
	public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
		[SerializeField] private List<SerializableDictionaryRecord> records = new();

        public void OnAfterDeserialize()
        {
            Clear();
            foreach (var record in records)
            {
                if (!ContainsKey(record.Key))
                    Add(record.Key, record.Value);
            }
        }

        public void OnBeforeSerialize()
        {
            //records.Clear();
            //foreach (var pair in this)
            //{
            //    records.Add(new() { Key = pair.Key, Value = pair.Value });
            //}
        }

        [Serializable]
        public class SerializableDictionaryRecord
		{
			public TKey Key;

			public TValue Value;
		}
	}
}