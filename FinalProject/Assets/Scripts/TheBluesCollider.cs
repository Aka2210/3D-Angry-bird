using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBluesCollider : BirdCommonVar
{
    // Start is called before the first frame update
    [SerializeField] Transform helmet, bird1, bird2, bird3;
    bool isSplit = false;
    void Start()
    {
        //把不要的丟到天邊
        /*helmet.position = new Vector3(0, -1000, 0);
        bird2.position = new Vector3(0, -1000, 0);
        bird3.position = new Vector3(0, -1000, 0);*/
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        //按下R世界就會分裂成三半
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            if(isSplit == false)
            {
                isSplit = true;
                Split();
            }
        }*/
    }
    /*void Split()
    {
        //把不小心遺棄的孩子撿回來
        bird2.position = bird1.position;
        bird3.position = bird1.position;
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        HasCollider = true;
        isSplit = true;
        Destroy(gameObject, 3.0f);
    }
}
