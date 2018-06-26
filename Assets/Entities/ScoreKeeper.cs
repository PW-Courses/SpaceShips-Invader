using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public Text text;
	public int score = 0;

	void Start()
	{
		Reset();
	}

	public void AddScore(int points)
	{
		score += points;

		if (score > 2000)
		{
			Application.LoadLevel ("Win Screen");
		}

		text.text = score.ToString ();
	}


	public void Reset()
	{
		score = 0;
		text.text = score.ToString ();
	}
}
