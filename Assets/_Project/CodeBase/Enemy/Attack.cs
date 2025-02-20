 
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic; 
using System.Linq;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        public Transform ParentTransform;
        public EnemyAnimator Animator;
        public float AttckCooldown = 3f;
        public float Cleavage = 0.5f;
        public float EffectiveDistance = 0.5f;
        public int Damage = 10;

        private IGameFactory _factory;
        private Transform _heroTransform;
        private float _attackCooldown;
        private bool _isAttacking;
  
        private int _layerMask;
        public Collider[] _hits = new Collider [1]  ;
        private bool _attackIsActive;
    

        public void Construct(Transform transform)
        {
           _heroTransform = transform;
        }
        private void Awake()
        { 
            _layerMask = 1 << LayerMask.NameToLayer("Player");
             
        }
        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }
        public void EnableAttack()
        {
            _attackIsActive = true; 
        }
        public void DisableAttack()
        {
            _attackIsActive = false; 
        }
        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
        }

        private void OnAttack() 
        { 
            if(Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1);
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }
        private void OnAttackEnded()
        {
            _attackCooldown = AttckCooldown;
            _isAttacking = false;
        }

        private bool Hit(out Collider hit)
        { 
            int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitsCount > 0;
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * EffectiveDistance;
        }

       
        private void StartAttack()
        {
            Animator.PlayAttack();
            _isAttacking = true;
        }
        private bool CanAttack()
        { 
            return _attackIsActive && !_isAttacking && CooldownIsUp();
        }
        private bool CooldownIsUp()
        {
            return _attackCooldown <= 0;
        } 
    }
}