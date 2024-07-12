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
    public bool tophazir = true;

    public AudioClip hitsound, deathsound, smallwinsound, failsound;
    public AudioSource sesci;

    [SerializeField] GameObject informationpan;
    [SerializeField] GameObject informationpanclosebut;
    [SerializeField] pyramidgenerator piramidsc;
    [SerializeField] Button ballspawnbut, upbut, downbut;
    [SerializeField] TextMeshProUGUI cointext;
    [SerializeField] GameObject ballobject;

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

    }

    public void delayledstart()
    {
        informationpanclosebut.gameObject.SetActive(true);
    }

    public void ballspawn()
    {
        piramidsc.testerecoinspawn();

        if (!tophazir)
            return;

        if(PlayerPrefs.GetFloat("coint") >= 4f)
        {
            PlayerPrefs.SetFloat("coint", PlayerPrefs.GetFloat("coint") - 4f);
            Instantiate(ballobject, startpos, Quaternion.identity);
            tophazir = false;
        }
    }

    public void pointreset()
    {
        PlayerPrefs.SetFloat("coint", 20f);
    }

    private void LateUpdate()
    {
        if(PlayerPrefs.GetFloat("coint") < 0)
            PlayerPrefs.SetFloat("coint", 0f);

        ballspawnbut.interactable = tophazir;

        if (piramidsc.pyramidHeight <= 10)
            downbut.interactable = false;
        else
            downbut.interactable = tophazir;

        if (piramidsc.pyramidHeight >= 24)
            upbut.interactable = false;
        else
            upbut.interactable = tophazir;

        cointext.text = PlayerPrefs.GetFloat("coint").ToString();
    }

    public void oyunbitti(GameObject playerobje, float value)
    {
        StopCoroutine(bekleoyunbitir(playerobje, 0f));
        StartCoroutine(bekleoyunbitir(playerobje, value));
    }

    public void updatepuan(float value)
    {
        PlayerPrefs.SetFloat("coint", PlayerPrefs.GetFloat("coint") + (4 * value));
    }

    IEnumerator bekleoyunbitir(GameObject item, float value)
    {
        yield return new WaitForSeconds(2f);
        updatepuan(value);
        Destroy(item);
        tophazir = true;
    }
}
