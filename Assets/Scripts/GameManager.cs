using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int gems, totalGems, score, totalScore;



    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public GameObject FindLastFireObject()
    {
        //int indis = GameObject.Find("Atesler").transform.childCount;
        //PlaneController.instance.lastFireObject = GameObject.Find("Atesler").transform.GetChild(GameObject.Find("Atesler").transform.childCount - 1).gameObject;
        return GameObject.Find("Atesler").transform.GetChild(GameObject.Find("Atesler").transform.childCount - 1).gameObject;
    }

    public void increaseScore(int _score)
    {
        score += _score;
        totalScore += _score;
        PlayerPrefs.SetInt("totalscore", totalScore);
        UIManager.instance.SetTotalScoreText();
    }

    public void increaseGems()
    {
        totalGems = PlayerPrefs.GetInt("totalgems");
        gems++;
        totalGems++;
        PlayerPrefs.SetInt("totalgems", totalGems);
        UIManager.instance.SetTotalGemsText();
    }


}
