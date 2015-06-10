using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour {
	public float maxSpeed = 10f;
	public float jumpForce = 700f;
	bool facingRight = true;
	bool grounded = false;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float score;
	public float move;
	public float spawnX, spawnY;
	private GameObject star;
	// Use this for initialization
	void Start () {
		spawnX = transform.position.x;
		spawnY = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		
		move = Input.GetAxis ("Horizontal");
		
	}
	
	void Update(){
		if (grounded && (Input.GetKeyDown (KeyCode.W)||Input.GetKeyDown (KeyCode.UpArrow))) {
			
			GetComponent<Rigidbody2D>().AddForce (new Vector2(0f,jumpForce));
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		
		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
		
		
		
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
		
		if (Input.GetKey(KeyCode.R))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		
		
	}
	
	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	void OnTriggerEnter2D(Collider2D col){
		//if ((col.gameObject.name == "dieCollider")||(col.gameObject.name == "saw"))
			//Application.LoadLevel (Application.loadedLevel);
		
		if (col.gameObject.name == "star") {
			score++;
			Destroy (col.gameObject);
		}
		
		if (col.gameObject.name == "endLevel") {
			if (!(GameObject.Find ("star")))
				Application.LoadLevel ("scene2");
		}

		if (col.gameObject.name == "endLevel2") {
			if (!(GameObject.Find("star"))) Application.LoadLevel ("scene3");
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.name == "dieCollider" || col.gameObject.name == "saw")
			transform.position = new Vector3(spawnX,spawnY,transform.position.z);
		}
	
	void OnGUI(){
		GUI.Box (new Rect (0, 0, 100, 100), "Stars: " + score);
	}
	
}
