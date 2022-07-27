using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId="4478544";
    [SerializeField] string _iOsGameId="4478545";
    [SerializeField] bool _testMode = true;
    [SerializeField] bool _enablePerPlacementMode = true;
    private string _gameId;
    private ReklamLokshi reklam;
    public void InitializeAds()
    {
        
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, _enablePerPlacementMode, this);

    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        //reklam.LoadAd();    
        
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}