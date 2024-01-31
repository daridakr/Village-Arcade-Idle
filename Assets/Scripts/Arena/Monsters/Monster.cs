using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vampire
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Monster : IDamageable, ISpatialHashGridClient
    {
        [SerializeField] protected Material defaultMaterial, whiteMaterial, dissolveMaterial;
        [SerializeField] protected ParticleSystem deathParticles;
        [SerializeField] private PlayerHealthTrigger _playerHealthTrigger;
        [SerializeField] private Renderer _renderer;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected Collider _monsterLegsCollider;

        protected int _monsterIndex;
        protected MonsterBlueprint _monsterBlueprint;
        protected float currentHealth;
        protected EntityManager entityManager;  // monster pools

        protected ArenaPlayerCharacterModel _playerModel;
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
        public Vector3 Position => transform.position;
        public Vector3 Size => _monsterLegsCollider.bounds.size;
        public Dictionary<int, int> ListIndexByCellIndex { get; set; }
        public int QueryID { get; set; } = -1;

        protected virtual void Awake()
        {
            _body = GetComponent<Rigidbody>();
            //zPositioner = gameObject.AddComponent<ZPositioner>();

            _playerHealthTrigger.Stay += OnPlayerHealthTriggerStay;
        }

        public virtual void Init(EntityManager entityManager, ArenaPlayerCharacterModel playerModel, ArenaPlayerMovement playerMovement)
        {
            _playerModel = playerModel;
            _playerMovement = playerMovement;

            this.entityManager = entityManager;
        }

        public virtual void Setup(int monsterIndex, Vector3 position, MonsterBlueprint monsterBlueprint, float hpBuff = 0)
        {
            _monsterIndex = monsterIndex;
            _monsterBlueprint = monsterBlueprint;
            _body.position = position;
            transform.position = position;

            // Reset health to max
            currentHealth = monsterBlueprint.hp + hpBuff;

            // Toggle alive flag on
            alive = true;

            // Add to list of living monsters
            entityManager.LivingMonsters.Add(this);

            // Initialize the animator
            //monsterSpriteAnimator.Init(monsterBlueprint.walkSpriteSequence, monsterBlueprint.walkFrameTime, true);
            // Start and reset animation
            //monsterSpriteAnimator.StartAnimating(true);

            //CapsuleCollider legsCollider = gameObject.AddComponent<CapsuleCollider>();
            //legsCollider.center = new Vector3(0f, 0.5f, 0f);  // Подбирается в зависимости от геометрии вашего монстра
            //legsCollider.height = 1.0f;  // Подбирается в зависимости от геометрии вашего монстра
            //legsCollider.radius = 0.3f;  // Подбирается в зависимости от геометрии вашего монстра
            //_monsterLegsCollider = legsCollider;

            //_monsterHitbox.enabled = true;
            //// Настройка размеров и положения коллайдера в соответствии с геометрией монстра
            //_monsterHitbox.size = new Vector3(1.0f, 1.0f, 1.0f);  // Подбирается в зависимости от геометрии вашего монстра
            //_monsterHitbox.center = new Vector3(0f, 0.5f, 0f);  // Подбирается в зависимости от геометрии вашего монстра

            // Ensure colliders are enabled and sized correctly
            //monsterHitbox.enabled = true;
            //monsterHitbox.size = monsterSpriteRenderer.bounds.size;
            //monsterHitbox.offset = Vector3.up * monsterHitbox.size.y / 2;
            //_monsterLegsCollider.radius = monsterHitbox.size.x / 2.5f;
            //centerTransform = (new GameObject("Center Transform")).transform;
            //centerTransform.SetParent(transform);
            //centerTransform.position = transform.position + (Vector3)monsterHitbox.offset;

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
                //entityManager.SpawnDamageText(_monsterHitbox.transform.position, damage);
                entityManager.SpawnDamageText(transform.position, damage);
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
            _renderer.sharedMaterial = whiteMaterial;
            yield return new WaitForSeconds(0.15f);
            _renderer.sharedMaterial = defaultMaterial;
            knockedBack = false;
        }

        public virtual IEnumerator Killed(bool killedByPlayer = true)
        {
            // Toggle alive flag off and disable hitbox
            alive = false;
            //_monsterHitbox.enabled = false;
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
                _renderer.enabled = false;
                yield return new WaitForSeconds(deathParticles.main.duration - 0.15f);
                _renderer.enabled = true;
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
            entityManager.DespawnMonster(_monsterIndex, this, true);
        }

        protected virtual void DropLoot()
        {
            if (_monsterBlueprint.gemLootTable.TryDropLoot(out GemType gemType))
                entityManager.SpawnExpGem((Vector2)transform.position, gemType);
            if (_monsterBlueprint.coinLootTable.TryDropLoot(out CoinType coinType))
                entityManager.SpawnCoin((Vector2)transform.position, coinType);
        }

        private void OnDisable() => _playerHealthTrigger.Stay -= OnPlayerHealthTriggerStay;

        protected abstract void OnPlayerHealthTriggerStay(PlayerHealth playerHealth);
    }
}
