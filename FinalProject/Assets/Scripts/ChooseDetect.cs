using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseDetect : MonoBehaviour
{
    Camera cam;
    //�i�����a���UE�i�H�ߨ����J��UI
    [SerializeField] GameObject PickUpUI;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject Egg;
    void Start()
    {
        // ����۾����
        cam = Camera.main;
        //���ø�UI
        PickUpUI.SetActive(false);
    }

    void Update()
    {
        // �q���w��point�Ыخg�u
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        //�g�u�IĲ��ƻ�
        RaycastHit hit;
        
        // �i��g�u���z�p��, ray���g�u, hit�Ψӽեήg�u���, 3f���g�u����, layerMask���L�o��, �u���Q�I�쪺����layer = layerMask�ɤ~�|�i�JIf
        if (Physics.Raycast(ray, out hit, 3f, layerMask))
        {
            //��ܸ�UI
            PickUpUI.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E)) 
            {
                Egg.SetActive(true);
            }
        }
        else
        {
            //���ø�UI
            PickUpUI.SetActive(false);
        }

        // �b�������Ϥ���ܮg�u�A��Kdebug����i�R
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
    }
}
