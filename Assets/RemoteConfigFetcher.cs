using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using Unity.Services.Core.Environments;
using Unity.Services.Authentication;
using System;

public struct userAttributes
{
    public int characterLevel;
}
public struct appAttributes
{

}
public class RemoteConfigFetcher : MonoBehaviour
{
    [SerializeField] string environmentName;
    [SerializeField] int characterLevel;
    [SerializeField] bool fetch;
    [SerializeField] float gravity;
    [SerializeField] PhoneGravity phoneGravity;

    async void Awake()
    {
        var options = new InitializationOptions();
        options.SetEnvironmentName(environmentName);
        await UnityServices.InitializeAsync(options);

        Debug.Log("UGS Initialized");

        if (AuthenticationService.Instance.IsSignedIn == false)
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

        Debug.Log("Player Signed In");
        RemoteConfigService.Instance.FetchCompleted += onFetchConfig;


        Debug.Log("Fetch Config");
        RemoteConfigService.Instance.FetchConfigs(
            new userAttributes() { characterLevel = this.characterLevel },
            new appAttributes());
    }

    private void OnDestroy()
    {
        RemoteConfigService.Instance.FetchCompleted -= onFetchConfig;
    }

    private void onFetchConfig(ConfigResponse response)
    {
        Debug.Log(response.requestOrigin);
        Debug.Log(response.body);

        switch (response.requestOrigin)
        {
            case ConfigOrigin.Default:
                Debug.Log("Default");
                break;
            case ConfigOrigin.Cached:
                Debug.Log("Cached");
                break;
            case ConfigOrigin.Remote:
                Debug.Log("Remote");
                gravity = RemoteConfigService.Instance.appConfig.GetFloat("Gravity");
                phoneGravity.SetGravityMagnitude(gravity);
                break;
        }
    }

    void Update()
    {
        if (fetch)
        {
            fetch = false;
            Debug.Log("Fetch Config");
            RemoteConfigService.Instance.FetchConfigs(
                new userAttributes() { characterLevel = this.characterLevel },
                new appAttributes()
            );
        }
    }
}
