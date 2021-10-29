using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public static PlaneController instance;

    public bool isFlying, isWatering, isFireFighting,isVictory,isComplete,isCrashed,isMaxHeight;
    [Header("U?u?la ilgili de?i?kenler")]
    public float flyingSpeed = 50f;
    public float wateringSpeed = .5f;
    public float fireFightingSpeed = 5f;
    public float sinkingSpeed = .5f; // suda d?z giderken batma  - ??kma h?z? olacak.
    public float risingSpeed = 120f;
    public float droppingSpeed = 60f;
    public float angleDifference = 15f;
    public float waterTakingSpeed = 0.5f;
    public float scoreSpeed = 10;
    [HideInInspector]
    public float maxAltitude;

    
    private float fireDistance,fireEndPoint,fireStartPoint; 


    private float waterDistance;  // su ile olan uzakl??? ?l?ecek suya tamamen bat?p batmamas?n? kontrol etmek i?in.. fail durumu...
    private float maxValueDrownSlider;
    private float waterAmount;

    [Header("Effektler")]
    public GameObject explodeEffect;
    public GameObject splashEffect;
    public GameObject waterEffect;
    public GameObject smokePrefab;
    public GameObject planeCrashSmoke;

    [Header("?htiya? duyulan objeler")]
    public GameObject waterDedector;
    public Animator pervane1Anim,pervane2Anim;
    [HideInInspector]
    public GameObject lastFireObject;
    private GameObject bosaltmaNoktasiObject; // mesafe hesaplamalar? i?in.
    [HideInInspector]
    public GameObject bosaltmaNoktasi; // bo?altma noktas? var?? i?in rota d?zeltme noktas?

    public GameObject tikImage;  // t?klamay? g?stermek i?in sonra silinecek...



    private void Awake()
	{
        if (instance == null) instance = this;
        else Destroy(this);
	}
	void Start()
    {
        isFlying = false;
        maxValueDrownSlider = UIManager.instance.drownSlider.maxValue;
    }

    void Update()
    {

		//if (Input.GetMouseButton(0))
		//{
  //          tikImage.SetActive(true);
		//}
		//else
		//{
  //          tikImage.SetActive(false);
		//}


        // U?U? ??LEMLER?....
		if (isFlying)
		{
            splashEffect.SetActive(false);

            if (Input.GetMouseButton(0))
            {
                if(transform.position.y < maxAltitude)
				{
                    transform.position = new Vector3(transform.position.x, transform.position.y + risingSpeed * Time.deltaTime * 10, transform.position.z + flyingSpeed * Time.deltaTime);
                    if (transform.localEulerAngles.x < 180)
                    {
                        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - angleDifference * Time.deltaTime * 10, 0, 0);
                    }
                    else if (transform.localEulerAngles.x - 360 > -16)
                    {
                        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - angleDifference * Time.deltaTime * 10, 0, 0);
                    }
				}
				else
				{
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + flyingSpeed * Time.deltaTime);
                    if (transform.localEulerAngles.x > 180) // U?A?IN BURNU YUKARDADIR.. YAN? ?N?ALLAH ?YLED?R :D
                    {
                        if (transform.localEulerAngles.x > 2 && transform.localEulerAngles.x - 360 < -2) transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + angleDifference * Time.deltaTime * 4, 0, 0);
                    }
                    else if (transform.localEulerAngles.x < 180)  // U?A?IN BURNU A?A?IDADIR..
                    {
                        if (transform.localEulerAngles.x > 2 && transform.localEulerAngles.x - 360 < -2) transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - angleDifference * Time.deltaTime * 10, 0, 0);
                    }
                }
            
            }
			else
			{
                transform.position = new Vector3(transform.position.x, transform.position.y - droppingSpeed * Time.deltaTime*10, transform.position.z + flyingSpeed * Time.deltaTime);
                if (transform.localEulerAngles.x < 13)
                {
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + angleDifference * Time.deltaTime * 5, 0, 0);
                }else if(transform.localEulerAngles.x > 180)
				{
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + angleDifference * Time.deltaTime * 5, 0, 0);
                }
            }
        }
        // SU ALMA ??LEMLER?????
        else if (isWatering) // SU ALMA ??LEMLER?... 
		{
            // su seviyesi slider?...
            waterAmount += Time.deltaTime * waterTakingSpeed;
            UIManager.instance.waterSlider.value = waterAmount;

            // bat?? miktar? slideri..
            waterDistance = waterDedector.transform.position.y;
            if (waterDistance < 0.01f) 
            {
                LevelFailedEvents();
            } 
            else if(waterDistance < maxValueDrownSlider)
			{
                UIManager.instance.drownSlider.value = maxValueDrownSlider - waterDistance;
			}
            if (Input.GetMouseButton(0))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + risingSpeed * Time.deltaTime, transform.position.z + Time.deltaTime * wateringSpeed * .2f);
                if (transform.localEulerAngles.x > 180) // U?A?IN BURNU YUKARDADIR.. YAN? ?N?ALLAH ?YLED?R :D
                {
                    if (transform.localEulerAngles.x > 2 && transform.localEulerAngles.x - 360 < -2) transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + angleDifference * Time.deltaTime * 4, 0, 0);
                }
                else if (transform.localEulerAngles.x < 180)  // U?A?IN BURNU A?A?IDADIR..
                {
                    if (transform.localEulerAngles.x > 2 && transform.localEulerAngles.x - 360 < -2) transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - angleDifference * Time.deltaTime * 10, 0, 0);
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y- +sinkingSpeed * Time.deltaTime, transform.position.z + Time.deltaTime * wateringSpeed * .2f);
                if (transform.localEulerAngles.x > 180) // U?A?IN BURNU YUKARDADIR.. YAN? ?N?ALLAH ?YLED?R :D
                {
                    if (transform.localEulerAngles.x > 2 && transform.localEulerAngles.x - 360 < -2) transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + angleDifference * Time.deltaTime * 4, 0, 0);
                }
                else if (transform.localEulerAngles.x < 180)  // U?A?IN BURNU A?A?IDADIR..
                {
                    if (transform.localEulerAngles.x > 2 && transform.localEulerAngles.x - 360 < -2) transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - angleDifference * Time.deltaTime * 10, 0, 0);
                }
            }

        }
        // S?ND?RME ??LEMLER??.....
        else if (isFireFighting)
        {
            // Su slider?n?n ayarlanmas?...
            //UIManager.instance.waterSlider.value = Mathf.Abs(fireEndPoint - transform.position.z) / fireDistance;
            GameManager.instance.increaseScore((int)(scoreSpeed*Time.deltaTime));

            // Su miktari biti ise levelin sonland?r?lmas?..
            if (UIManager.instance.waterSlider.value <= 0.0005f) LevelSuccessfullEvent();
            else
            {
                UIManager.instance.waterSlider.value = (fireEndPoint - transform.position.z) / fireDistance;

            }
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * fireFightingSpeed * .2f);
            if (transform.localEulerAngles.x > 180) // U?A?IN BURNU YUKARDADIR.. YAN? ?N?ALLAH ?YLED?R :D
            {
                if (transform.localEulerAngles.x > 2 && transform.localEulerAngles.x - 360 < -2) transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + angleDifference * Time.deltaTime * 4, 0, 0);
            }
            else if (transform.localEulerAngles.x < 180)  // U?A?IN BURNU A?A?IDADIR..
            {
                if (transform.localEulerAngles.x > 2 && transform.localEulerAngles.x - 360 < -2) transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - angleDifference * Time.deltaTime * 10, 0, 0);
            }
        } 
        // BA?ARILI S?ND?RME SONRASI ?D? U?A?IN E??ML? ?EK?LDE EKRANDAN UZAKLA?MASI ???N
        else if (isVictory)
		{
            transform.position = new Vector3(transform.position.x + Time.deltaTime * fireFightingSpeed * .1f, transform.position.y, transform.position.z + Time.deltaTime * fireFightingSpeed * .1f);
            if (transform.localEulerAngles.z < 180)
            {
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y + angleDifference * Time.deltaTime * 10, transform.localEulerAngles.x - angleDifference * Time.deltaTime * 10);
            }
            else if (transform.localEulerAngles.z - 360 > -30)
            {
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y + angleDifference * Time.deltaTime * 10, transform.localEulerAngles.z - angleDifference * Time.deltaTime * 10);
            }
        }
        // U?A?IN YANGIN S?ND?RME NOKTASINDAN ?NCE UYGUN POZ?SYONA ALINMASI ???N..
        else if (isComplete)
		{
            //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, bosaltmaNoktasi.transform.position.y, bosaltmaNoktasi.transform.position.z ), Time.deltaTime*2);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, bosaltmaNoktasi.transform.position.y, bosaltmaNoktasi.transform.position.z), Time.deltaTime * 70);
            if (transform.localEulerAngles.x > 180) // U?A?IN BURNU YUKARDADIR.. YAN? ?N?ALLAH ?YLED?R :D
            {
                if (transform.localEulerAngles.x > 2 && transform.localEulerAngles.x - 360 < -2) transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + angleDifference * Time.deltaTime * 4, 0, 0);
            }
            else if (transform.localEulerAngles.x < 180)  // U?A?IN BURNU A?A?IDADIR..
            {
                if (transform.localEulerAngles.x > 2 && transform.localEulerAngles.x - 360 < -2) transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - angleDifference * Time.deltaTime * 10, 0, 0);
            }
        }
        // ?ARPTIKTAN SONRAK? HAREKETLER?...
        else if (isCrashed)
        {
            //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, bosaltmaNoktasi.transform.position.y, bosaltmaNoktasi.transform.position.z ), Time.deltaTime*2);
            transform.position = new Vector3(transform.position.x, transform.position.y- Time.deltaTime * 5, transform.position.z + Time.deltaTime * 5);
            transform.localEulerAngles = new Vector3(75, transform.localEulerAngles.y + angleDifference * Time.deltaTime * 5, transform.localEulerAngles.z);
        }


    }

    public void PlaneStartingEvents()
	{
        isCrashed = false;
        isFlying = true;
        isWatering = false;
        isFireFighting = false;
        isVictory = false;
        isComplete = false;
        ResetAllAnim();
        StartPervaneAnim();
        lastFireObject =  GameManager.instance.FindLastFireObject();
        GetComponent<CapsuleCollider>().enabled = true;
        planeCrashSmoke.SetActive(false);
        CameraController.instance.ActivateCMVcam();
        waterAmount = 0;
        UIManager.instance.waterSlider.value = 0;
        UIManager.instance.drownSlider.value = 0;
        GameManager.instance.gems = 0;
        GameManager.instance.score = 0;
    }

    public void StartPanelEvents()
	{
        isCrashed = false;
        isFlying = false;
        isWatering = false;
        isFireFighting = false;
        isVictory = false;
        isComplete = false;
        planeCrashSmoke.SetActive(false);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        GetComponent<CapsuleCollider>().enabled = true;
        StopPervaneAnim();
        explodeEffect.SetActive(false);
        splashEffect.SetActive(false);
        waterEffect.SetActive(false);
        CameraController.instance.ActivateCMVcam();
        UIManager.instance.waterSlider.value = 0;
        UIManager.instance.drownSlider.value = 0;
        GameManager.instance.gems = 0;
        GameManager.instance.score = 0;
    }

    public void LevelFailedEvents()
	{
        isCrashed = true;
        isFlying = false;
        isWatering = false;
        isFireFighting = false;
        CameraController.instance.DeactivateCMVcam();
        UIManager.instance.LoosePanel.SetActive(true);
        UIManager.instance.GamePanel.SetActive(false);
        GetComponent<CapsuleCollider>().enabled = false;
        StopPervaneAnim();
        explodeEffect.SetActive(true);
        planeCrashSmoke.SetActive(true);
        SoundController.instance.PlayLooseEffectSound();
    }

    public void LevelSuccessfullEvent()
	{
        isVictory = true;
        isFlying = false;
        isWatering = false;
        isFireFighting = false;
        isComplete = false;
        waterEffect.SetActive(false);
        GetComponent<CapsuleCollider>().enabled = false;
        UIManager.instance.WinPanel.SetActive(true);
        UIManager.instance.GamePanel.SetActive(false);
        UIManager.instance.SetScoreText();
        UIManager.instance.SetGemsText();
        SoundController.instance.PlayWinEffectSound();
    }


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("water"))
		{
            // burada su al?rkenki meseleler d?necek...
            isFlying = false;
            isWatering = true;
            splashEffect.SetActive(true);
		}
        else if (other.CompareTag("ground"))
		{
            LevelFailedEvents();

		}
        else if (other.CompareTag("obstacle"))
		{
            // MESH RENDERERLAR KAPATILACAK.. GAME MANAGARDAK? L?STE ???NE NESNELER ATILACAK .. DAHA SONRA RESTART LEVEL DA A?ILACAK..
            //other.GetComponent<MeshRenderer>().enabled = false;
            LevelFailedEvents();
		}
        else if (other.CompareTag("sondurme"))
		{
            
            isComplete = true;
            isFlying = false;
            isWatering = false;

        }
        else if (other.CompareTag("collectible"))
		{
            other.GetComponent<MeshRenderer>().enabled = false;
            GameManager.instance.increaseGems();
            GameManager.instance.increaseScore(10);
		}
        else if (other.CompareTag("bosalt"))
		{
            CameraController.instance.DeactivateCMVcam();
            fireStartPoint = transform.position.z;
            bosaltmaNoktasiObject = other.gameObject;
            other.GetComponent<BoxCollider>().enabled = false;
            isComplete = false;
            isFlying = false;
            isWatering = false;
            isFireFighting = true;
            waterEffect.SetActive(true);
            CalculateFireDistance();
		}
        else if (other.CompareTag("fire"))
		{
            GameObject smokeparticle = Instantiate(smokePrefab, other.transform.position + new Vector3(0,10,0), Quaternion.Euler(-90,0,0));
            other.gameObject.SetActive(false);
            Destroy(smokeparticle, 10);
		}
	}

	private void OnTriggerExit(Collider other)
	{
        if (other.CompareTag("water"))
        {
            // burada su al?rkenki meseleler d?necek...
            isFlying = true;
            isWatering = false;
            splashEffect.SetActive(false);
        }
    }

    public void StartPervaneAnim()
	{
        pervane1Anim.SetTrigger("pervane");
        pervane2Anim.SetTrigger("pervane");
	}

    public void StopPervaneAnim()
	{
        pervane1Anim.SetTrigger("stop");
        pervane2Anim.SetTrigger("stop");
    }

    public void ResetAllAnim()
	{
        pervane1Anim.ResetTrigger("pervane");
        pervane2Anim.ResetTrigger("pervane");
        pervane1Anim.ResetTrigger("stop");
        pervane2Anim.ResetTrigger("stop");
	}

    public void CalculateFireDistance()
	{
        fireDistance = Mathf.Abs(transform.position.z - lastFireObject.transform.position.z);
        float beta = fireDistance * UIManager.instance.waterSlider.value;
        fireEndPoint = transform.position.z + beta;
      
	}
  
}
