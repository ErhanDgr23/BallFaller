using UnityEngine;
using UnityEngine.Advertisements;

public class sdk : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] GameObject reward, inter, banner;
    [SerializeField] string _androidGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    private void Start()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        _gameId =_androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        reward.gameObject.SetActive(true);
        inter.gameObject.SetActive(true);
        banner.gameObject.SetActive(true);
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}