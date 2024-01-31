using System.Collections;
using UnityEngine;

namespace Vampire
{
    public class FixedDirectionStabAbility : StabAbility
    {
        protected override IEnumerator Stab()
        {
            hitMonsters = new FastList<GameObject>();
            timeSinceLastAttack -= stabTime;
            float t = 0;
            weaponSpriteRenderer.enabled = true;
            Vector3 direction = _playerModel.LookDirection.x > 0 ? Vector3.right : Vector3.left;
            //while (t < stabTime)
            //{
            //    Vector3 attackBoxPosition = _playerModel.CenterTransform.position + direction * (weaponSize.x/2 + stabOffset + stabDistance/stabTime*t);
            //    float attackAngle = Vector3.SignedAngle(Vector3.right, direction);
            //    Collider2D[] hitColliders = Physics2D.OverlapBoxAll(attackBoxPosition, weaponSize, attackAngle, targetLayer);
                
            //    weaponSpriteRenderer.transform.position = attackBoxPosition;
            //    weaponSpriteRenderer.transform.localRotation = Quaternion.Euler(0, 0, attackAngle);

            //    foreach (Collider2D collider in hitColliders)
            //    {
            //        if (!hitMonsters.Contains(collider.gameObject))
            //        {
            //            hitMonsters.Add(collider.gameObject);
            //            Monster monster = collider.gameObject.GetComponentInParent<Monster>();
            //            DamageMonster(monster, damage.Value, direction*knockback.Value);
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
                weaponSpriteRenderer.transform.localPosition = _playerModel.CenterTransform.position + direction * (weaponSpriteRenderer.transform.localScale.x/initialScale.x*weaponSize.x/2 + stabOffset + stabDistance);
                weaponSpriteRenderer.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, EasingUtils.EaseInQuart(t));
                t += Time.deltaTime * 4;
                yield return null;
            }
            weaponSpriteRenderer.transform.localScale = initialScale;
            weaponSpriteRenderer.enabled = false;
        }
    }
}
