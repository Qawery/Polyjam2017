using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlaTest : MonoBehaviour {

	private bool hasSpawn;

	//  Component references
	private Animator animator;
	private SpriteRenderer[] renderers;

	private int triggerUsed = 0;

	void Awake()
	{

		// Get the animator
		animator = GetComponent<Animator>();

		// Get the renderers in children
		renderers = GetComponentsInChildren<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetBool("isWalking", true);
		animator.SetBool("isActive", true);
		if (triggerUsed % 9 == 0) {
			animator.SetTrigger("shoot");
		}
		++triggerUsed;
	}
}
