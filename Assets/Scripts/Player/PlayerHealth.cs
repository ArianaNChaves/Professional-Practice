using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{ 
    [SerializeField] private PlayerSO playerData;
    
    public void TakeDamage(float damage)
    {
        AudioManager.Instance.PlayEffect("Player Hit");
        MessageSystem.Publish(new PlayerHitEvent());
    }
}
