// Filename: PlayerController.cs
// Author: Brahm Ramkissoon
// Created Date  (dd/mm/yyyy): 26/10/2015
// Description: all things controlled by player sprite
// Revision:        - added coin sound
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    // PRIVATE INSTANCE VARIABLES
    private AudioSource[] _audioSources;
    private AudioSource _coinSound;


	// Use this for initialization
	void Start ()
	{
	    this._audioSources = gameObject.GetComponents<AudioSource>();
	    this._coinSound = this._audioSources[0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Play sound on collision with coin
    void OnCollisionEnter2D ( Collision2D otherCollider )
    {
        if ( otherCollider.gameObject.CompareTag("Coin") )
        {
            this._coinSound.Play();
        }
    }
}
