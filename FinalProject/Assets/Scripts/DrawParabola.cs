using Cinemachine;
using UnityEngine;

public class DrawParabola : MonoBehaviour
{
    LineRenderer lineRenderer;
    float ThrowPowerX, ThrowPowerY;
    public Animator animator;
    [SerializeField] private Projection _projection;
    GameObject egg, ThrowingObject;
    Transform ThrowingOrient;
    public Vector3 ThrowPoint;

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

            ThrowPoint = egg.transform.position;
            ThrowPoint.x += 0.069f;
            ThrowPoint.y += 0.961f;
            ThrowPoint.z += 0.212f;

            ThrowingObject = GetComponentInParent<ThrowControllor>().ThrowingObject;
            _projection.SimulateTrajectory(ThrowingObject, ThrowPoint, ThrowingOrient.forward * ThrowPowerX + ThrowingOrient.up * ThrowPowerY);
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }
}
