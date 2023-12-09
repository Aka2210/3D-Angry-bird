using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    [SerializeField] private Pig pig;
    private void Start()
    {
        transform.position = pig.transform.position + Vector3.up * 2f;
    }
    private void Update()
    {
        transform.position = pig.transform.position + Vector3.up * 2f;
    }
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _healthbarSprite.fillAmount = currentHealth / maxHealth;
    }
}
