//@cpf

using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

//TODO: order creation
//TODO: order quantity increasing
//TODO: order fufillment
//TODO: arrow state machine for pickup then delivery

public class arrow_manager : MonoBehaviour {
    private List<Arrow> arrows = new List<Arrow>();
    private List<int> free_arrows = new List<int>();
    private List<int> unused_arrows = new List<int>();
    private int failed_orders = 0;
    private Dictionary<int,OrderData> active_orders = new Dictionary<int, OrderData>();

    public location_manager locations;
    public OrderableData[] order_repository;

    void Start() {
        foreach (Transform child in transform) {
            ExpireTime timer = child.gameObject.GetComponent<ExpireTime>();
            ArrowControl ctl = child.gameObject.GetComponent<ArrowControl>();
            if (timer == null || ctl == null) {
                Debug.LogError("arrow is missing either ExpireTime or ArrowControl");
                continue;
            }
            int id = arrows.Count;
            unused_arrows.Add(id);
            Arrow arrow = new Arrow(timer,ctl,child.gameObject, () => {
                failed(id);
            });
            arrow.unset();
            arrows.Add(arrow);
        }
        if (unused_arrows.Count == 0) {
            Debug.LogError("no arrows");
            return;
        }
        release_arrow();
        release_arrow();
    }

    [ContextMenu("release arrow")]
    public void release_arrow() {
        if (unused_arrows.Count > 0) {
            free(unused_arrows[0]);
            arrows[unused_arrows[0]].unset();
            arrows[unused_arrows[0]].set(order_repository[0].sprite, 16f);
            unused_arrows.RemoveAt(0);
        }
    }

    void Update() {
        foreach (Arrow arrow in arrows) {
            arrow.update();
        }
        
        foreach (int arrow_id in free_arrows) {
            location provider = locations.get_random_provider();
            location requester = locations.get_random_predicate((location loc)=>{
                return loc.requested.Intersect(provider.provided).Count() > 0;
            });
            var requestable = requester.requested.Intersect(provider.provided);
            OrderType requested = requestable.ElementAt(UnityEngine.Random.Range(0,requestable.Count()));
            int orderable_id = 0;
            bool found = false;
            for (int i = 0; i < order_repository.Count(); i+=1) {
                if (order_repository[i].typ == requested) {
                    found = true;
                    orderable_id = i;
                    break;
                }
            }
            if (!found) {
                Debug.LogError("order type is not registered: "+nameof(requested));
                continue;
            }
            OrderData data = new OrderData(orderable_id,provider,requester);
            arrows[arrow_id].set(order_repository[orderable_id].sprite, 2f);
            active_orders.Add(arrow_id, data);
        }
        free_arrows.Clear();
    }

    public void free(int id) {
        if (free_arrows.Contains(id)) {
            Debug.LogError("arrow already freed");
            return;
        }
        free_arrows.Add(id);
        arrows[id].unset();
        active_orders.Remove(id);
    }

    public void failed(int id) {
        free(id);
        failed_orders += 1;
    }

    [System.Serializable]
    public class OrderableData {
        public OrderType typ;
        public Sprite sprite;
    }

    public class OrderData {
        public int order_type_id;
        public location pickup;
        public location delivery;
        public bool has_been_picked_up = false;

        public OrderData(
            int order_type_id,
            location pickup,
            location delivery,
            bool has_been_picked_up = false
        ) {
            this.order_type_id = order_type_id;
            this.pickup = pickup;
            this.delivery = delivery;
            this.has_been_picked_up = has_been_picked_up;
        }
    }
}

public class Arrow {
    public ExpireTime timer;
    public ArrowControl ctl;
    public GameObject host;

    public Arrow(
        ExpireTime timer, 
        ArrowControl ctl, 
        GameObject host, 
        Action callback
    ) {
        this.timer = timer;
        this.ctl = ctl;
        this.host = host;

        this.timer.use_callback = true;
        this.timer.callback = callback;
    }

    public void update() {
        ctl.progress = timer.left_unit();
        // this is where screen orientation code could go???
    }

    public void unset() {
        host.SetActive(false);
        timer.reset_paused();
    }

    public void set(Sprite order_sprite, float until) {
        host.SetActive(true);
        timer.reset(until);
        ctl.set_image(order_sprite);
    }
}

public enum OrderType {
    Pizza, Sandwich, StirfryNoodles, Soup
}