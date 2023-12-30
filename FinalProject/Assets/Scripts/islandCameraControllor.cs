using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class islandCameraControllor : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera islandCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bird")
        {
            islandCamera.Priority = 10000;
            Invoke("closeCamera", 5.0f);
        }
    }

    void closeCamera()
    {
        islandCamera.Priority = 0;
    }
}
