using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTCollider : MonoBehaviour
{
    [SerializeField] float triggerForce;
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionForce;
    [SerializeField] GameObject particles;
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
        //如果碰撞的力>=觸發TNT所需的力則進行TNT爆炸程式碼
        if(collision.relativeVelocity.magnitude >= triggerForce)
        {
            //利用Physics抓取以transform.position為中心, explosionRadius為半徑的橢圓範圍內所有的物件
            var surroundingObject = Physics.OverlapSphere(transform.position, explosionRadius);
            
            foreach(var obj in surroundingObject)
            {
                var rb = obj.GetComponent<Rigidbody>();
                if (rb == null) continue;

                //給予所有附近物件一爆破力(模擬爆炸的力)，參數分別為(爆炸力量, 爆炸中心點, 爆炸半徑)
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            //創建此物件爆炸粒子效果
            GameObject explosive = Instantiate(particles, transform.position, Quaternion.identity);

            //刪除TNT物件(使用gameObject連同父、子物件一起刪除)
            Destroy(gameObject);
            //兩秒後刪除爆炸用的粒子效果，避免堆疊太多導致卡頓
            Destroy(explosive, 2);
        }
    }
}
