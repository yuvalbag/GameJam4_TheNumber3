using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    public float _horizontalMove = 0f;
    public float mPlayerSpeed = 4f;
    public float maxMagnitude = 1f;
    public float jumpPower = 100f;
    private Vector2 leftRotation = new Vector2(-2.5f,2.5f);
    private Vector2 rightRotation = new Vector2(2.5f,2.5f);
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_animator.GetBool("isJumping") && Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetBool("isJumping", true);
            _rigidbody.AddForce(Vector2.up * jumpPower);
            StartCoroutine(land());
        }
    }

    IEnumerator land()
    {
        yield return new WaitForSeconds(1f);
        _animator.SetBool("isJumping", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _horizontalMove = Input.GetAxis("Horizontal") * mPlayerSpeed;
        _animator.SetFloat("speed", Mathf.Abs(_horizontalMove));

        if (Mathf.Abs(_horizontalMove) <= 0.01f && _rigidbody.velocity.magnitude <= 0.3f)
        {
            //_rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(Vector2.left * mPlayerSpeed);
        }
        else if (_horizontalMove < -0.1f && _rigidbody.velocity.magnitude <= maxMagnitude)
        {
            transform.localScale = leftRotation;
            _rigidbody.AddForce(Vector2.left * mPlayerSpeed);
        }
        else if (_horizontalMove > 0.1f && _rigidbody.velocity.magnitude <= maxMagnitude)
        {
            transform.localScale = rightRotation;
            _rigidbody.AddForce(Vector2.right * mPlayerSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //TODO NEED TO MOVE TO PLAYER LOGIC !!
        if (other.transform.CompareTag("leftBound"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
