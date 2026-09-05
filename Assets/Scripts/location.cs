//@cpf

using UnityEngine;
using System.Linq;

public class location : MonoBehaviour {
    public Transform action_location;
    public float activation_range = 2f;
    public OrderType[] requested;
    public OrderType[] provided;

    public int quick_id;

    public bool requester() {
        return requested.Length > 0;
    }
    
    public bool provider() {
        return provided.Length > 0;
    }

    public bool eq(location other) {
        return quick_id == other.quick_id;
    }
    
    void Start() {
        if (action_location == null) {
            action_location = GetComponent<Transform>();
        }

        foreach (var item in requested) {
            if (provided.Contains(item)) {
                Debug.LogError("location contains requested item that it also provides");
            }
        }
    }
}
