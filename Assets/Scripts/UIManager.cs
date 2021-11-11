using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Slider drownSlider, waterSlider; // u?a??n bo?ulmas? fazla suya dalmas?
    public GameObject TapToStartPanel, LoosePanel, GamePanel, WinPanel;
    public Text soundButtonText, levelNoText, scoreText, gemsText,
    totalScoreTextStartPanel, totalGemsTextStartPanel, totalScoreTextGamePanel, totalGemsTextGamePanel;

    [SerializeField] private List<GameObject> _compliments = new List<GameObject>();

    [SerializeField] private GameObject _tapAndHoldText;

    //[SerializeField] private List<GameObject> _xObjects = new List<GameObject>();

    private int _complimentNumber;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        StartUI();
        totalScoreTextStartPanel.text = PlayerPrefs.GetInt("totalscore").ToString();
        totalGemsTextStartPanel.text = PlayerPrefs.GetInt("totalgems").ToString();
        totalGemsTextGamePanel.text = PlayerPrefs.GetInt("totalgems").ToString();
        totalScoreTextGamePanel.text = PlayerPrefs.GetInt("totalscore").ToString();


    }

    public void StartUI()
    {
        TapToStartPanel.SetActive(true);
        LoosePanel.SetActive(false);
        GamePanel.SetActive(false);
        _tapAndHoldText.SetActive(false);
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
        _tapAndHoldText.SetActive(true);
        Invoke("TapAndHoldKapat", 3f);
    }

    private void TapAndHoldKapat()
    {
        _tapAndHoldText.SetActive(false);
    }

    // RESTART TU?UNA BASILDI?INDA  --- LOOSE EKRANINDA
    public void RestartButtonClick()
    {
        TapToStartPanel.SetActive(true);
        LoosePanel.SetActive(false);
        PlaneController.instance.StartPanelEvents();
        LevelController.instance.RestartLevelEvents();

        //_compliments[_complimentNumber].SetActive(false);
    }


    // NEXT LEVEL TU?UNA BASILDI?INDA  --- W?N EKRANINDA
    public void NextLevelButtonClick()
    {
        TapToStartPanel.SetActive(true);
        WinPanel.SetActive(false);
        GamePanel.SetActive(false);
        PlaneController.instance.StartPanelEvents();
        LevelController.instance.NextLevelEvents();

        _compliments[_complimentNumber].SetActive(false);
    }

    public void SetScoreText()
    {
        scoreText.text = GameManager.instance.score.ToString();
    }

    public void SetGemsText()
    {
        gemsText.text = GameManager.instance.gems.ToString();
    }

    public void SetTotalScoreText()
    {
        totalScoreTextStartPanel.text = PlayerPrefs.GetInt("totalscore").ToString();
        totalScoreTextGamePanel.text = PlayerPrefs.GetInt("totalscore").ToString();
    }

    public void SetTotalGemsText()
    {
        totalGemsTextStartPanel.text = PlayerPrefs.GetInt("totalgems").ToString();
        totalGemsTextGamePanel.text = PlayerPrefs.GetInt("totalgems").ToString();
    }

    public void SetCompliments(int deger)
    {
        _complimentNumber = deger;
        _compliments[_complimentNumber].SetActive(true);
    }


}
