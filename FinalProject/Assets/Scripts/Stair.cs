using StarterAssets;
using UnityEngine;

//攀爬樓梯
public class Stair : MonoBehaviour
{
    float horizontal, vertical;
    [SerializeField] float _upSpeed;
    [SerializeField] GameObject _player;
    [SerializeField] ThirdPersonController _controller;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //偵測是否按下前後方向鍵
        vertical = Input.GetAxisRaw("Vertical");
        //偵測是否按下左右方向鍵
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player"  && (vertical != 0 || horizontal != 0))
        {
            //以玩家為起始點向前射出射線並把碰撞到的物體儲存在hit
            Physics.Raycast(new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z), _player.transform.forward, out hit);
            Debug.Log(hit.collider.gameObject.name);
            //如果碰撞到的物體=樓梯，代表玩家面相樓梯，使玩家向上爬
            if (hit.collider.gameObject == gameObject)
                _controller._verticalVelocity = _upSpeed;
        }
    }
}
