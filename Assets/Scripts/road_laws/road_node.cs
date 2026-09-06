using UnityEngine;
public class RoadNode : MonoBehaviour {
    public RoadForm next;
    public RoadForm prev;

    [HideInInspector]
    public int entry_id;
    [HideInInspector]
    public Transform location;

    public bool reached(Vector3 pos) {
        return Vector3.Distance(pos,location.position) < 0.01f;
    }

    void Start() {
        location = GetComponent<Transform>();
    }
}
