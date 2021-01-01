using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
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
        transform.position = new Vector3(_horizontalMove,-3.63f);
    }
}
