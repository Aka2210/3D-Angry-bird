using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCommonVar : MonoBehaviour
{
    public bool HasCollider = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (GetComponent<Transform>().position.y <= -2)
        {
            Destroy(gameObject, 1f);
        }
    }
}
