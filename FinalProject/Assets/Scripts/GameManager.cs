using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//控制遊戲開始、結束及重來

public class GameManager : MonoBehaviour
{
    [SerializeField] Vector3 _spawn;
    [SerializeField] GameObject _pigs, _player, starsDisplay, scoreDisplay, levelDisplay, clear, E;
    float score;
    float maxPigsScore;

    private void Awake()
    {
        //關閉結算畫面
        clear.gameObject.active = false;
        //初始畫分數
        score = 0;
        //關閉滑鼠
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //計算當前關卡所有小豬死亡後的分數
        for(int i = 0; i < _pigs.transform.childCount; i++)
        {
            GameObject _pig = _pigs.transform.GetChild(i).gameObject;
            maxPigsScore += _pig.GetComponent<Rigidbody>().mass * 100;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //開始後0.5秒重製分數，因為物件放置離地有誤差，此誤差會導致物件產生掉落速度而使玩家加分
        Invoke("scoreReset", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //角色掉落後重回主島嶼
        if(_player.transform.position.y <= -10)
        {
            CharacterController controller = _player.GetComponent<CharacterController>();
            Vector3 currentPosition = _player.transform.position;
            // 從當前位置到新位置的向量
            Vector3 offset = _spawn - currentPosition;
            // 使用這個向量移動
            controller.Move(offset);
        }

        if(_pigs.transform.childCount == 0)
        {
            //小豬皆死亡後三秒開始結算
            Invoke("end", 3.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //小豬若離開敵人島嶼範圍則血量歸零，直接死亡
        if (other.gameObject.GetComponent<Pig>() != null)
        {
            other.gameObject.GetComponent<Pig>().explosiveDamage(float.MaxValue);
            pigDie(other.gameObject.GetComponent<Rigidbody>().mass);
        }
        Destroy(other.gameObject, 1.0f);
    }

    //計算物件產生的分數
    public void PropsScore(float velocity)
    {
        score += velocity;
    }

    //計算小豬死亡產生的分數
    public void pigDie(float mass)
    {
        score += mass * 100;
    }

    void scoreReset()
    {
        score = 0;
    }

    public void reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    //回到選擇關卡畫面
    public void check()
    {
        SceneManager.LoadScene("ChooseLevelScene");
        Time.timeScale = 1;
    }

    void end()
    {
        Debug.Log(maxPigsScore);
        //UI關閉及開啟
        E.active = false;
        clear.gameObject.active = true;
        //暫停遊戲物理引擎
        Time.timeScale = 0;
        //暫停視角旋轉
        _player.GetComponent<ThirdPersonController>().Stop = true;
        //開啟滑鼠
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //計算星星數
        for (int i = 0; i < starsDisplay.transform.childCount; i++)
        {
            if (score >= (i+3) * maxPigsScore)
                starsDisplay.transform.GetChild(i).gameObject.active = true;
            else
                break;
        }
        //更改UI關卡、分數顯示
        scoreDisplay.GetComponent<TextMeshProUGUI>().text = $"Score: {(int)score}";
        levelDisplay.GetComponent<TextMeshProUGUI>().text = $"{SceneManager.GetActiveScene().name}";
    }
}
