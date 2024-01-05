using UnityEngine;

//音效設定
public class Props : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    private void Awake()
    {
        //Awake的時候先關閉音效，0.5秒後再開啟，用來避免大部分物件放置誤差導致的物件小範圍摔落造成聲音產生
        if (gameObject.GetComponent<AudioSource>() != null)
        {
            gameObject.GetComponent<AudioSource>().mute = true;
            Invoke("openMusic", 0.5f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //計算物件速度分
        if(gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.2)
            _gameManager.PropsScore(gameObject.GetComponent<Rigidbody>().velocity.magnitude);
    }

    //碰撞發出音效
    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.GetComponent<AudioSource>() != null)
            gameObject.GetComponent<AudioSource>().Play();
    }

    void openMusic()
    {
        gameObject.GetComponent<AudioSource>().mute = false;
    }
}
