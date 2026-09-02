using UnityEngine;

public class watchtime : MonoBehaviour {
    /// number of frames since last reset
    public int frame_time = 0;
    /// real time since last reset
    public float real_time = 0f;
    /// real time since last reset with respect to pauses
    public float partial_time = 0f;

    /// public interfacing
    public bool is_paused = false;

    [Header("Formatting")]
    public bool show_millis = false;
    public bool force_mins = false;
    public bool force_hours = false;
    
    /// is the game paused
    private bool paused = false;
    private float reset_time = 0f;
    private float paused_time = 0f;
    private float paused_at_time = 0f;

    /// resets the watch
    [ContextMenu("reset")]
    void reset() {
        frame_time = 0;
        real_time = 0f;
        partial_time = 0f;
        paused = false;
        is_paused = paused;
        paused_time = 0f;
        paused_at_time = Time.realtimeSinceStartup;
        reset_time = Time.realtimeSinceStartup;
    }

    /// pauses partial_time
    [ContextMenu("pause")]
    void pause() {
        if (paused) unpause();
        paused = true;
        paused_at_time = Time.realtimeSinceStartup;
    }

    /// resumes partial_time
    [ContextMenu("unpause")]
    void unpause() {
        if (!paused) pause();
        paused = false;
        paused_time += Time.realtimeSinceStartup-paused_at_time;
        partial_update();
    }

    /// updates all times except for frame_time, with respect to pause state
    void partial_update() {
        real_time = Time.realtimeSinceStartup-reset_time;
        is_paused = paused;
        if (!paused) {
            partial_time = Time.realtimeSinceStartup-reset_time-paused_time;
        }
    }

    int partial_millis() {
        return (int) ((partial_time*1000f)%1000f);
    }

    int partial_secs() {
        return (int) ((partial_time)%60f);
    }
    
    int partial_mins() {
        return (int) ((partial_time/60f)%60f);
    }
    
    int partial_hours() {
        return (int) ((partial_time/60f/60f)%60f);
    }

    string formated_time() {
        string temp = show_millis ? "."+partial_millis() : "";
        temp = partial_secs() + temp;
        int mins = partial_mins();
        int hrs = partial_hours();
        if (mins > 0 || hrs > 0 || force_hours || force_mins) {
            temp = mins+":"+temp;
            if (hrs > 0 || force_hours) {
                temp = hrs+":"+temp;
            }
        }
        return temp;
    }

    void Start() {reset();}

    void Update() {
        frame_time+=1;
        partial_update();
    }
}
