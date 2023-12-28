using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    float horizontal, vertical;
    [SerializeField] float _upSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{vertical}, {horizontal}, {collision.collider.gameObject.name}");
        if (collision.collider.tag == "Player" && vertical != 0 || horizontal != 0)
        {
            Debug.Log("test");
            collision.collider.gameObject.transform.position = new Vector3(collision.collider.gameObject.transform.position.x, collision.collider.gameObject.transform.position.y + Time.deltaTime * _upSpeed, collision.collider.gameObject.transform.position.z);
        }
    }
}
