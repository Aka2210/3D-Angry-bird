using UnityEngine;

public class TNTCollider : BombCollider
{
    [SerializeField] islandCameraControllor _islandCameraControllor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        //如果受力過大則爆炸
        if (collision.relativeVelocity.magnitude >= triggerForce)
        {
            if (_islandCameraControllor != null)
                _islandCameraControllor.openCamera(collision.collider.gameObject, 5.0f, true);
            Explosion();
        }
    }
}
