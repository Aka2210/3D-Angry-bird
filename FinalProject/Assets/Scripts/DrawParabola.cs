using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class DrawParabola : MonoBehaviour
{
    LineRenderer lineRenderer;

    public Vector2 initialVelocit;
    public float timeResolution = 0.01f;
    public float maxTime = 0.5f;
    Rigidbody rb;
    float ThrowPowerX, ThrowPowerY, TotalPower;
    public CharacterController character;
    public Transform orient, Player;
    public Animator animator;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponentInParent<ThrowControllor>().ThrowingObject.GetComponent<Rigidbody>();
        ThrowPowerX = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerX;
        ThrowPowerY = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerY;
        TotalPower = Mathf.Sqrt(ThrowPowerX * ThrowPowerX + ThrowPowerY * ThrowPowerY);
    }

    private void LateUpdate()
    {
        if (GetComponent<CinemachineVirtualCamera>().Priority == 100 && !animator.GetBool("Throw") && GetComponentInParent<ThrowControllor>().clonedObject == null)
        {
            ThrowPowerX = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerX;
            ThrowPowerY = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerY;
            TotalPower = Mathf.Sqrt(ThrowPowerX * ThrowPowerX + ThrowPowerY * ThrowPowerY);
            Vector3 euler = orient.rotation.eulerAngles;
            float angleInDegrees = 45 - euler.x;
            float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
            float cosValue = Mathf.Cos(angleInRadians), sinValue = Mathf.Sin(angleInRadians);
            initialVelocit.x = (TotalPower * cosValue) / rb.mass;
            initialVelocit.y = (TotalPower * sinValue) / rb.mass;
            Draw();
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }

    void Draw()
    {
        int numPoints = Mathf.FloorToInt(maxTime / timeResolution);
        Vector3 euler = Player.rotation.eulerAngles;
        float angleInDegrees = euler.y;
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        float cosValue = Mathf.Cos(angleInRadians), sinValue = Mathf.Sin(angleInRadians);

        Vector3 offset = new Vector3(0.5f * cosValue, character.height, -0.5f * sinValue);
        Vector3[] points = new Vector3[numPoints];
        for (int i = 0; i < numPoints; i++)
        {
            float t = i > 3 ? i * timeResolution : 3 * timeResolution;
            Vector3 point = new Vector3(
                0,
                initialVelocit.y * t + 0.5f * Physics2D.gravity.y * t * t,
                initialVelocit.x * t
            );

            points[i] = Player.rotation * point + Player.position + offset;
        }

        lineRenderer.positionCount = numPoints;
        lineRenderer.SetPositions(points);
    }
}
