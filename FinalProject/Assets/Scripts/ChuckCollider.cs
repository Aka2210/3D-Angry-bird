using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuckCollider : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject ThrowingObject;
    [SerializeField] Transform ThrowingOrient;
    bool isEnhance = false;
    Vector3 currentRotation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        while (transform.eulerAngles.x < 30)
        {
            float newRotationX = currentRotation.x + 30f * Time.deltaTime;
            transform.rotation = Quaternion.Euler(newRotationX, currentRotation.y, currentRotation.z);
            yield return null;
        }

        int throwPower = 50;
        rb.AddForce(ThrowingOrient.forward * throwPower, ForceMode.Impulse);


    }
}
