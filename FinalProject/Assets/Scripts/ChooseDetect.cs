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
}

public class ChooseDetect : MonoBehaviour
{
    [SerializeField] private List<BirdImgPair> pairs;

    Dictionary<GameObject, Image> _imgs = new Dictionary<GameObject, Image>();
    Dictionary<GameObject, GameObject> _birds = new Dictionary<GameObject, GameObject>();

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
        }
    }
}
