using UnityEngine;

public class GameHealthUI : MonoBehaviour
{
    [SerializeField] private Transform healthBar;

    private void Start()
    {
        HealthManager.Instance.OnHealthChange.AddListener(SetHealth);
    }

    private void SetHealth(float health)
    {
        var maxHealth = HealthManager.Instance.MaxHealth;
        
        var percentage = health / maxHealth;
        healthBar.localScale = new Vector3(percentage, 1, 1);
    }
}