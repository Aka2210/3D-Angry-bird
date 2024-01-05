using Cinemachine;
using UnityEngine;

//島嶼相機，以此在特定操作後俯瞰島嶼，確定操作效果
public class islandCameraControllor : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _islandCamera;
    [SerializeField] ThrowControllor _throwControllor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //按R後若clonedObject存在則代表當前有鳥被投擲，鳥消失前按R會轉換為俯瞰視角
        if (Input.GetKeyUp(KeyCode.R) && _throwControllor != null && _throwControllor.clonedObject != null)
        {
            openCamera(_throwControllor.clonedObject, 5.0f, false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        openCamera(other.gameObject, 3f, false);
    }

    public void openCamera(GameObject collider, float delay, bool TNT)
    {
        //如果collider是小鳥或是TNT自動觸發都會讓視角轉為俯瞰視角
        if ((collider.tag == "Bird" || TNT))
        {
            _islandCamera.Priority = 10000;
            Invoke("closeCamera", delay);
        }
    }

    //關閉俯瞰島嶼相機
    void closeCamera()
    {
        _islandCamera.Priority = 0;
    }
}
