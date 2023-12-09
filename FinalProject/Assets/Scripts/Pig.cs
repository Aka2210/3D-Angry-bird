using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 10, _currentHealth;
    [SerializeField] private HealthBar _healthBar;
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentHealth > 0)
        {
            _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        _currentHealth -= collision.relativeVelocity.magnitude / 100;
    }
}
