using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{gameObject.name}, {collision.collider.gameObject.name}");
        gameObject.GetComponent<AudioSource>().Play();
    }
}
