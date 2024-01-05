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

        //press R to lay egg
        if (Input.GetKeyDown(KeyCode.R) && isLayEgg == false)
        {
            LayEgg();
            isLayEgg = true;
        }
    }

    private void LayEgg()
    {
        //下蛋
        Vector3 temp = this.transform.position + Vector3.down * 2f;
        GameObject clonedObject = Instantiate(eggBomb, temp, Quaternion.identity);

        //讓鳥稍微往上飛
        int ThrowPowerX = 30, ThrowPowerY = 50;
        rb.AddForce(ThrowingOrient.forward * ThrowPowerX + ThrowingOrient.up * ThrowPowerY, ForceMode.Impulse);

        //最晚10秒後egg消失
        Destroy(clonedObject, 10.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player")
        {
            //3秒後鳥消失
            HasCollider = true;
            isLayEgg = true;
            Destroy(gameObject, 3.0f);
        }
    }
}
