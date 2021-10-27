using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public void DeactivateCMVcam()
	{
        GetComponent<CinemachineBrain>().enabled = false;
	}

    public void ActivateCMVcam()
    {
        GetComponent<CinemachineBrain>().enabled = true;
    }
}
