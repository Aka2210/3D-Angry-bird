using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Stair : MonoBehaviour
{
    float horizontal, vertical;
    [SerializeField] float _upSpeed;
    [SerializeField] GameObject _player;
    [SerializeField] ThirdPersonController _controller;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(_player.transform.position, _player.transform.forward, out hit);
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && hit.collider.gameObject == gameObject && (vertical != 0 || horizontal != 0))
        {
            _controller._verticalVelocity = _upSpeed;
        }
    }
}
