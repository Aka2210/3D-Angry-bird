using UnityEngine;

public class TheBluesCollider : BirdCommonVar
{
    // Start is called before the first frame update
    [SerializeField] Transform helmet, bird1, bird2, bird3;
    [SerializeField] Rigidbody rb1, rb2, rb3, rbH;
    [SerializeField] Collider c, c1, c2, c3, cH;
    [SerializeField] int throwPower;
    bool isSplit = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        //按下R世界就會分裂成三半
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(isSplit == false)
            {
                isSplit = true;
                Split();
            }
        }
    }
    void Split()
    {
        //分別丟出去 安全帽下去
        c1.enabled = true;
        c2.enabled = true;
        c3.enabled = true;
        cH.enabled = true;
        c.enabled = false;
        rb1.isKinematic = false;
        rb2.isKinematic = false;
        rb3.isKinematic = false;
        rbH.isKinematic = false;
        rb1.AddForce(-bird1.forward * throwPower, ForceMode.Impulse);
        rb2.AddForce(-bird2.forward * throwPower, ForceMode.Impulse);
        rb3.AddForce(-bird3.forward * throwPower, ForceMode.Impulse);
        rbH.AddForce(Vector3.down * throwPower, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player")
        {
            HasCollider = true;
            isSplit = true;
            Destroy(gameObject, 3.0f);
        }
    }
}
