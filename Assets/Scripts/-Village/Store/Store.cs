using System.Collections.Generic;
using UnityEngine;

namespace Village
{
    public abstract class Store<T> : MonoBehaviour where T : IStorableObject
    {
        protected abstract DataList<T> DataList { get; }
        public IEnumerable<T> Data => DataList.Data;
    }
}