using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamespeed : MonoBehaviour
{
    [SerializeField] rewarded adsc;
    [SerializeField] Button adbut;
    [SerializeField] GameObject textobj;
    public float speedMultiplier = 2f;
    public float duration = 30f;

    public void OnSpeedButtonClicked()
    {
        adsc.ShowAd();
    }

    public void speedupbutton()
    {
        StopCoroutine(SpeedUpGame());
        StartCoroutine(SpeedUpGame());
    }

    public IEnumerator SpeedUpGame()
    {
        textobj.gameObject.SetActive(true);
        adbut.interactable = false;
        float originalTimeScale = Time.timeScale;
        Time.timeScale = speedMultiplier;
        yield return new WaitForSeconds(duration);
        Time.timeScale = originalTimeScale;
        adbut.interactable = true;
        textobj.gameObject.SetActive(false);
    }
}
