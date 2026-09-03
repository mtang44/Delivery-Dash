using UnityEngine;

public class arrow_material_control : MonoBehaviour {
    public float progress = 0f;
    public float rotation = 0f;

    MeshRenderer rend;
    MaterialPropertyBlock block;

    void Start() {}

    void Update() {
        if (rend == null) rend = GetComponent<MeshRenderer>();
        if (block == null) block = new MaterialPropertyBlock();

        block.SetFloat("_progress", progress);
        block.SetFloat("_rotation", rotation);
        rend.SetPropertyBlock(block);
    }
}
