using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;
	public Slider drownSlider, waterSlider; // u?a??n bo?ulmas? fazla suya dalmas?
	public GameObject TapToStartPanel,LoosePanel,GamePanel,WinPanel;
	public Text soundButtonText, levelNoText,scoreText,gemsText, 
	totalScoreTextStartPanel, totalGemsTextStartPanel, totalScoreTextGamePanel,totalGemsTextGamePanel;


	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}

	private void Start()
	{
		StartUI();
		totalScoreTextStartPanel.text =PlayerPrefs.GetInt("totalscore").ToString();
		totalGemsTextStartPanel.text =PlayerPrefs.GetInt("totalgems").ToString();
		totalGemsTextGamePanel.text =PlayerPrefs.GetInt("totalgems").ToString();
		totalScoreTextGamePanel.text =PlayerPrefs.GetInt("totalscore").ToString();
	}

	public void StartUI()
	{
		TapToStartPanel.SetActive(true);
		LoosePanel.SetActive(false);
		GamePanel.SetActive(false);
	}

	public void SetLevelText(int levelNo)
	{
		levelNoText.text = "Level " + levelNo.ToString();
	}

	// TAPTOSTART TU?UNA BASILDI?INDA  --- G?R?? EKRANINDA VE LEVEL BA?LARINDA
	public void TapToStartButtonClick()
	{
		PlaneController.instance.PlaneStartingEvents();
		TapToStartPanel.SetActive(false);
		GamePanel.SetActive(true);
		PlaneController.instance.bosaltmaNoktasi = GameObject.FindGameObjectWithTag("bosalt");
		PlaneController.instance.maxAltitude = GameObject.FindGameObjectWithTag("maxyuk").transform.position.y;
	}

	// RESTART TU?UNA BASILDI?INDA  --- LOOSE EKRANINDA
	public void RestartButtonClick()
	{
		TapToStartPanel.SetActive(true);
		LoosePanel.SetActive(false);
		PlaneController.instance.StartPanelEvents();
		LevelController.instance.RestartLevelEvents();
	}


	// NEXT LEVEL TU?UNA BASILDI?INDA  --- W?N EKRANINDA
	public void NextLevelButtonClick()
	{		
		TapToStartPanel.SetActive(true);
		WinPanel.SetActive(false);
		GamePanel.SetActive(false);
		PlaneController.instance.StartPanelEvents();
		LevelController.instance.NextLevelEvents();
	}

	public void SetScoreText()
	{
		scoreText.text =GameManager.instance.score.ToString();
	}

	public void SetGemsText()
	{
		gemsText.text =GameManager.instance.gems.ToString();
	}

	public void SetTotalScoreText()
	{
		totalScoreTextStartPanel.text = PlayerPrefs.GetInt("totalscore").ToString();
		totalScoreTextGamePanel.text = PlayerPrefs.GetInt("totalscore").ToString();
	}

	public void SetTotalGemsText()
	{
		totalGemsTextStartPanel.text =PlayerPrefs.GetInt("totalgems").ToString();
		totalGemsTextGamePanel.text = PlayerPrefs.GetInt("totalgems").ToString();
	}


}
