using UnityEngine;

public class RedCollider : BirdCommonVar
{
    [SerializeField] GameObject _effect;
    [SerializeField] float skillRange, damage, rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _effect.transform.childCount; i++)
        {
            Transform effect = _effect.transform.GetChild(i);
            effect.gameObject.GetComponent<ParticleSystem>().startSize = skillRange * 4;
        }
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        var surroundingObject = Physics.OverlapSphere(transform.position, skillRange);
        foreach(Collider other in surroundingObject)
        {
            GameObject obj = other.gameObject;
            if (obj.GetComponent<Pig>() != null)
            {
                obj.GetComponent<Pig>().islandCameraControllor.openCamera(obj, 5.0f, false);
                obj.GetComponent<Pig>().redSkillDamage(damage);
            }
            if(obj.tag != "Bird")
                obj.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player")
        {
            //撞到東西後三秒消失
            HasCollider = true;
            Destroy(gameObject, 3.0f);
        }
    }
}
