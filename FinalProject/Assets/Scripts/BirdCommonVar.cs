using UnityEngine;

public class BirdCommonVar : MonoBehaviour
{
    public bool HasCollider = false;
    [SerializeField] float _birdsDeleteHeight;
    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (GetComponent<Transform>().position.y <= _birdsDeleteHeight)
        {
            Destroy(gameObject, 1f);
        }
    }
}
