using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInitializeScript : MonoBehaviour
{
    SpriteRenderer sr;
    // Start is called before the first frame update
    public Gradient gradient;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        sr.color = gradient.Evaluate(Random.Range(0f, 1f));
    }

}
