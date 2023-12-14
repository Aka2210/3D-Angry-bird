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
    float ThrowPowerX, ThrowPowerY;
    public CharacterController character;
    public Transform orient, Player;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponentInParent<ThrowControllor>().ThrowingObject.GetComponent<Rigidbody>();
        ThrowPowerX = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerX;
        ThrowPowerY = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerY;
        initialVelocit.x = ThrowPowerX / rb.mass;
        initialVelocit.y = ThrowPowerY / rb.mass;
    }

    private void LateUpdate()
    {
        if (GetComponent<CinemachineVirtualCamera>().Priority == 100)
        {
            ThrowPowerX = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerX;
            ThrowPowerY = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerY;
            initialVelocit.x = ThrowPowerX / rb.mass;
            initialVelocit.y = ThrowPowerY / rb.mass;
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
        Vector3 offset = new Vector3(GetComponentInParent<Animator>().GetBool("PowerThrow") ? (float)0.5 : 0, character.height - orient.position.y, 0);
        Vector3[] points = new Vector3[numPoints];
        for (int i = 0; i < numPoints; i++)
        {
            float t = i > 3 ? i * timeResolution : 3 * timeResolution;
            Vector3 point = new Vector3(
                0,
                initialVelocit.y * t + 0.5f * Physics2D.gravity.y * t * t,
                initialVelocit.x * t
            );

            //使point*orient旋轉讓y、z軸方向與當前orient的y、z軸方向相同，然後+上position視為以orient.position作為起點，最後+上offset，修正投擲誤差
            points[i] = orient.rotation * point + orient.position + offset;
        }

        lineRenderer.positionCount = numPoints;
        lineRenderer.SetPositions(points);
    }
}
