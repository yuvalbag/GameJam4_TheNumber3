using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    [Header("Block Settings")]
    public float movementSpeed = 2f;
    public float movementDirection = 0.1f; //added if we want to flip it at sometimes?

    [Header("Block Data")]
    public int blockState; //expected power\state player is using to pass this block

    private Vector2 left; //location of left edge of screen
    private Vector2 right; //location of right edge of screen

    // Start is called before the first frame update
    void Start()
    {
        left = Camera.main.ViewportToWorldPoint(new Vector2(0.0f, 0.0f)); //location of left edge of screen
        right = Camera.main.ViewportToWorldPoint(new Vector2(1.0f, 0.0f)); //location of right edge of screen
        string tag = gameObject.tag;
        Debug.Log("id is " + tag.Substring(tag.Length - 1));
        blockState = int.Parse(tag.Substring(tag.Length - 1));
    }

    // Update is called once per frame
    void Update()
    {
        updateLocation(Time.deltaTime);
    }

    void updateLocation(float deltaTime)
    {
        Vector2 new_location = new Vector2(transform.position.x - movementSpeed * movementDirection * deltaTime, transform.position.y);
        transform.position = new_location;

        //check out of bounds relative to camera, and cycle
        Vector2 locationOfRightEdge = new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.y);
        Vector3 pos = Camera.main.WorldToViewportPoint(locationOfRightEdge);
        if (pos.x < 0.0f) {
            Debug.Log("I am left of the camera's view.");
            //cycle to right part of screen
            transform.position = new Vector2(right.x, transform.position.y);
        };
    }
}
