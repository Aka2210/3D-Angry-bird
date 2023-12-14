using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFollowCamera : MonoBehaviour
{
    public CinemachineVirtualCamera birdCamera;
    public GameObject cloneObject;
    public bool Throw = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(cloneObject == null && Throw)
        {
            PrepareToCloseCamera(2f);
            Throw = false;
        }
    }

    public void PrepareToCloseCamera(float delay)
    {
        StartCoroutine(CloseCamera(delay));
    }

    IEnumerator CloseCamera(float delay)
    {
        //delay秒後執行下面的程式
        yield return new WaitForSeconds(delay);
        birdCamera.Priority = 0;
    }
}
