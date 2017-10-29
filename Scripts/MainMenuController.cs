using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    int totalCoins;
    private Text totalCoinsText;
    public Button quitButton;
    public Button facebookButton, instaButton;
    public Button storeButton;
    public Transform storeCanvas;
    public StoreController store;
    public GameObject loader;
    public Slider slide;


    private void Start()
    {
        //PlayerPrefs.DeleteAll();


        totalCoinsText = GameObject.Find("TotalCoins").GetComponent<Text>();
        totalCoins = PlayerPrefs.GetInt("totalCoins");
        totalCoinsText.text = totalCoins.ToString("00000");

        store.ShopInit();
        //  quitButton.onClick.AddListener(QuitButtonListener);
        facebookButton.onClick.AddListener(FacebookListener);
        instaButton.onClick.AddListener(InstaListener);
        storeButton.onClick.AddListener(storeButtonListener);


    }

    private void FixedUpdate()
    {
        totalCoins = PlayerPrefs.GetInt("totalCoins");
        totalCoinsText.text = totalCoins.ToString("00000");
    }


    private void storeButtonListener()
    {
        if (storeCanvas.gameObject.activeInHierarchy == false)
        {
            storeCanvas.gameObject.SetActive(true);
        }
        else
        {
            storeCanvas.gameObject.SetActive(false);
        }
    }

    private void InstaListener()
    {
        Application.OpenURL("https://www.instagram.com/37bhushan/");
    }

    private void FacebookListener()
    {
        Application.OpenURL("https://www.facebook.com/5bhushan.gosavi");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void QuitButtonListener()
    {
        Application.Quit();
    }

    public void LoadScene(int level)
    {
        //SceneManager.LoadScene(level);
        StartCoroutine(LoadAsync(level));

    }

    IEnumerator LoadAsync(int level)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(level);

        loader.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slide.value = progress;

            yield return null;
        }
    }


}
