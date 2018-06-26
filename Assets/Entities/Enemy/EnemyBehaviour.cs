using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public GameObject projectilePrefab;
	public float health = 150;
	public float projectileSpeed = -3f;
	public float shotsPerSecond = 0.5f;
	private int points = 150;
	private ScoreKeeper scoreKeeper;

	public AudioClip fireSound;
	public AudioClip deathSound;
		
	void Start()
	{
		scoreKeeper = FindObjectOfType<ScoreKeeper>();
	}


	void OnTriggerEnter2D(Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile){
			health -= missile.getDamage();
			missile.Hit ();
			if (health <= 0) {
				Die();
			}
		}
	}
	
	void Fire()
	{
		Vector3 startPosition = transform.position + new Vector3(0f,-0.4f,0);
		GameObject projectile = Instantiate (projectilePrefab, startPosition, Quaternion.identity) as GameObject;
		projectile.rigidbody2D.velocity = new Vector3 (0f, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	void Update()
	{	
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability)
		{
			Fire ();
		}
	}
	
	void Die()
	{
		AudioSource.PlayClipAtPoint(deathSound,transform.position);
		Destroy(gameObject);
		//could be also FindObjectOfType<ScoreKeeper>().AddScore(points); moze byc wolniejsze od przydzielenia obiektu w Start(), bo musi szukac za kazdym razem obiektu ScoreKeeper. 
		scoreKeeper.AddScore(points);
	}
	
}
