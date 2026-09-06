using UnityEngine;

public class RoadFormSegment : MonoBehaviour, RoadForm {
    public RoadNode entry;
    public RoadNode exit;

    private KeyStore follower_count = new KeyStore(); 

    public RoadNode next(int _entry_id) {
        return exit;
    }

    public bool full() {
        //TODO: full, this is temp, this prevents traffic collisions
        return follower_count.active() > 0;
    }

    public int entered(int _entry_id) {
        return follower_count.make();
    }

    public void left(int _entry_id, int ticket) {
        follower_count.free(ticket);
    }

    public Vector3 move(int _entry_id, int _ticket, Vector3 current_position, float speed) {
        //TODO: can move into
        return Vector3.MoveTowards(current_position, exit.location.position, speed*Time.deltaTime);
    }

    void Start() {
       entry.entry_id = 0;
       entry.next = this;
       exit.prev = this;
       //TODO: find follower capacity
    }
}