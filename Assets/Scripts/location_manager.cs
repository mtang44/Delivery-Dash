//@cpf

using UnityEngine;
using System.Collections.Generic;
using System;

public class location_manager : MonoBehaviour {
    private List<location> locations = new List<location>();
    
    public location get_random() {
        return locations[UnityEngine.Random.Range(0,locations.Count)];
    }

    public location get_random_provider() {
        return get_random_predicate((loc) => loc.provider());
    }

    public location get_random_requester() {
        return get_random_predicate((loc) => loc.requester());
    }

    public location get_random_predicate(Func<location,bool> predicate) {
        List<int> grabbed = new List<int>();
        int seen = 0;
        while (seen < locations.Count) {
            int rand = UnityEngine.Random.Range(0,locations.Count);
            if (grabbed.Contains(rand)) continue;
            location loc = locations[rand];
            if (predicate(loc)) return loc;
            seen+=1;
            grabbed.Add(rand);
        }

        Debug.LogError("no locations meet predicate");

        return null;
    }

    public void add(location loc) {
        loc.quick_id = locations.Count;
        locations.Add(loc);
    }

    void Start() {
        foreach (Transform child in transform) {
            locations.Add(child.gameObject.GetComponent<location>());
        }
        for (int idx = 0; idx < locations.Count; idx+=1) {
            locations[idx].quick_id = idx;
        }
    }
}
