using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public float moveSpeed;
	public Rigidbody2D theRB;
	public float junpForce;

	private bool isGrounded;
	public Transform groundCheckPoint;
	public LayerMask whatIsGround;

	private bool canDoubleJump;

	private Animator anim;
	private SpriteRenderer theSR;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
		theSR = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		theRB.velocity = new Vector2 (moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

		isGrounded = Physics2D.OverlapCircle (groundCheckPoint.position, .2f, whatIsGround);

		if (isGrounded) 
		{
			canDoubleJump = true;
		}

		if (Input.GetButtonDown ("Jump")) 
		{
			if (isGrounded) 
			{
				theRB.velocity = new Vector2 (theRB.velocity.x, junpForce);
			} 
			else 
			{
				if (canDoubleJump) 
				{
					theRB.velocity = new Vector2 (theRB.velocity.x, junpForce);
					canDoubleJump = false;
				}
			}
		}

		if (theRB.velocity.x < 0) 
		{
			theSR.flipX = true;
		} else if (theRB.velocity.x > 0)
		{
			theSR.flipX = false;
		}

		anim.SetFloat ("moveSpeed", Mathf.Abs( theRB.velocity.x));
		anim.SetBool ("isGrounded", isGrounded);
	}
}
