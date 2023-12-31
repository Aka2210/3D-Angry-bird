using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    public Pig pig;
    float deltaHight;
    private void Start()
    {
        deltaHight = transform.position.y - pig.transform.position.y + 1f;
    }
    private void Update()
    {
        transform.position = new Vector3(pig.transform.position.x, pig.transform.position.y + deltaHight, pig.transform.position.z);
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
