using System.Collections.Generic;
using UnityEngine;

public interface RoadForm {
    public RoadNode next(int entry_id);
    public bool full();
    public int entered(int entry_id);
    public void left(int entry_id, int ticket);
    public Vector3 move(int entry_id, int ticket, Vector3 position, float speed);
}

public class KeyStore {
    private List<int> free_keys = new List<int>();
    private int head = 0;
    private int active_keys = 0;

    public int make() {
        active_keys+=1;
        if (free_keys.Count > 0) {
            int key = free_keys[0];
            free_keys.RemoveAt(0);
            return key;
        }
        head+=1;
        return head-1;
    }

    public void free(int key) {
        if (key == head-1)
            head-=1;
        else 
            free_keys.Add(key);
        active_keys-=1;
    }

    public int active() {
        return active_keys;
    }
}
