// Filename: CoinController.cs
// Author: Brahm Ramkissoon
// Created Date  (dd/mm/yyyy): 26/10/2015
// Description: Make coin dissappear when coin is in contact with player
using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Make coin disappear when coin is in contact with player
    void OnCollisionEnter2D ( Collision2D otherCollider )
    {
        if ( otherCollider.gameObject.CompareTag("Player") )
        {
            Destroy(gameObject);
        }
    }
}
