using UnityEngine;

public class Rotatable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float degreeTurnRate = 5f;
    [SerializeField] Transform destination;

    // Update is called once per frame
    void Update()
    {
        
        float step =  degreeTurnRate * Time.deltaTime;
        Vector3 targetDirection = destination.position - transform.position;
        Vector3 turnDirection =  Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(turnDirection);
    }
}
