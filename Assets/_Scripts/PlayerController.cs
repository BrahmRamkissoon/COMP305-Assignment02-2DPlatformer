// Filename: PlayerController.cs
// Author: Brahm Ramkissoon
// Created Date  (dd/mm/yyyy): 26/10/2015
// Description: all things controlled by player sprite
// Revision:        - added coin sound
//                  - added player movement along x and y axes, adjusted scale
//                  - added idle, walk, jump animation             


using UnityEngine;
using System.Collections;

// Velocity Range Utliity class
[System.Serializable]
public class VelocityRange
{
    public float vMin, vMax;

    public VelocityRange(float vMin, float vMax)
    {
        this.vMin = vMin;
        this.vMax = vMax;
    }
}

// Player controller class
public class PlayerController : MonoBehaviour {

    // PUBLIC INSTANCE VARIABLES
    public float speed = 50f;
    public float jump = 500f;

    // PRIVATE INSTANCE VARIABLES
    private AudioSource[] _audioSources;
    private AudioSource _coinSound;
    private AudioSource _jumpSound;
    private Rigidbody2D _rigidBody2D;
    private Transform _transform;
    private Animator _animator;             // use this later
    private float _movingValue = 0f;        // check movement 
    private bool _isFacingRight = true;
    private bool _isGrounded = true;

    // Set velocity range
    public VelocityRange velocityRange = new VelocityRange( 300f, 1000f );      

    
	// Use this for initialization
	void Start ()
	{
        // Reference to audio source
	    this._audioSources = gameObject.GetComponents<AudioSource>();
        this._coinSound = this._audioSources[0];
	    this._jumpSound = this._audioSources[1];
        
        // Reference shortcuts
        this._rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent <Animator>();
    }

    // Physics Update 
    void FixedUpdate () {

        float forceX = 0f;      // force along X axis
        float forceY = 0f;      // force along Y axis

        float absVelX = Mathf.Abs(this._rigidBody2D.velocity.x);    // velocity along X axis
        float absVelY = Mathf.Abs(this._rigidBody2D.velocity.y);    // velocity along Y axis

        this._movingValue = Input.GetAxis("Horizontal");            // Get user input, value is between -1 and 1

        // Check if player is moving
        if ( this._movingValue != 0 )
        {
            // we're moving
            this._animator.SetInteger( "AnimState", 1 );            // Play walk clip
            if ( this._movingValue > 0 )
            {   // moving right
                this._isFacingRight = true;
                this._flip();
                if ( absVelX < this.velocityRange.vMax )
                {   // change velocity along +x axis to Max
                    forceX = this.speed;
                }
            }
            else if ( this._movingValue < 0 )
            {   //moving left
                this._isFacingRight = false;
                this._flip();
                if ( absVelX < this.velocityRange.vMax )
                {   // change velocity along -x axis to Max
                    forceX = -this.speed;
                }
            }
        }
        else if ( this._movingValue == 0 )
        {   // we're idle
            this._animator.SetInteger( "AnimState", 0 );            // Play idle clip
        }

        // Check if player is jumping
        if ( Input.GetKey("up") || Input.GetKey(KeyCode.W) )
        {
            // Check if the player is grounded
            if ( this._isGrounded )
            {   // player is jumping
                this._jumpSound.Play();
                this._isGrounded = false;
                this._animator.SetInteger( "AnimState", 2 );        // Play jump anim
                if ( absVelY < this.velocityRange.vMax )
                {
                    forceY = this.jump;
                }
            }
        }

        // add force along X and Y axis depending on player input
        this._rigidBody2D.AddForce(new Vector2(forceX, forceY));
    }

    // Play sound on collision with coin
    void OnCollisionEnter2D ( Collision2D otherCollider )
    {
        if ( otherCollider.gameObject.CompareTag("Coin") )
        {
            this._coinSound.Play();
        }
    }

    // Set isGrounded true while touching ground
    void OnCollisionStay2D ( Collision2D otherCollider )
    {
        if ( otherCollider.gameObject.CompareTag("Platform") || otherCollider.gameObject.CompareTag("Boss") )
        {
            this._isGrounded = true;
        }
    }

    // PRIVATE METHODS
    private void _flip ()
    {
        if ( this._isFacingRight )
        {   // flip to face right ( normal )
            this._transform.localScale = new Vector3(1f, 1f, 1f);     
        }
        else
        {   // flip to face left
            this._transform.localScale = new Vector3(-1f, 1f, 1f);    
        }
    }
}
