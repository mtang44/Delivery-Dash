using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// road follower moves with a speed to the form.next().position, every frame it checks if it can move
/// towards the targeted position that frame, it does not move if it cannot
/// </summary>
public class RoadFollower : MonoBehaviour {
    private RoadNode entry_node;
    private RoadForm current_form;
    private Transform location;
    private int ticket = 0;

    public float speed = 2f;

    public RoadNode enter_on_start;

    void Start() {
       location = GetComponent<Transform>();
       
       entry_node = enter_on_start;
       current_form = enter_on_start.next;
       if (current_form != null)
           current_form.entered(entry_node.entry_id);
    }

    void Update() {
        // request move
        // if the end has been reached, check if next is full
        // if not, then set entry node to next and
        // set current form to next.next

        if (current_form == null) {
            current_form = entry_node.next;
            current_form.entered(entry_node.entry_id);
        }

        location.position = current_form.move(entry_node.entry_id, ticket, location.position, speed);

        RoadNode next = current_form.next(entry_node.entry_id);
        if (next.reached(location.position)) {
            if (!next.next.full()) {
                current_form.left(entry_node.entry_id, ticket);
                entry_node = next;
                current_form = next.next;
                ticket = current_form.entered(entry_node.entry_id);
            }
        }
    }
}