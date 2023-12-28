using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour {
    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxPhysicsFrameIterations = 100;
    [SerializeField] private Transform _obstaclesParent;
    [SerializeField] private Animator _animator;

    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();

    private void Start() {
        //創建一個只有物理模組的場景
        CreatePhysicsScene();
    }

    void childDfs(GameObject ghostObj)
    {
        if (ghostObj.GetComponent<Renderer>() != null)
            ghostObj.GetComponent<Renderer>().enabled = false;

        if (ghostObj.GetComponent<HealthBar>() != null)
            ghostObj.gameObject.active = false;

        foreach (Transform child in ghostObj.transform)
            childDfs(child.gameObject);
    }

    private void CreatePhysicsScene() {
        //創建場景
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();

        //將當前場景中_obstaclesParent中的物件逐一生成並關閉渲染(避免花費太多效能)，然後放入創建的物理場景中，如果物件非static(代表可以物理作用)，將其放入字典中
        foreach (Transform obj in _obstaclesParent) {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            childDfs(ghostObj);
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            if (!ghostObj.isStatic) _spawnedObjects.Add(obj, ghostObj.transform);
        }
    }

    private void Update() {
        //將字典中的物件抓出，一一將其位置更改為原場景的位置
        foreach (Transform obj in _obstaclesParent) {
            _spawnedObjects[obj] = obj;
        }
    }

    public void SimulateTrajectory(GameObject ThrowingObject, Vector3 pos, Vector3 velocity) {
        //將要丟出的物件複製到物理場景中
        var ghostObj = Instantiate(ThrowingObject, pos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);

        //模擬一次投擲軌跡
        ghostObj.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);

        _line.positionCount = _maxPhysicsFrameIterations;

        //根據投擲的動畫更改虛線點的大小
        if (_animator.GetBool("PowerThrow"))
        {
            _line.textureScale = new Vector2(0.166f, 0.33f);
        }
        else
            _line.textureScale = new Vector2(0.1f, 0.33f);

        //將投擲的軌跡一一畫出
        for (var i = 0; i < _maxPhysicsFrameIterations; i++) {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, ghostObj.transform.position);

            //如果碰撞則直接離開(代表虛線一遇到物體就停止)
            if (ghostObj.GetComponent<BirdCommonVar>().HasCollider || ghostObj.transform.position.y <= -1.5f)
            {
                _line.positionCount = i;
                break;
            }
        }

        Destroy(ghostObj.gameObject);
    }
}