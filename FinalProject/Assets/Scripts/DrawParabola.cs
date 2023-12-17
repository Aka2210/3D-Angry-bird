using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class DrawParabola : MonoBehaviour
{
    LineRenderer lineRenderer;
    Rigidbody rb;
    float ThrowPowerX, ThrowPowerY;
    public Animator animator;
    [SerializeField] private Projection _projection;
    GameObject egg, ThrowingObject;
    Transform ThrowingOrient;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        egg = GetComponentInParent<ThrowControllor>().egg;
        ThrowingObject = GetComponentInParent<ThrowControllor>().ThrowingObject;
        ThrowingOrient = GetComponentInParent<ThrowControllor>().ThrowingOrient;
        ThrowPowerX = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerX;
        ThrowPowerY = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerY;
    }

    private void LateUpdate()
    {
        if (GetComponent<CinemachineVirtualCamera>().Priority == 100 && !animator.GetBool("Throw") && GetComponentInParent<ThrowControllor>().clonedObject == null)
        {
            ThrowPowerX = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerX;
            ThrowPowerY = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerY;

            Vector3 ThrowPoint = egg.transform.position;
            ThrowPoint.x += 0.069f;
            ThrowPoint.y += 0.961f;
            ThrowPoint.z += 0.212f;

            _projection.SimulateTrajectory(ThrowingObject, ThrowPoint, ThrowingOrient.forward * ThrowPowerX + ThrowingOrient.up * ThrowPowerY);
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }
}
