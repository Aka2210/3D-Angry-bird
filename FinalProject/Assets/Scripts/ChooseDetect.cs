using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    Dictionary<GameObject, Image> _imgs = new Dictionary<GameObject, Image>();
    Dictionary<GameObject, GameObject> _birds = new Dictionary<GameObject, GameObject>();
    Dictionary<GameObject, AudioClip> _audioClips = new Dictionary<GameObject, AudioClip>();
    Dictionary<GameObject, AudioSource> _audioSources = new Dictionary<GameObject, AudioSource>();
    Dictionary<GameObject, AudioClip> _yellClips = new Dictionary<GameObject, AudioClip>();
    Dictionary<GameObject, AudioSource> _yellSources = new Dictionary<GameObject, AudioSource>();
    [SerializeField]
    GameObject PickUpUI, Egg, _player;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float _detectRadius;
    [SerializeField] Image nowBird;
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
        Collider[] colliders = Physics.OverlapSphere(_player.transform.position, _detectRadius);

        // 按距離由小到大排序碰撞的物件
        if (colliders.Length > 1)
        {
            System.Array.Sort(colliders, (c1, c2) =>
            {
                float distance1 = Vector3.Distance(_player.transform.position, c1.transform.position);
                float distance2 = Vector3.Distance(_player.transform.position, c2.transform.position);
                return distance1.CompareTo(distance2);
            });
        }

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
