using UnityEngine;

public class MatildaCollider : BirdCommonVar
{
    [SerializeField] GameObject eggBomb;
    [SerializeField] GameObject ThrowingObject;
    [SerializeField] Transform ThrowingOrient;
    [SerializeField] Rigidbody rb;
    bool isLayEgg = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        //�p�GR�Q���U�ӥB�|���ϥιL�p���ޯ�h����p���ޯ�üаO���w�ϥιL�ޯ�
        if (Input.GetKeyDown(KeyCode.R) && isLayEgg == false)
        {
            LayEgg();
            isLayEgg = true;
        }
    }

    private void LayEgg()
    {
        //�]�w�z�����J�ͦ���m�A+0.5f��offset�קK�@�ͦ��N����p�����I���c�ɭP�z��
        Vector3 temp = this.transform.position + Vector3.down * 2f;
        GameObject clonedObject = Instantiate(eggBomb, temp, Quaternion.identity);

        //�]�w�p���U�J��V�V�W�B�e�����O�q
        int ThrowPowerX = 30, ThrowPowerY = 50;
        rb.AddForce(ThrowingOrient.forward * ThrowPowerX + ThrowingOrient.up * ThrowPowerY, ForceMode.Impulse);

        //�Q���R�������J����A�T�O���J�Y�ϱ��i��Ť]�|�Q�R��
        Destroy(clonedObject, 10.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player")
        {
            //��X��Y�I���쪫��h�����U�J�\��æb�T����R���p��
            HasCollider = true;
            isLayEgg = true;
            Destroy(gameObject, 3.0f);
        }
    }
}
