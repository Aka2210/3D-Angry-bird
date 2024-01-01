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
    public islandCameraControllor islandCameraControllor;
    [SerializeField] GameManager _gameManager;

    bool _waitPlay;
    // Start is called before the first frame update
    void Start()
    {
        if (_healthBar != null)
        {
            _currentHealth = _maxHealth;
            _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
            _waitPlay = false;
        }

        if (gameObject.GetComponent<AudioSource>() != null)
        {
            gameObject.GetComponent<AudioSource>().mute = true;
            Invoke("openMusic", 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(audioClips.Count != 0 && !audioSource.isPlaying && !_waitPlay)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
            _waitPlay = true;
            StartCoroutine(DelayPlay(Random.Range(5, 8)));
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
            islandCameraControllor.openCamera(collision.collider.gameObject, 5.0f, false);
        }
        else
        {
            _gameManager.pigDie(gameObject.GetComponent<Rigidbody>().mass);
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

    public void explosiveDamage(float force)
    {
        _currentHealth -= (Mathf.Sqrt(Mathf.Sqrt(force)) / this.GetComponent<Rigidbody>().mass);
        _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
    }
    public void redSkillDamage(float force)
    {
        _currentHealth -= force / this.GetComponent<Rigidbody>().mass;
        _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
    }

    public float getHealth()
    {
        return _currentHealth;
    }
    void openMusic()
    {
        gameObject.GetComponent<AudioSource>().mute = false;
    }
}
