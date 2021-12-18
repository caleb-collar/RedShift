using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Scoller | Caleb A. Collar | 10.7.21
public class Scroll : MonoBehaviour
{
    [SerializeField] float scrollSpeed;
    
    private Renderer renderer;
    private Vector2 savedOffset;

    void Start () {
        renderer = GetComponent<Renderer> ();
    }

    void Update () {
    float y = Mathf.Repeat (Time.time * scrollSpeed, 1);
    Vector2 offset = new Vector2 (0, y);
    renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
