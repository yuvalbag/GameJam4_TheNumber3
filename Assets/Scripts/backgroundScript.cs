using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScript : MonoBehaviour
{
    public float speed = 0.5f;
    private Renderer _renderer;
    public playerMovement player;
    public float offsetIndex = 1000f;
    public float count = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player._horizontalMove > 0.1f)
        {
            Vector2 offset = new Vector2(count * speed, 0);
            _renderer.material.mainTextureOffset = offset;
            count = (count+1) % offsetIndex;
        }
        else
        {
            //camera goes to player ??
        }

    }
}
