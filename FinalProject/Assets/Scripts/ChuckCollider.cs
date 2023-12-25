using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuckCollider : BirdCommonVar
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject ThrowingObject;
    [SerializeField] Transform ThrowingOrient;
    bool isEnhance = false;　//是否觸發技能
    Vector3 currentRotation; // 目前rotation
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        //觸發技能:先停止前進進行旋轉再施加力量
        if (Input.GetKeyDown(KeyCode.R) && isEnhance == false)
        {
            isEnhance = true;
            currentRotation = transform.eulerAngles;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            StartCoroutine(EnhanceAndThrow());
        }
    }
    IEnumerator EnhanceAndThrow()
    {
        //將Chuck的旋轉角度轉到30度
        if(currentRotation.x > 300) currentRotation.x -= 360;
        while (transform.eulerAngles.x > 80 || transform.eulerAngles.x < 30)
        {
            float newRotationX = currentRotation.x + 100f * Time.deltaTime;
            transform.rotation = Quaternion.Euler(newRotationX, currentRotation.y, currentRotation.z);
            currentRotation = transform.eulerAngles;
            yield return null;
        }
        //對Chuck施加較強的力讓他俯衝
        int throwPower = 80;
        rb.AddForce(ThrowingOrient.forward * throwPower, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //撞到東西後三秒消失
        HasCollider = true;
        Destroy(gameObject, 3.0f);
    }
}
