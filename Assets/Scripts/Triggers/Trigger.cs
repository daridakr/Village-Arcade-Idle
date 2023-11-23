using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public abstract class Trigger<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private bool _disableStayCallback = false;

    private List<KeyValuePair<Collider, T>> _enteredObjects;

    public event UnityAction<T> Enter;
    public event UnityAction<T> Stay;
    public event UnityAction<T> Exit;

    private void Awake()
    {
        _enteredObjects = new List<KeyValuePair<Collider, T>>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out T triggered))
        {
            _enteredObjects.Add(new KeyValuePair<Collider, T>(other, triggered));
            Enter?.Invoke(triggered);

            OnEnter(triggered);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_disableStayCallback)
        {
            return;
        }

        if (other.TryGetComponent(out T triggered))
        {
            Stay?.Invoke(triggered);

            OnStay(triggered);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out T triggered))
        {
            _enteredObjects.Remove(new KeyValuePair<Collider, T>(other, triggered));
            Exit?.Invoke(triggered);

            OnExit(triggered);
        }
    }

    protected virtual void OnEnter(T triggered) { }
    protected virtual void OnStay(T triggered) { }
    protected virtual void OnExit(T triggered) { }
}
