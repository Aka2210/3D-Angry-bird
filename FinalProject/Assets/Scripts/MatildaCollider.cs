using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatildaCollider : MonoBehaviour
{
    [SerializeField] GameObject eggBomb;
    [SerializeField] GameObject ThrowingObject;
    [SerializeField] Transform ThrowingOrient;
    [SerializeField] Rigidbody rb;
    bool isLayEgg = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isLayEgg == false)
        {
            LayEgg();
            isLayEgg = true;
        }
    }

    private void LayEgg()
    {
        Vector3 temp = this.transform.position + Vector3.down * 0.5f;
        GameObject clonedObject = Instantiate(eggBomb, temp, Quaternion.identity);

        int ThrowPowerX = 30, ThrowPowerY = 50;
        rb.AddForce(ThrowingOrient.forward * ThrowPowerX + ThrowingOrient.up * ThrowPowerY, ForceMode.Impulse);
    }

}
