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
        openCamera(other.gameObject, 1.5f, false);
    }

    public void openCamera(GameObject collider, float delay, bool TNT)
    {
        if ((collider.tag == "Bird" || TNT) && islandCamera.Priority != 10000)
        {
            islandCamera.Priority = 10000;
            Invoke("closeCamera", delay);
        }
    }

    void closeCamera()
    {
        islandCamera.Priority = 0;
    }
}
