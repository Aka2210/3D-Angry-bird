using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Vector3 _spawn;
    [SerializeField] GameObject _pigs, _player, starsDisplay, scoreDisplay, levelDisplay, clear, E;
    float score;
    float maxPigsScore;

    private void Awake()
    {
        clear.gameObject.active = false;
        score = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        for(int i = 0; i < _pigs.transform.childCount; i++)
        {
            GameObject _pig = _pigs.transform.GetChild(i).gameObject;
            maxPigsScore += _pig.GetComponent<Rigidbody>().mass * 100;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("scoreReset", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
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
            Invoke("end", 3.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Pig>() != null)
        {
            other.gameObject.GetComponent<Pig>().explosiveDamage(float.MaxValue);
            pigDie(other.gameObject.GetComponent<Rigidbody>().mass);
        }
        Destroy(other.gameObject, 1.0f);
    }

    public void PropsScore(float velocity)
    {
        score += velocity;
    }

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

    public void check()
    {
        SceneManager.LoadScene("ChooseLevelScene");
        Time.timeScale = 1;
    }

    void end()
    {
        Debug.Log(maxPigsScore);
        E.active = false;
        clear.gameObject.active = true;
        Time.timeScale = 0;
        _player.GetComponent<ThirdPersonController>().Stop = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        for (int i = 0; i < starsDisplay.transform.childCount; i++)
        {
            if (score >= (i+3) * maxPigsScore)
                starsDisplay.transform.GetChild(i).gameObject.active = true;
            else
                break;
        }
        scoreDisplay.GetComponent<TextMeshProUGUI>().text = $"Score: {(int)score}";
        levelDisplay.GetComponent<TextMeshProUGUI>().text = $"{SceneManager.GetActiveScene().name}";
    }
}
