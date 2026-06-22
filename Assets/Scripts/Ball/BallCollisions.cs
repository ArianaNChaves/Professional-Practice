using UnityEngine;

public class BallCollisions : MonoBehaviour
{
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField, Range(10f, 100f)] private float collisionRange;
    [SerializeField] private PlayerSO playerData;
    private float _damage;
    private const float BallSpeed = 10f;
    
    Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _damage = playerData.Damage;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (Utilities.CompareLayerAndMask(obstacleMask, other.gameObject.layer))
        {
            if (_rigidbody.velocity.magnitude >= BallSpeed)
            {
                MessageSystem.Publish(new BallCrashEvent());
                MessageSystem.Publish(new BallWallCrashEvent(transform.position));
            }
        }
        
        if (!Utilities.CompareLayerAndMask(enemyMask, other.gameObject.layer)) return;
        if (!(_rigidbody.velocity.magnitude >= collisionRange)) return;
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            MessageSystem.Publish(new BallCrashEvent());
            damageable.TakeDamage(_damage); 
        }
    }
}
