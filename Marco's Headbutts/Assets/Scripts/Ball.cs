using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	[SerializeField] Paddle paddle;
	[SerializeField] float xPush = 2f;
	[SerializeField] float yPush = 15f;
	[SerializeField] AudioClip[] ballSounds;
	[SerializeField] float randomFactor = 0.2f;

	Vector2 paddleToBallVector;
	bool hasStarted = false;
	// Use this for initialization
	AudioSource myAudioSource;
	Rigidbody2D myRigidBody2D;

	void Start () {
		paddleToBallVector = transform.position - paddle.transform.position;
		myAudioSource = GetComponent<AudioSource>();
		myRigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasStarted){
		    LockBallToPaddle();
			LaunchOnMouseClick();
		}
	}

	private void LaunchOnMouseClick()
	{
		if (Input.GetMouseButtonDown(0)){
			hasStarted = true;
			myRigidBody2D.velocity = new Vector2(xPush,yPush);
		}
	}
	private void LockBallToPaddle(){
		Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
		transform.position = paddlePos + paddleToBallVector;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));

		if (hasStarted)
		{
			AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
			myAudioSource.PlayOneShot(clip);
			myRigidBody2D.velocity += velocityTweak;
		}
	}
}
