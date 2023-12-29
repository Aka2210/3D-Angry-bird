using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 10, _currentHealth;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] AudioSource audioSource;
    bool _waitPlay;
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
        _waitPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(audioSource != null && !audioSource.isPlaying && !_waitPlay)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
            _waitPlay = true;
            StartCoroutine(DelayPlay(Random.Range(1, 4)));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        _currentHealth -= collision.relativeVelocity.magnitude;
        if (_currentHealth > 0)
        {
            _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
        }
    }

    IEnumerator DelayPlay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
        _waitPlay = false;
    }
}
