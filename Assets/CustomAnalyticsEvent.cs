using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services;
using UnityEngine;
using Unity.Services.Core.Environments;
using Unity.Services.Analytics;

public class CustomAnalyticsEvent : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] string level;
    [SerializeField] int score;
    [SerializeField] bool isAlive;
    async void Start()
    {
        InitializationOptions options = new InitializationOptions();
        options.SetEnvironmentName("development");
        await UnityServices.InitializeAsync(options);
        await AnalyticsService.Instance.CheckForRequiredConsents();

        Debug.Log("User ID: " + AnalyticsService.Instance.GetAnalyticsUserID());
    }

    [SerializeField] float timer = 61;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 60)
        {
            var Parameters = new Dictionary<string, object>();
            Parameters["Level"] = level;
            Parameters["Health"] = health;
            Parameters["Score"] = score;
            Parameters["Alive"] = isAlive;

            Debug.Log("Send Analytic Player Stats");
            AnalyticsService.Instance.CustomData("PlayerStats", Parameters);

            health -= 1;
            score += 1;
            isAlive = !isAlive;

            timer = 0;
        }
    }
}

