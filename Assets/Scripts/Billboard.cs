using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   [SerializeField] public GameObject playerCam;
    // Update is called once per frame
    void Start()
    {
        playerCam = GameObject.FindGameObjectWithTag("followPos");
    }

    void Update()
    {
        transform.LookAt(playerCam.transform); 
    }
}
