using System.Collections.Generic;
using UnityEngine;

namespace ForeverVillage.Scripts
{
    public class Store<T> : MonoBehaviour where T : IStorableObject
    {
        [SerializeField] private DataList<T> _dataList;

        public IEnumerable<T> Data => _dataList.Data;
    }
}