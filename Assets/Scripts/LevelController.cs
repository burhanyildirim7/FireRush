using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElephantSDK;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
	public int levelNo;
	public List<GameObject> levels = new List<GameObject>();
	private GameObject currentLevelObj;

	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this.gameObject);
	}

	private void Start()
	{
		//PlayerPrefs.DeleteAll();
		levelNo = PlayerPrefs.GetInt("level");
		if (levelNo == 0) levelNo = 1;
		UIManager.instance.SetLevelText(levelNo);
		LevelStartingEvents();
	}

	public void IncreaseLevelNo()
	{
		levelNo++;
		PlayerPrefs.SetInt("level", levelNo);
		UIManager.instance.SetLevelText(levelNo);
	}

	// Bu fonksiyon oyun ilk a??ld???nda ?a?r?lacak..
	public void LevelStartingEvents()
	{
		currentLevelObj = Instantiate(levels[levelNo - 1], Vector3.zero, Quaternion.identity);
		Elephant.LevelStarted(levelNo);
	}

	// next level tu?una bas?ld???nda UIManager scriptinden ?a?r?lacak..
	public void NextLevelEvents()
	{
		Elephant.LevelCompleted(levelNo);
		Destroy(currentLevelObj);
		IncreaseLevelNo();
		LevelStartingEvents();
	}

	// restart level tu?una bas?ld???nda UIManager scriptinden ?a?r?lacak..
	public void RestartLevelEvents()
	{
		Elephant.LevelFailed(levelNo);
		// DEAKT?F ED?LEN OBSTACLELARIN TEKRAR A?ILMASI ???N..
		GameObject[] obstacles;
		obstacles = GameObject.FindGameObjectsWithTag("obstacle");
		for (int i = 0; i < obstacles.Length; i++)
		{
			obstacles[i].GetComponent<MeshRenderer>().enabled = true;
		}
		GameObject[] collectibles;
		collectibles = GameObject.FindGameObjectsWithTag("collectible");
		for (int i = 0; i < collectibles.Length; i++)
		{
			collectibles[i].GetComponent<MeshRenderer>().enabled = true;
		}
	}
}
