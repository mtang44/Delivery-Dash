using UnityEngine;
using System;

public class ExpireTime : MonoBehaviour {
    public float default_time = 25f;
    public bool use_callback = true;

    float reset_time = 0f;
    float paused_time = 0f;
    float paused_at_time = 0f;
    float left_time = 0f;
    bool paused = false;

    public Action callback = () => {
        Debug.Log("callback action not set on expiretime");
    };

    /// resets the countdown
    [ContextMenu("reset")]
    public void reset() {reset(default_time);}
    [ContextMenu("reset paused")]
    public void reset_paused() {reset_paused(default_time);}

    public void reset(float left) {
        left_time = left;
        reset_time = Time.realtimeSinceStartup;
        paused_at_time = reset_time;
        paused_time = 0f;
        paused = false;
    }

    public void reset_paused(float left) {
        left_time = left;
        reset_time = Time.realtimeSinceStartup;
        paused_at_time = reset_time;
        paused_time = 0f;
        paused = true;
    }

    /// calls callback while the timer is up
    public void partial_update() {
        if (paused || !use_callback) return; 
        if (Time.realtimeSinceStartup-reset_time-paused_time >= left_time) {
            callback();
        }
    }

    /// pauses
    [ContextMenu("pause")]
    public void pause() {
        if (paused) unpause();
        paused = true;
        paused_at_time = Time.realtimeSinceStartup;
    }

    /// resumes
    [ContextMenu("unpause")]
    public void unpause() {
        if (!paused) pause();
        paused = false;
        paused_time += Time.realtimeSinceStartup-paused_at_time;
        partial_update();
    }

    /// time in seconds that have passed since the timer started
    public float passed() {
        return Time.realtimeSinceStartup-reset_time-paused_time - (paused ? Time.realtimeSinceStartup-paused_at_time : 0f);
    }

    /// time in seconds until the timer is up
    public float left() {
        return left_time-passed();
    }

    /// time as a scale of (0,1) equaling 0 when the timer is up
    public float left_unit() {
        if (left() == 0f) return 0f;
        return left()/left_time;
    }

    /// time as a scale of (0,1) equaling 1 when the timer is up
    public float passed_unit() {
        return 1f-left_unit();
    }

    void Start() {
        reset();
    }

    void Update() {
        partial_update();

        // Debug.Log(""+left()+":"+left_unit());
    }
}
