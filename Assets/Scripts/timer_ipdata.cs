using UnityEngine;
using TMPro;

public class timer_ipdata : MonoBehaviour {
    public TextMeshProUGUI mesh;
    public watchtime time;
    
    void Start() {
        mesh = GetComponent<TextMeshProUGUI>();
        time = GetComponent<watchtime>();
    }

    void Update() {
        mesh.text = time.formated_time();
    }
}
