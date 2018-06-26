using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public GameObject projectilePrefab;
	public float speed;
	public float padding;
	public float health = 300;
	public float projectileSpeed;
	public float firingRate;
	float xmin;
	float xmax;

	public AudioClip fireSound;


	void Start ()
	{	
		float distance = transform.position.z - Camera.main.transform.position.z;
		
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.LeftArrow)) {
			//this.transform.position -= new Vector3 (speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			//transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		
		//Restrict the player to the gamespace
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);	
		
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		} 
		if (Input.GetKeyUp(KeyCode.Space))
		{
			CancelInvoke("Fire");
		}
		
	}
	
	void Fire ()
	{
		Vector3 offset = new Vector3 (0f,0.7f,0);
		GameObject projectile = Instantiate (projectilePrefab, transform.position + offset, Quaternion.identity) as GameObject;
		projectile.rigidbody2D.velocity = new Vector3 (0f, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile){
			health -= missile.getDamage();
			missile.Hit ();
			if (health <= 0) {
				Destroy(gameObject);
				Application.LoadLevel ("Win Screen");
			}
		}
	}
	
}
