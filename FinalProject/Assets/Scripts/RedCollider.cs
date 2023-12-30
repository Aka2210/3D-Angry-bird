using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCollider : BirdCommonVar
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
        if (collision.collider.tag != "Player")
        {
            //撞到東西後三秒消失
            HasCollider = true;
            Destroy(gameObject, 3.0f);
        }
    }
}
