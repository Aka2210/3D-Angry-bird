using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//此class用來根據當前靠近的鳥蛋選擇不同的鳥

//音效、憤怒鳥prefabs替換
[Serializable]
public class BirdImgPair
{
    public GameObject Key, _birdPrefabs;
    public Image Value;
    public AudioClip audioClip;
    public AudioSource audioSource;
    public AudioClip yellClip;
    public AudioSource yellSource;
}

public class ChooseDetect : MonoBehaviour
{
    [SerializeField] private List<BirdImgPair> pairs;

    //建立物件對應關係
    Dictionary<GameObject, Image> _imgs = new Dictionary<GameObject, Image>();
    Dictionary<GameObject, GameObject> _birds = new Dictionary<GameObject, GameObject>();
    Dictionary<GameObject, AudioClip> _audioClips = new Dictionary<GameObject, AudioClip>();
    Dictionary<GameObject, AudioSource> _audioSources = new Dictionary<GameObject, AudioSource>();
    Dictionary<GameObject, AudioClip> _yellClips = new Dictionary<GameObject, AudioClip>();
    Dictionary<GameObject, AudioSource> _yellSources = new Dictionary<GameObject, AudioSource>();

    //抓取UI
    [SerializeField]
    GameObject PickUpUI, Egg, _player;

    //偵測Layer
    [SerializeField] LayerMask layerMask;
    //Layer偵測半徑
    [SerializeField] float _detectRadius;
    //右上角相框內憤怒鳥圖片替換
    [SerializeField] Image nowBird;
    //當前角色範圍內是否有鳥可供選擇
    private bool _hasEgg;
    GameObject nowCollider;
    
    void Awake()
    {
        foreach (BirdImgPair pair in pairs)
        {
            _imgs[pair.Key] = pair.Value;
            _birds[pair.Key] = pair._birdPrefabs;
            _audioClips[pair.Key] = pair.audioClip;
            _audioSources[pair.Key] = pair.audioSource;
            _yellClips[pair.Key] = pair.yellClip;
            _yellSources[pair.Key] = pair.yellSource;
        }
    }

    void Start()
    {
        PickUpUI.SetActive(false);
    }

    void Update()
    {
        //角色為圓心，半徑範圍內所有擁有collision的物體皆被存入colliders，方便後續檢測layer
        Collider[] colliders = Physics.OverlapSphere(_player.transform.position, _detectRadius);

        // 按距離由小到大排序碰撞的物件，以此使得距離較近的鳥會先被選擇
        if (colliders.Length > 1)
        {
            System.Array.Sort(colliders, (c1, c2) =>
            {
                float distance1 = Vector3.Distance(_player.transform.position, c1.transform.position);
                float distance2 = Vector3.Distance(_player.transform.position, c2.transform.position);
                return distance1.CompareTo(distance2);
            });
        }

        //遍歷附近的collider以此偵測是否要開啟提示(E)
        foreach (var collider in colliders)
        {
            if ((layerMask.value & (1 << collider.gameObject.layer)) != 0)
            {
                PickUpUI.SetActive(true);
                _hasEgg = true;
                nowCollider = collider.gameObject;
                break;
            }
            else
            {
                PickUpUI.SetActive(false);
                _hasEgg = false;
            }
        }
       
        //按下E後附近有鳥蛋則投擲鳥直接替換為該鳥
        if(Input.GetKeyDown(KeyCode.E) && _hasEgg) 
        {
            Egg.SetActive(true);
            nowBird.sprite = _imgs[nowCollider].sprite;
            _player.GetComponent<ThrowControllor>().ThrowingObject = _birds[nowCollider];
            _player.GetComponent<ThrowControllor>()._birdYellClip = _yellClips[nowCollider];
            _player.GetComponent<ThrowControllor>()._birdYellSource = _yellSources[nowCollider];
            _audioSources[nowCollider].clip = _audioClips[nowCollider];
            _audioSources[nowCollider].Play();
        }
    }
}
