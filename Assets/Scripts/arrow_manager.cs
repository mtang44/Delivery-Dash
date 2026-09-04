using UnityEngine;
using System.Collections.Generic;
using System;

//TODO: order creation
//TODO: order quantity increasing
//TODO: order fufillment
//TODO: arrow state machine for pickup then delivery

public class arrow_manager : MonoBehaviour {
    private List<Arrow> arrows = new List<Arrow>();
    private List<int> free_arrows = new List<int>();
    private List<int> unused_arrows = new List<int>();

    public OrderableData[] order_repository;

    void Start() {
        foreach (Transform child in transform) {
            ExpireTime timer = child.gameObject.GetComponent<ExpireTime>();
            ArrowControl ctl = child.gameObject.GetComponent<ArrowControl>();
            if (timer == null || ctl == null) {
                Debug.LogError("arrow is missing either ExpireTime or ArrowControl");
                continue;
            }
            timer.reset_paused();
            unused_arrows.Add(arrows.Count);
            Arrow arrow = new Arrow(timer,ctl,child.gameObject, this, arrows.Count);
            arrow.hide();
            arrows.Add(arrow);
        }
        if (unused_arrows.Count == 0) {
            Debug.LogError("no arrows");
            return;
        }
        free_arrows.Add(unused_arrows[0]);
        unused_arrows.RemoveAt(0);
    }

    void Update() {
        foreach (Arrow arrow in arrows) {
            arrow.update();
        }
        // if free arrows, assign pickups to them
    }

    public void free(int id) {
        if (free_arrows.Contains(id)) {
            Debug.LogError("arrow already freed");
            return;
        }
        free_arrows.Add(id);
        arrows[id].hide();
    }

    [System.Serializable]
    public class OrderableData {
        public OrderType typ;
        public Sprite sprite;
    }
}

public class Arrow {
    public ExpireTime timer;
    public ArrowControl ctl;
    public GameObject host;
    public arrow_manager manager;
    public int id;

    public Arrow(
        ExpireTime timer, 
        ArrowControl ctl, 
        GameObject host, 
        arrow_manager manager, 
        int id
    ) {
        this.timer = timer;
        this.ctl = ctl;
        this.host = host;
        this.manager = manager;
        this.id = id;

        this.timer.use_callback = true;
        this.timer.callback = () => {
            this.manager.free(this.id);
        };
    }

    public void update() {
        ctl.progress = timer.left_unit();
        // this is where screen orientation code could go???
    }

    public void hide() {
        // host.GetComponent<Renderer>().enabled = false;
        host.SetActive(false);
        timer.reset_paused();
    }

    public void show() {
        host.SetActive(true);
    }
}

public enum OrderType {
    Pizza, Sandwich, StirfryNoodles, Soup
}