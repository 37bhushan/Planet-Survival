using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;


public class AddManager : MonoBehaviour
{

    public Button adInitiator;

    public void PlayAD()
    {
        adInitiator.interactable = false;
        if (Advertisement.IsReady())
        {
            if (!adInitiator.IsInteractable())
                adInitiator.interactable = true;
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResults});
        }
    }

    private void HandleAdResults(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                AddCoins();
                break;
            case ShowResult.Failed:
                break;
            case ShowResult.Skipped:
                break;
        }
    }

    private void AddCoins()
    {
        int currentCoins = PlayerPrefs.GetInt("totalCoins");
        PlayerPrefs.SetInt("totalCoins", currentCoins + 25);
    }

}
