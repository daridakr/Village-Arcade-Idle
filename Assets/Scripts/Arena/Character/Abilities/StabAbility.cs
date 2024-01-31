using System.Collections;
using UnityEngine;

namespace Vampire
{
    public class StabAbility : MeleeAbility
    {
        [Header("Stab Stats")]
        [SerializeField] protected float stabOffset;
        [SerializeField] protected float stabDistance;
        [SerializeField] protected float stabTime;
        protected Vector3 weaponSize;
        protected FastList<GameObject> hitMonsters;

        protected override void Use()
        {
            base.Use();
            weaponSize = weaponSpriteRenderer.bounds.size;
            weaponSpriteRenderer.enabled = false;
        }

        protected override void Attack()
        {
            StartCoroutine(Stab());
        }

        protected virtual IEnumerator Stab()
        {
            hitMonsters = new FastList<GameObject>();
            timeSinceLastAttack -= stabTime;
            float t = 0;
            weaponSpriteRenderer.enabled = true;
            Vector3 dir = _playerModel.LookDirection;
            //while (t < stabTime)
            //{
            //    Vector3 attackBoxPosition = (Vector3)_playerModel.CenterTransform.position + dir * (weaponSize.x/2 + stabOffset + stabDistance/stabTime*t);
            //    float attackAngle = Vector3.SignedAngle(Vector3.right, dir);
            //    Collider2D[] hitColliders = Physics2D.OverlapBoxAll(attackBoxPosition, weaponSize, attackAngle, targetLayer);
                
            //    weaponSpriteRenderer.transform.position = attackBoxPosition;
            //    weaponSpriteRenderer.transform.localRotation = Quaternion.Euler(0, 0, attackAngle);

            //    foreach (Collider2D collider in hitColliders)
            //    {
            //        if (!hitMonsters.Contains(collider.gameObject))
            //        {
            //            hitMonsters.Add(collider.gameObject);
            //            Monster monster = collider.gameObject.GetComponentInParent<Monster>();
            //            DamageMonster(monster, damage.Value, dir*knockback.Value);
            //            _playerHealth.OnDealDamage.Invoke(damage.Value);
            //        }
            //    }

            //    t += Time.deltaTime;
            //    yield return null;
            //}
            Vector3 initialScale = weaponSpriteRenderer.transform.localScale;
            t = 0;
            while (t < 1)
            {
                weaponSpriteRenderer.transform.localPosition = _playerModel.CenterTransform.position + dir * (weaponSpriteRenderer.transform.localScale.x/initialScale.x*weaponSize.x/2 + stabOffset + stabDistance);
                weaponSpriteRenderer.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, EasingUtils.EaseInQuart(t));
                t += Time.deltaTime * 4;
                yield return null;
            }
            weaponSpriteRenderer.transform.localScale = initialScale;
            weaponSpriteRenderer.enabled = false;
        }

        protected virtual void DamageMonster(Monster monster, float damage, Vector3 knockback)
        {
            monster.TakeDamage(damage, knockback);
        }
    }
}
