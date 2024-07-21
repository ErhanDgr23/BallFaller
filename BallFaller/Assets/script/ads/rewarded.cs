using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Events;

public class rewarded : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
	[SerializeField] UnityEvent fonksiyon;
	[SerializeField] string _androidAdUnitId = "Rewarded_Android";
	[SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
	string _adUnitId;
	public float timer;
	bool reklamgösterildi;

    void Awake()
	{
		_adUnitId = _androidAdUnitId;
		//Disable button until ad is ready to show
		//_showAdButton.interactable = false;
	}
    private void Update()
    {
		timer += Time.deltaTime;
        if (reklamgösterildi == false && timer >= 260)
        {
			ShowAd();
			reklamgösterildi = true;
        }
	}
    private void Start()
    {
		LoadAd();
    }

    // Load content to the Ad Unit:
    public void LoadAd()
	{
		// IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
		Debug.Log("Loading Ad: " + _adUnitId);
		Advertisement.Load(_adUnitId, this);
	}

	// If the ad successfully loads, add a listener to the button and enable it:
	public void OnUnityAdsAdLoaded(string adUnitId)
	{
		Debug.Log("Ad Loaded: " + adUnitId);

		if (adUnitId.Equals(_adUnitId))
		{

		}
	}

	// Implement a method to execute when the user clicks the button.
	public void ShowAd()
	{
		// Disable the button: 
		//_showAdButton.interactable = false;
		// Then show the ad:
		Advertisement.Show(_adUnitId, this);
	}

	// Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
	public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
	{
		if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
		{
			Debug.Log("Unity Ads Rewarded Ad Completed");
			// Grant a reward.
			fonksiyon.Invoke();
            // Load another ad:
            Advertisement.Load(_adUnitId, this);
		}
	}

	// Implement Load and Show Listener error callbacks:
	public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
	{
		Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
		// Use the error details to determine whether to try to load another ad.
		LoadAd();

	}

	public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
	{
		Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
		// Use the error details to determine whether to try to load another ad.
		LoadAd();
	}

	public void OnUnityAdsShowStart(string adUnitId) { }
	public void OnUnityAdsShowClick(string adUnitId) { }

	void OnDestroy()
	{

	}
}