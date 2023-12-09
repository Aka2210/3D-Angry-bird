using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//繼承至BombCollider(從角色那邊繼承爆破函式來應用)
public class TNTCollider : BombCollider
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        //如果碰撞的力>=觸發TNT所需的力則進行TNT爆炸程式碼
        if (collision.relativeVelocity.magnitude >= triggerForce)
        {
            Explosion(collision);
        }
    }
}
