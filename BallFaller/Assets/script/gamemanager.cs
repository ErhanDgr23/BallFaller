using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gamemanager : MonoBehaviour
{
    public static gamemanager mangersc;

    public Vector3 startpos;
    public PhysicMaterial fizikmat;
    public pyramidgenerator piramidsc;
    public bool tophazir = true;
    public AudioClip hitsound, deathsound, smallwinsound, failsound;
    public AudioSource sesci;

    [SerializeField] GameObject textanim;
    [SerializeField] GameObject informationpan;
    [SerializeField] GameObject informationpanclosebut;
    [SerializeField] Transform ballstorge;
    [SerializeField] Button ballspawnbut, upbut, downbut;
    [SerializeField] TextMeshProUGUI cointext;
    [SerializeField] GameObject ballobject;
    [SerializeField] AiryUIAnimatedElement settingpanair;
    [SerializeField] Sprite soundon, soundoff;
    [SerializeField] Image soundeimage;
    [SerializeField] float ballkatsayi = 1;

    float[] playerxrandom;
    bool settingpan, sesb = true, adsb;

    private void Awake()
    {
        mangersc = this;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("inform"))
        {
            informationpan.gameObject.gameObject.SetActive(true);
            Invoke("delayledstart", 2f);
            PlayerPrefs.SetString("inform", "inform");
        }
        else
            informationpan.gameObject.gameObject.SetActive(false);

        if (!PlayerPrefs.HasKey("coint"))
            PlayerPrefs.SetFloat("coint", 20f);

        playerxrandom = new float[] { -0.05f, -0.04f, -0.03f, -0.02f, -0.01f, 0.01f, 0.02f, 0.03f, 0.04f, 0.05f };
    }

    public void delayledstart()
    {
        informationpanclosebut.gameObject.SetActive(true);
    }

    public void ballspawn()
    {
        startpos = new Vector3(playerxrandom[Random.Range(0, playerxrandom.Length)], (piramidsc.pyramidHeight * 1.65f), 0f);
        //piramidsc.testerecoinspawn();

        if (!tophazir)
            return;

        if (PlayerPrefs.GetFloat("coint") >= 1f)
        {
            PlayerPrefs.SetFloat("coint", PlayerPrefs.GetFloat("coint") - 1f);
            GameObject ball = Instantiate(ballobject, ballstorge);
            ball.transform.position = startpos;
            tophazir = false;
            StopCoroutine(bekletophazir());
            StartCoroutine(bekletophazir());
        }
    }

    public void pointreset()
    {
        PlayerPrefs.SetFloat("coint", 20f);
    }

    public void settingpanackapa()
    {
        settingpan = !settingpan;

        if (settingpan)
            settingpanair.ShowElement();
        else
            settingpanair.HideElement();
    }

    public void sesbut()
    {
        sesb = !sesb;

        if (sesb)
            soundeimage.sprite = soundon;
        else
            soundeimage.sprite = soundoff;
    }

    private void LateUpdate()
    {
        if(PlayerPrefs.GetFloat("coint") < 0)
            PlayerPrefs.SetFloat("coint", 0f);

        if (Input.GetKeyDown(KeyCode.G))
            PlayerPrefs.SetFloat("coint", PlayerPrefs.GetFloat("coint") + 25);

        sesci.enabled = sesb;
        ballspawnbut.interactable = tophazir;

        if (piramidsc.pyramidHeight <= 10)
            downbut.interactable = false;
        else if(ballstorge.childCount <= 0f)
            downbut.interactable = true;
        else if (ballstorge.childCount > 0f)
            downbut.interactable = false;

        if (piramidsc.pyramidHeight >= 24)
            upbut.interactable = false;
        else if (ballstorge.childCount <= 0f)
            upbut.interactable = true;
        else if (ballstorge.childCount > 0f)
            upbut.interactable = false;

        cointext.text = PlayerPrefs.GetFloat("coint").ToString("#,##0.###");
    }

    public void oyunbitti(GameObject playerobje, float value, kapsc kap)
    {
        StopCoroutine(bekleoyunbitir(playerobje, 0f, null));
        StartCoroutine(bekleoyunbitir(playerobje, value, kap));
    }

    public void updatepuan(float value)
    {
        PlayerPrefs.SetFloat("coint", PlayerPrefs.GetFloat("coint") + (value));
    }

    IEnumerator bekleoyunbitir(GameObject item, float value, kapsc kap)
    {
        yield return new WaitForSeconds(0.1f);

        if(item != null)
        {
            GameObject textanimclone = Instantiate(textanim, item.transform.position + new Vector3(0f, 4f, 0f), Quaternion.identity);
            textanimclone.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = kap.color;
            textanimclone.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + (ballkatsayi * value).ToString("#,##0.###");
            Destroy(textanimclone, 1f);
        }

        updatepuan(ballkatsayi * value);
        yield return new WaitForSeconds(0.1f);
        Destroy(item);
        yield return new WaitForSeconds(1f);
        tophazir = true;
    }

    IEnumerator bekletophazir()
    {
        yield return new WaitForSeconds(0.2f);
        tophazir = true;
    }
}
