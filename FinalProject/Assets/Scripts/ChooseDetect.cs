using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseDetect : MonoBehaviour
{
    [SerializeField]
    GameObject PickUpUI, Egg, _player;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float _detectRadius;
    private bool _hasEgg;

    void Start()
    {
        PickUpUI.SetActive(false);
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(_player.transform.position, _detectRadius);

        foreach(var collider in colliders)
        {
            if ((layerMask.value & (1 << collider.gameObject.layer)) != 0)
            {
                PickUpUI.SetActive(true);
                _hasEgg = true;
                break;
            }
            else
            {
                PickUpUI.SetActive(false);
                _hasEgg = false;
            }
        }
       
        if(Input.GetKeyDown(KeyCode.E) && _hasEgg) 
        {
            Egg.SetActive(true);
        }
    }
}
