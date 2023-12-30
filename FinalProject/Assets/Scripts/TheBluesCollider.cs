using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBluesCollider : BirdCommonVar
{
    // Start is called before the first frame update
    [SerializeField] Transform helmet, bird1, bird2, bird3;
    [SerializeField] Rigidbody rb1, rb2, rb3, rbH;
    bool isSplit = false;
    void Start()
    {
        //把不要的丟到天邊
        //transform.localPosition = Vector3.zero;
        //bird1.position = transform.position;
        /*helmet.position = new Vector3(-1000, 0, 0);
        bird2.position = new Vector3(-1000, 0, 0);
        bird3.position = new Vector3(-1000, 0, 0);*/
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
        //把不小心遺棄的孩子撿回來
        //bird2.position = transform.position;
        //bird3.position = transform.position;
        rb1.isKinematic = false;
        rb2.isKinematic = false;
        rb3.isKinematic = false;
        rbH.isKinematic = false;
        int throwPower = 30;
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
