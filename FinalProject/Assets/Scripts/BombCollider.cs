using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BombCollider : MonoBehaviour
{
    [SerializeField] public float triggerForce;
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionForce;
    [SerializeField] GameObject particles;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�p�G�I�����O>=Ĳ�oTNT�һݪ��O�h�i��TNT�z���{���X
        if (collision.relativeVelocity.magnitude >= triggerForce)
        {
            StartCoroutine(DelayedExplosion(collision, 2.0f));
        }
    }

    IEnumerator DelayedExplosion(Collision collision, float delay)
    {
        //����򥻾ާ@
        animator.SetBool("Collider", true);

        //����delay��
        yield return new WaitForSeconds(delay);

        //�z��
        Explosion(collision);
    }

    public void Explosion(Collision collision)
    {
        //�Q��Physics����Htransform.position������, explosionRadius���b�|�����d�򤺩Ҧ�������
        var surroundingObject = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var obj in surroundingObject)
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb == null) continue;

            //�����Ҧ����񪫥�@�z�}�O(�����z�����O)�A�ѼƤ��O��(�z���O�q, �z�������I, �z���b�|)
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        //�Ыئ������z���ɤl�ĪG
        GameObject explosive = Instantiate(particles, transform.position, Quaternion.identity);

        //�R��TNT����(�ϥ�gameObject�s�P���B�l����@�_�R��)
        Destroy(gameObject);
        //����R���z���Ϊ��ɤl�ĪG�A�קK���|�Ӧh�ɭP�d�y
        Destroy(explosive, 2);
    }
}
