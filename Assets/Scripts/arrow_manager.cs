//@cpf

using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

//TODO: order quantity increasing
//TODO: order fufillment
//TODO: arrow state machine for pickup then delivery

public class arrow_manager : MonoBehaviour {
    private List<Arrow> arrows = new List<Arrow>();
    private List<int> free_arrows = new List<int>();
    private List<int> unused_arrows = new List<int>();
    private Dictionary<int,OrderData> active_orders = new Dictionary<int, OrderData>();
    private int active_arrows = 0;

    public Transform player;
    public float order_release_scale = 4f;
    public float order_release_root = 1.5f;
    public float order_time_scale = 5f;
    public float order_time_root = 1.8f;
    public float order_time_offset = 32f;
    public location_manager locations;
    public OrderableData[] order_repository;
    public int failed_orders = 0;
    public int passed_orders = 0;

    void Start() {
        foreach (Transform child in transform) {
            ExpireTime timer = child.gameObject.GetComponentInChildren<ExpireTime>(true);
            ArrowControl ctl = child.gameObject.GetComponentInChildren<ArrowControl>(true);
            if (timer == null || ctl == null) {
                Debug.LogError("arrow is missing either ExpireTime or ArrowControl");
                continue;
            }
            int id = arrows.Count;
            unused_arrows.Add(id);
            Arrow arrow = new Arrow(timer,ctl,child.gameObject, () => {
                Debug.Log("order failed");
                failed(id);
            },id);
            arrow.unset();
            arrows.Add(arrow);
        }
        if (unused_arrows.Count == 0) {
            Debug.LogError("no arrows");
            return;
        }
        release_arrow();
    }

    [ContextMenu("release arrow")]
    public void release_arrow() {
        if (unused_arrows.Count > 0) {
            free(unused_arrows[0]);
            arrows[unused_arrows[0]].host.SetActive(true);
            arrows[unused_arrows[0]].timer.reset(1000000000000f);
            unused_arrows.RemoveAt(0);
            active_arrows+=1;
        }
    }

    void Update() {
        while (active_arrows < arrows_at_order(passed_orders)) release_arrow();

        foreach (Arrow arrow in arrows) {
            OrderData data;
            if (active_orders.TryGetValue(arrow.id, out data)) {
                if (!data.has_been_picked_up) {
                    Transform where = data.pickup.action_location;
                    if (Vector3.Distance(where.position, player.position) <= data.pickup.activation_range) {
                        // arrow.set_time(data.deliver_time);

                        //NONTS: this is where you would change arrow pointing location to where.position
                        data.has_been_picked_up = true;
                        Debug.Log("order picked up");
                    }
                } else {
                    Transform where = data.delivery.action_location;
                    if (Vector3.Distance(where.position, player.position) <= data.delivery.activation_range) {
                        Debug.Log("order passed");
                        passed(arrow.id);
                    }
                }
            }
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
            //NONTS: this is where you would change arrow pointing location to provider.action_location.position
            arrows[arrow_id].set(order_repository[orderable_id].sprite, time_at_order(passed_orders)*2f);
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

    public void passed(int id) {
        free(id);
        passed_orders += 1;
    }

    public int arrows_at_order(int order) {
        return Math.Max((int) MathF.Ceiling(MathF.Pow(((float) order)/order_release_scale, 1f/order_release_root)),1); 
    }

    public float time_at_order(int order) {
        return order_time_offset-MathF.Pow(((float)order)/order_time_scale,1f/order_time_root);
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
        // public float deliver_time;

        public OrderData(
            int order_type_id,
            location pickup,
            location delivery,
            // float deliver_time,
            bool has_been_picked_up = false
        ) {
            this.order_type_id = order_type_id;
            this.pickup = pickup;
            this.delivery = delivery;
            this.has_been_picked_up = has_been_picked_up;
            // this.deliver_time = deliver_time;
        }
    }
}

public class Arrow {
    public ExpireTime timer;
    public ArrowControl ctl;
    public GameObject host;
    public int id;

    public Arrow(
        ExpireTime timer, 
        ArrowControl ctl, 
        GameObject host, 
        Action callback,
        int id
    ) {
        this.timer = timer;
        this.ctl = ctl;
        this.host = host;
        this.id = id;

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

    public void set_time(float until) {
        host.SetActive(true);
        timer.reset(until);
    }
}

public enum OrderType {
    Pizza, Sandwich, StirfryNoodles, Soup
}