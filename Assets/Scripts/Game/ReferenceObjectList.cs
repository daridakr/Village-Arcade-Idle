using System.Collections.Generic;
using UnityEngine;

public class ReferenceObjectList<T> : MonoBehaviour where T : MonoBehaviour
{
    private List<T> _data = new List<T>();

    public IEnumerable<T> Data => _data;

}