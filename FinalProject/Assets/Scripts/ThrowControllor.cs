using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowControllor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator animator;
    [SerializeField] GameObject egg;
    [SerializeField] GameObject ThrowingObject;
    [SerializeField] Transform ThrowingOrient;
    [SerializeField] Transform Orient;
    GameObject clonedObject;

    bool PowerThrow = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //偵測滑鼠左鍵被按下，0: 左鍵 1: 右鍵 2: 中鍵
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Throw", true);
            Vector3 ThrowRotate = ThrowingOrient.rotation.eulerAngles;
            ThrowRotate.x = 0;

            Orient.rotation = Quaternion.Euler(ThrowRotate);
            if(PowerThrow)
                StartCoroutine(CloseThrow(3.24f));
            else
                StartCoroutine(CloseThrow(2f));
        }

        //較遠的投擲為按住左alt+左鍵
        if(Input.GetKey(KeyCode.LeftAlt))
        {
            PowerThrow = true;
        }

        animator.SetBool("PowerThrow", PowerThrow);
    }

    private void FixedUpdate()
    {
        
    }

    void ThrowObject()
    {
        int ThrowPowerX, ThrowPowerY;
        if(PowerThrow)
        {
            ThrowPowerX = 50;
            ThrowPowerY = 50;
        }
        else
        {
            ThrowPowerX = 30;
            ThrowPowerY = 30;
        }

        Vector3 temp = new Vector3(egg.GetComponent<Transform>().position.x, egg.GetComponent<Transform>().position.y, egg.GetComponent<Transform>().position.z);
        egg.SetActive(false);

        if(clonedObject != null)
        {
            Destroy(clonedObject);
        }
        clonedObject = Instantiate(ThrowingObject, temp, Quaternion.identity);

        Rigidbody rb = clonedObject.GetComponent<Rigidbody>();
        rb.AddForce(ThrowingOrient.forward * ThrowPowerX + ThrowingOrient.up * ThrowPowerY, ForceMode.Impulse);
    }

    IEnumerator CloseThrow(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("Throw", false);
        PowerThrow = false;
    }
}
