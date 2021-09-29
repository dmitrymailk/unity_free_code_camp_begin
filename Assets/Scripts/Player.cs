using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float jupmForce = 10f;

    private float movementX;
    private Rigidbody2D myBody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private string WALK_ANIMATION = "Walk";
    private bool isGrounded = true;

    private string GROUND_TAG = "Ground";
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    // void Update()
    // {
    // }

    private void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        Debug.Log(movementX);
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer()
    {
        if (movementX > 0)
        {
            _animator.SetBool(WALK_ANIMATION, true);
            _spriteRenderer.flipX = false;
        } else if (movementX < 0)
        {
            _animator.SetBool(WALK_ANIMATION, true);
            _spriteRenderer.flipX = true;
        }
        else if(movementX == 0)
        {
            _animator.SetBool(WALK_ANIMATION, false);
        }
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = !isGrounded;
            myBody.AddForce(new Vector2(0f, jupmForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }
    }
}
