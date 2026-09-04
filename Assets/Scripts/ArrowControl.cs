using UnityEngine;

public class ArrowControl : MonoBehaviour {
    public float progress = 0f;
    public float rotation = 0f;
    public SpriteRenderer image;

    MeshRenderer rend;
    MaterialPropertyBlock block;

    void Start() {
        if (image == null) image = GetComponentInChildren<SpriteRenderer>();
    }

    void set_image(Sprite img) {
        image.sprite = img;
    }

    void Update() {
        if (rend == null) rend = GetComponent<MeshRenderer>();
        if (block == null) block = new MaterialPropertyBlock();

        rend.SetPropertyBlock(block);
        block.SetFloat("_progress", progress);
        block.SetFloat("_rotation", rotation);
    }
}
