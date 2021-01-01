using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class player_or: MonoBehaviour
{
    [Header("Key Bindings")]
    public KeyCode state0 = KeyCode.Q;
    public KeyCode state1 = KeyCode.W;
    public KeyCode state2 = KeyCode.E;

    [Header("Player Data")]
    public int state = 0;

    private Animator _animator;
    public float _horizontalMove = 0f;
    public float mPlayerSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_animator.GetBool("isJumping") && Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetBool("isJumping", true);
            transform.DOJump(transform.position, 2, 1, 0.5f);
            StartCoroutine(land());
        }
        //change states (temp code - unscaleable. make it into an array later
        if (Input.GetKeyDown(state0))
        {
            state = 0;
        }
        if (Input.GetKeyDown(state1))
        {
            state = 1;
        }
        if (Input.GetKeyDown(state2))
        {
            state = 2;
        }

        
    }

    IEnumerator land()
    {
        yield return new WaitForSeconds(0.5f);

        _animator.SetBool("isJumping", false);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _horizontalMove = Input.GetAxis("Horizontal") * mPlayerSpeed;
        _animator.SetFloat("speed", Mathf.Abs(_horizontalMove));
        transform.position = new Vector3(_horizontalMove, -3.63f);

        if (Mathf.Abs(_horizontalMove) <= 0.01f)
        {
            transform.position = new Vector3(_horizontalMove, -3.63f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided, player");
        if (collision.gameObject.tag.StartsWith("block"))
        { //state block collided
            BlockLogic script  = collision.GetComponent < BlockLogic > ();
            Debug.Log("collision detected. other's id = " + script.blockState);
            if(state != script.blockState)
            {
                die();
            }
        }

    }

    private void die()
    {
        //NEED TO CHANGE THIS FUNCTION
        Debug.Log("Death");
        Time.timeScale = 0; //freeze
    }
}
