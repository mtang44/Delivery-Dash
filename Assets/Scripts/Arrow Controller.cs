using UnityEngine;
using TMPro;
public class ArrowController : MonoBehaviour {
    public float degreeTurnRate = 5f;
    public GameObject distanceTMP; 
    public arrow_manager manager;
    public ArrowControl control;

    void Update() {
        arrow_manager.OrderData data = manager.get_order(control.arrow_id);
        Vector3 destination;
        if (data.has_been_picked_up) destination = data.delivery.action_location.position;
        else destination = data.pickup.action_location.position;

        float step =  degreeTurnRate * Time.deltaTime;
        Vector3 targetDirection = destination - transform.position;
        Vector3 turnDirection =  Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(turnDirection);
        // distanceTMP.GetComponent<TextMeshProUGUI>().text = "" + findDistanceToTarget() + "M";
    }

    // public int findDistanceToTarget() {
    //     float distance = Vector3.Distance(transform.position,destination.position);
        
    //     return (int)distance;
    // }
}
