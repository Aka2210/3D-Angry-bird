using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimForward : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        Quaternion newRotation = Quaternion.EulerAngles(0, transform.rotation.y, transform.rotation.z);
        lineRenderer.SetPosition(1, (newRotation * transform.forward) * 100 + transform.position);
    }
}
