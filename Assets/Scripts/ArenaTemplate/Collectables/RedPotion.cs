using Arena;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Vampire
{
    public class RedPotion : Collectable
    {
        [SerializeField] protected float healAmount = 50;
        [SerializeField] protected float healTime = 30;

        private PlayerHealth _playerHealth;

        [Inject]
        private void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }

        protected override void OnCollected()
        {
            gameObject.SetActive(true);
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            StartCoroutine(HealOverTime());
        }

        private IEnumerator HealOverTime()
        {
            float t = 0;
            while (t < healTime)
            {
                t += Time.deltaTime;
                //_playerHealth.GainHealth(Time.deltaTime * healAmount / healTime);
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
