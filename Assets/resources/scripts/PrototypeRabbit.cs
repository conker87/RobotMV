using UnityEngine;
using System.Collections;

public class PrototypeRabbit : MonoBehaviour
{
	public float defaultVelocity = 3f;
	public float currentVelocity;
	public float previousVelocity = 3f;
	public Transform Top;
	public Transform Bottom;

	public bool colliding;

	public LayerMask detectWhat;

	private Animator anim;

	public Random random = new Random();
	public int rand;
	public int defaultTimesToIgnoreFlip;
	public int timesToIgnoreFlip;

	void Start()
	{
		anim = GetComponent<Animator>();
		
		currentVelocity = defaultVelocity;
		timesToIgnoreFlip = defaultTimesToIgnoreFlip;
	}

	void FixedUpdate()
	{
		rand = Random.Range (0, 10000); // 0.01%
	}

	void OnMouseDown()
	{
		Debug.Log ("Clicked on: " + this.name);
	}

	void Update()
	{
		colliding = Physics2D.Linecast(Top.position, Bottom.position, detectWhat);

		if (colliding)
		{
			Flip ();

			if (timesToIgnoreFlip < defaultTimesToIgnoreFlip / 2)
			{
				timesToIgnoreFlip = defaultTimesToIgnoreFlip / 2;
			}
		}
		else
		{
			if (rand > 0 & rand <= 1000)
			{
				timesToIgnoreFlip--;

				if (timesToIgnoreFlip < 1)
				{
					Flip ();

					//Debug.Log ("Randomly flipped!");

					timesToIgnoreFlip = defaultTimesToIgnoreFlip;
				}
			}
		}
		
		GetComponent<Rigidbody2D>().velocity = new Vector2(currentVelocity, GetComponent<Rigidbody2D>().velocity.y);

		anim.SetFloat("Speed", Mathf.Abs(currentVelocity));
	}

	void Flip()
	{
		transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
		
		currentVelocity *= -1;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;

		Gizmos.DrawLine(Top.position, Bottom.position);
	}

	// Collision between Rigidbodies?
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Debug.Log ("Collided with player");
		}
	}
}