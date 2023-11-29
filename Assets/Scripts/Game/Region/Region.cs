using UnityEngine;

public class Region : MonoBehaviour
{
    [SerializeField] private float _cost;

    private bool _isUnlocked => _cost == 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInteraction player))
        {
            if (!_isUnlocked)
            {
                player.DisableInteraction();
            }
        }
    }
}
