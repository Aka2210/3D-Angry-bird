using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatildaCollider : BirdCommonVar
{
    [SerializeField] GameObject eggBomb;
    [SerializeField] GameObject ThrowingObject;
    [SerializeField] Transform ThrowingOrient;
    [SerializeField] Rigidbody rb;
    bool isLayEgg = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        //如果R被按下而且尚未使用過小雞技能則執行小雞技能並標記為已使用過技能
        if (Input.GetKeyDown(KeyCode.R) && isLayEgg == false)
        {
            LayEgg();
            isLayEgg = true;
        }
    }

    private void LayEgg()
    {
        //設定爆炸雞蛋生成位置，+0.5f的offset避免一生成就撞到小雞的碰撞箱導致爆炸
        Vector3 temp = this.transform.position + Vector3.down * 0.5f;
        GameObject clonedObject = Instantiate(eggBomb, temp, Quaternion.identity);

        //設定小雞下蛋後向向上、前飛的力量
        int ThrowPowerX = 30, ThrowPowerY = 50;
        rb.AddForce(ThrowingOrient.forward * ThrowPowerX + ThrowingOrient.up * ThrowPowerY, ForceMode.Impulse);

        //十秒後刪除該雞蛋物件，確保雞蛋即使掉進虛空也會被刪除
        Destroy(clonedObject, 10.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //丟出後若碰撞到物件則關閉下蛋功能並在三秒後刪除小雞
        HasCollider = true;
        isLayEgg = true;
        Destroy(gameObject, 3.0f);
    }
}
