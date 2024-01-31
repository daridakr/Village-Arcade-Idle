using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Vampire
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Monster : IDamageable, ISpatialHashGridClient
    {
        [SerializeField] protected Material defaultMaterial, whiteMaterial, dissolveMaterial;
        [SerializeField] protected ParticleSystem deathParticles;
        [SerializeField] protected GameObject shadow;
        [SerializeField] private PlayerHealthTrigger _playerHealthTrigger;

        protected BoxCollider2D monsterHitbox;
        protected CircleCollider2D monsterLegsCollider;
        protected int monsterIndex;
        protected MonsterBlueprint monsterBlueprint;
        protected SpriteAnimator monsterSpriteAnimator;
        protected SpriteRenderer monsterSpriteRenderer;
        protected ZPositioner zPositioner;
        protected float currentHealth;  // 血量
        protected EntityManager entityManager;  // 怪物池

        protected ArenaPlayerCharacterModel _playerModel;  // 角色
        protected ArenaPlayerMovement _playerMovement;

        protected Rigidbody _body;
        protected int currWalkSequenceFrame = 0;
        protected bool knockedBack = false;
        protected Coroutine hitAnimationCoroutine = null;
        protected bool alive = true;
        protected Transform centerTransform;
        public Transform CenterTransform { get => centerTransform; }
        public UnityEvent<Monster> OnKilled { get; } = new UnityEvent<Monster>();
        public float HP => currentHealth;
        // Spatial Hash Grid Client Interface
        public Vector2 Position => transform.position;
        public Vector2 Size => monsterLegsCollider.bounds.size;
        public Dictionary<int, int> ListIndexByCellIndex { get; set; }
        public int QueryID { get; set; } = -1;

        [Inject]
        private void Construct(
            ArenaPlayerCharacterModel playerModel,
            ArenaPlayerMovement playerMovement)
        {
            _playerModel = playerModel;
            _playerMovement = playerMovement;
        }

        protected virtual void Awake()
        {
            _body = GetComponent<Rigidbody>();
            monsterLegsCollider = GetComponent<CircleCollider2D>();
            monsterSpriteAnimator = GetComponentInChildren<SpriteAnimator>();
            monsterSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            zPositioner = gameObject.AddComponent<ZPositioner>();
            monsterHitbox = monsterSpriteRenderer.gameObject.AddComponent<BoxCollider2D>();
            monsterHitbox.isTrigger = true;

            _playerHealthTrigger.Stay += OnPlayerHealthTriggerStay;
        }

        public virtual void Init(EntityManager entityManager)
        {
            this.entityManager = entityManager;
            zPositioner.Init(_playerModel.transform);
        }

        public virtual void Setup(int monsterIndex, Vector2 position, MonsterBlueprint monsterBlueprint, float hpBuff = 0)
        {
            this.monsterIndex = monsterIndex;
            this.monsterBlueprint = monsterBlueprint;
            _body.position = position;
            transform.position = position;
            // Reset health to max
            currentHealth = monsterBlueprint.hp + hpBuff;
            // Toggle alive flag on
            alive = true;
            // Add to list of living monsters
            entityManager.LivingMonsters.Add(this);
            // Initialize the animator
            monsterSpriteAnimator.Init(monsterBlueprint.walkSpriteSequence, monsterBlueprint.walkFrameTime, true);
            // Start and reset animation
            monsterSpriteAnimator.StartAnimating(true);
            // Ensure colliders are enabled and sized correctly
            monsterHitbox.enabled = true;
            monsterHitbox.size = monsterSpriteRenderer.bounds.size;
            monsterHitbox.offset = Vector2.up * monsterHitbox.size.y/2;
            monsterLegsCollider.radius = monsterHitbox.size.x/2.5f;
            centerTransform = (new GameObject("Center Transform")).transform;
            centerTransform.SetParent(transform);
            centerTransform.position = transform.position + (Vector3)monsterHitbox.offset;
            // Set the drag based on acceleration and movespeed
            float spd = Random.Range(monsterBlueprint.movespeed-0.1f, monsterBlueprint.movespeed+0.1f);
            _body.drag = monsterBlueprint.acceleration / (spd * spd);
            // Reset the velocity
            _body.velocity = Vector3.zero;
            StopAllCoroutines();
        }

        protected virtual void Update()
        {
            // Direction
            //monsterSpriteRenderer.flipX = ((_playerReference.transform.position.x - rb.position.x) < 0);

            Vector3 lookAtDirection = _playerModel.transform.position - transform.position;
            lookAtDirection.y = 0; 
            transform.rotation = Quaternion.LookRotation(lookAtDirection);
        }

        protected virtual void FixedUpdate()
        {

        }

        public override void Knockback(Vector3 knockback)
        {
            _body.velocity += knockback * Mathf.Sqrt(_body.drag);
        }

        public override void TakeDamage(float damage, Vector3 knockback = default)
        {
            if (alive)
            {
                entityManager.SpawnDamageText(monsterHitbox.transform.position, damage);
                currentHealth -= damage;
                if (hitAnimationCoroutine != null) StopCoroutine(hitAnimationCoroutine);
                //if (knockback != default(Vector2))
                //{
                //    rb.velocity += knockback * Mathf.Sqrt(rb.drag);
                //    knockedBack = true;
                //}
                if (currentHealth > 0)
                    hitAnimationCoroutine = StartCoroutine(HitAnimation());
                else
                    StartCoroutine(Killed());
            }
        }

        protected IEnumerator HitAnimation()
        {
            monsterSpriteRenderer.sharedMaterial = whiteMaterial;
            yield return new WaitForSeconds(0.15f);
            monsterSpriteRenderer.sharedMaterial = defaultMaterial;
            knockedBack = false;
        }

        public virtual IEnumerator Killed(bool killedByPlayer = true)
        {
            // Toggle alive flag off and disable hitbox
            alive = false;
            monsterHitbox.enabled = false;
            // Remove from list of living monsters
            entityManager.LivingMonsters.Remove(this);
            // Drop loot
            if (killedByPlayer)
                DropLoot();

            if (deathParticles != null)
            {       
                deathParticles.Play();
            }

            yield return HitAnimation();

            if (deathParticles != null)
            {
                monsterSpriteRenderer.enabled = false;
                shadow.SetActive(false);
                yield return new WaitForSeconds(deathParticles.main.duration - 0.15f);
                monsterSpriteRenderer.enabled = true;
                shadow.SetActive(true);
            }
            // monsterSpriteRenderer.material = dissolveMaterial;
            // float t = 0;
            // while (t < 1)
            // {
            //     monsterSpriteRenderer.material.SetFloat("_Dissolve", t);
            //     t += Time.deltaTime*2;
            //     yield return null;
            // }
            // monsterSpriteRenderer.sharedMaterial = defaultMaterial;
            //yield return new WaitForSeconds(0.2f);

            // Invoke monster killed callback and remove all listeners
            OnKilled.Invoke(this);
            OnKilled.RemoveAllListeners();
            entityManager.DespawnMonster(monsterIndex, this, true);
        }

        protected virtual void DropLoot()
        {
            if (monsterBlueprint.gemLootTable.TryDropLoot(out GemType gemType))
                entityManager.SpawnExpGem((Vector2)transform.position, gemType);
            if (monsterBlueprint.coinLootTable.TryDropLoot(out CoinType coinType))
                entityManager.SpawnCoin((Vector2)transform.position, coinType);
        }

        private void OnDisable() => _playerHealthTrigger.Enter -= OnPlayerHealthTriggerStay;

        protected abstract void OnPlayerHealthTriggerStay(PlayerHealth playerHealth);
    }
}
