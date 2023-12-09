using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseDetect : MonoBehaviour
{
    Camera cam;
    //告知玩家按下E可以撿取雞蛋的UI
    [SerializeField] GameObject PickUpUI;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject Egg;
    void Start()
    {
        // 獲取相機資料
        cam = Camera.main;
        //隱藏該UI
        PickUpUI.SetActive(false);
    }

    void Update()
    {
        // 從給定的point創建射線
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        //射線碰觸到甚麼
        RaycastHit hit;
        
        // 進行射線物理計算, ray為射線, hit用來調用射線資料, 3f為射線長度, layerMask為過濾器, 只有被碰到的物件layer = layerMask時才會進入If
        if (Physics.Raycast(ray, out hit, 3f, layerMask))
        {
            //顯示該UI
            PickUpUI.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E)) 
            {
                Egg.SetActive(true);
            }
        }
        else
        {
            //隱藏該UI
            PickUpUI.SetActive(false);
        }

        // 在場景視圖中顯示射線，方便debug後期可刪
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
    }
}
