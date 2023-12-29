using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 10, _currentHealth;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] AudioClip damageSound, dieSound;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject PigDieParticle;
    
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
        if(audioClips.Count != 0 && !audioSource.isPlaying && !_waitPlay)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
            _waitPlay = true;
            StartCoroutine(DelayPlay(Random.Range(1, 4)));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        _currentHealth -= (collision.impulse.magnitude / this.GetComponent<Rigidbody>().mass);
        if (_currentHealth > 0)
        {
            audioSource.clip = damageSound;
            if(!audioSource.isPlaying)
                audioSource.Play();
            _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
        }
        else
        {
            audioSource.clip = dieSound;
            if (!audioSource.isPlaying)
                audioSource.Play();
            _currentHealth = 0;
            _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
            Instantiate(PigDieParticle, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), Quaternion.identity);
            Destroy(gameObject,0.3f);
        }
    }

    IEnumerator DelayPlay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
        _waitPlay = false;
    }
}
