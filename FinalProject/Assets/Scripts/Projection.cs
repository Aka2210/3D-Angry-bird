using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour {
    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxPhysicsFrameIterations = 100; 
    [SerializeField] private Animator _animator;

    private Scene _simulationScene;
    private PhysicsScene _physicsScene;

    private void Start() {
        //創建一個只有物理模組的場景
        CreatePhysicsScene();
    }

    private void CreatePhysicsScene() {
        //創建場景
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();
    }

    private void Update() {

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