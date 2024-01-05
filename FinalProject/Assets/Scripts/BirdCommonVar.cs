using UnityEngine;

//所有憤怒鳥的基底class，用來處理高度低於刪除高度及碰撞後n秒消失
public class BirdCommonVar : MonoBehaviour
{
    public bool HasCollider = false;
    [SerializeField] float _birdsDeleteHeight;
    islandCameraControllor islandCameraControllor;
    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (gameObject.GetComponent<Transform>().position.y <= _birdsDeleteHeight)
        {
            Destroy(gameObject, 1f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player")
        {
            //撞到東西後三秒消失
            HasCollider = true;
            Destroy(gameObject, 3.0f);
        }
    }
}
