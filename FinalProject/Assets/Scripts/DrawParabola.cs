using Cinemachine;
using UnityEngine;

//控制是否畫出拋物線
public class DrawParabola : MonoBehaviour
{
    //參數抓取、宣告，意義同其名稱，其中ThrowingObject代表當前投擲的鳥、ThrowingOrient代表當前camera仰角，_projection代表投射計算的函式
    LineRenderer lineRenderer;
    float ThrowPowerX, ThrowPowerY;
    public Animator animator;
    [SerializeField] private Projection _projection;
    GameObject egg, ThrowingObject;
    Transform ThrowingOrient;
    public Vector3 ThrowPoint;

    void Start()
    {
        //抓取各參數
        lineRenderer = GetComponent<LineRenderer>();
        egg = GetComponentInParent<ThrowControllor>().egg;
        ThrowingObject = GetComponentInParent<ThrowControllor>().ThrowingObject;
        ThrowingOrient = GetComponentInParent<ThrowControllor>().ThrowingOrient;
        ThrowPowerX = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerX;
        ThrowPowerY = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerY;
    }

    private void LateUpdate()
    {
        //偵測當前狀態，若處於投擲畫面並尚未進入投擲動畫且先前投擲的鳥已經消失則設定參數並利用_projection內的function畫出拋物線
        if (GetComponent<CinemachineVirtualCamera>().Priority == 100 && !animator.GetBool("Throw") && GetComponentInParent<ThrowControllor>().clonedObject == null)
        {
            ThrowPowerX = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerX;
            ThrowPowerY = gameObject.GetComponentInParent<ThrowControllor>().ThrowPowerY;

            //投擲位置以當前右手中的雞蛋為原點像前左上方移動使其投擲位置設置為角色頭頂前方
            ThrowPoint = egg.transform.position;
            ThrowPoint.x += 0.069f;
            ThrowPoint.y += 0.961f;
            ThrowPoint.z += 0.212f;

            ThrowingObject = GetComponentInParent<ThrowControllor>().ThrowingObject;
            _projection.SimulateTrajectory(ThrowingObject, ThrowPoint, ThrowingOrient.forward * ThrowPowerX + ThrowingOrient.up * ThrowPowerY);
        }
        else
        {
            //若不符合條件則關閉拋物線顯示
            lineRenderer.positionCount = 0;
        }
    }
}
