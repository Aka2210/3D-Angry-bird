using UnityEngine;

//讓第三關可以持續旋轉
public class level2Rotate : MonoBehaviour
{
    [SerializeField] float _rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }
}
