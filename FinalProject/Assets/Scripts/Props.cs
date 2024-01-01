using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    private void Awake()
    {
        if (gameObject.GetComponent<AudioSource>() != null)
        {
            gameObject.GetComponent<AudioSource>().mute = true;
            Invoke("openMusic", 0.5f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.2)
            _gameManager.PropsScore(gameObject.GetComponent<Rigidbody>().velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.GetComponent<AudioSource>() != null)
            gameObject.GetComponent<AudioSource>().Play();
    }

    void openMusic()
    {
        gameObject.GetComponent<AudioSource>().mute = false;
    }
}
