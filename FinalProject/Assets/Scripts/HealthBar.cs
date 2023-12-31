using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    [SerializeField] private Pig pig;
    private void Start()
    {
    }
    private void Update()
    {
        transform.position = new Vector3(pig.transform.position.x, transform.position.y, pig.transform.position.z);
    }
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        if (_healthbarSprite != null)
        {
            currentHealth = currentHealth >= 0 ? currentHealth : 0;
            _healthbarSprite.fillAmount = currentHealth / maxHealth;
        }
    }
}
