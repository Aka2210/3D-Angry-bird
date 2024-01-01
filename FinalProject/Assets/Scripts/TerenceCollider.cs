using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerenceCollider : BirdCommonVar
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player" && !HasCollider)
        {
            if (collision.collider.GetComponent<Pig>() != null)
                collision.collider.GetComponent<Pig>().BigRedDamage();

            //撞到東西後三秒消失
            HasCollider = true;
            Destroy(gameObject, 3.0f);
        }

        
    }
}
