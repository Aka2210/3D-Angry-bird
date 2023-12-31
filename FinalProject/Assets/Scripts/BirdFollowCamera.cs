using Cinemachine;
using UnityEngine;

public class BirdFollowCamera : MonoBehaviour
{
    public CinemachineVirtualCamera birdCamera;
    public GameObject cloneObject;
    public bool Throw = false;
    [SerializeField] AudioClip audioClip;
    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(cloneObject == null && Throw)
        {
            birdCamera.Priority = 0;
            Throw = false;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}
