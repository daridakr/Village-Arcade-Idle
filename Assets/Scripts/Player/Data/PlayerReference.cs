using UnityEngine;

public abstract class PlayerReference : MonoBehaviour
{
    [SerializeField] private GameObject _dataComponents;
    [SerializeField] private GameObject _modelContainer;

    public GameObject Data => _dataComponents;
    public GameObject Model => _modelContainer;
}