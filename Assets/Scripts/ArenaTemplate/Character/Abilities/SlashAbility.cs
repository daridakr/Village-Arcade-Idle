using System.Collections;
using UnityEngine;

namespace Vampire
{
    public class SlashAbility : MeleeAbility
    {
        [Header("Slash Stats")]
        [SerializeField] protected float slashAngle;
        [SerializeField] protected float slashOffset; 
        [SerializeField] protected float slashTime;
        [SerializeField] protected float scaleInTime = 0.1f;
        protected Vector3 weaponSize;
        protected Vector3 initialWeaponScale;
        private FastList<GameObject> hitMonsters;

        protected override void Use()
        {
            base.Use();
            weaponSize = weaponSpriteRenderer.bounds.size;
            initialWeaponScale = weaponSpriteRenderer.transform.localScale;
            weaponSpriteRenderer.enabled = false;
        }

        protected override void Attack()
        {
            StartCoroutine(Slash());
        }

        protected IEnumerator Slash()
        {
            hitMonsters = new FastList<GameObject>();
            timeSinceLastAttack -= slashTime;
            float t = 0;
            weaponSpriteRenderer.enabled = true;
            Vector3 initialDir = _playerModel.LookDirection;
            while (t < slashTime)
            {
                float scaleMultiplier = GetScaleMultiplier(t);
                float theta = slashAngle * (slashTime/2 - t) / slashTime;
                Vector3 dir = new Vector3(
                    initialDir.x * Mathf.Cos(Mathf.Deg2Rad * theta) - initialDir.y * Mathf.Sin(Mathf.Deg2Rad * theta),
                    initialDir.x * Mathf.Sin(Mathf.Deg2Rad * theta) + initialDir.y * Mathf.Cos(Mathf.Deg2Rad * theta)
                );
                Vector3 attackBoxPosition = _playerModel.CenterTransform.position + dir * (scaleMultiplier*weaponSize.x/2 + slashOffset);
                //Collider2D[] hitColliders = Physics2D.OverlapBoxAll(attackBoxPosition, scaleMultiplier*weaponSize, Vector3.SignedAngle(Vector3.right, dir), targetLayer);
                
                weaponSpriteRenderer.transform.position = attackBoxPosition;
                //weaponSpriteRenderer.transform.localRotation = Quaternion.Euler(0, 0, Vector3.SignedAngle(Vector3.right, dir));
                weaponSpriteRenderer.transform.localScale = initialWeaponScale * scaleMultiplier;

                //foreach (Collider2D collider in hitColliders)
                //{
                //    if (!hitMonsters.Contains(collider.gameObject))
                //    {
                //        hitMonsters.Add(collider.gameObject);
                //        Monster monster = collider.gameObject.GetComponentInParent<Monster>();
                //        monster.TakeDamage(damage.Value, dir * knockback.Value);
                //        _playerHealth.OnDealDamage.Invoke(damage.Value);
                //    }
                //}

                t += Time.deltaTime;
                yield return null;
            }
            weaponSpriteRenderer.enabled = false;
        }

        private void DeregisterMonster(Monster monster)
        {
            hitMonsters.Remove(monster.gameObject);
        }

        private float GetScaleMultiplier(float t)
        {
            if (t < scaleInTime)
            {
                return EasingUtils.EaseOutQuad(t/scaleInTime);
            }
            else if (t > slashTime - scaleInTime)
            {
                return EasingUtils.EaseOutQuad((slashTime-t)/scaleInTime);
            }
            else
            {
                return 1;
            }
        }
    }
}
