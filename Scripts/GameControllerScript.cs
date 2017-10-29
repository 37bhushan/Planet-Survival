using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameControllerScript : MonoBehaviour
{
    //integer variables
    public int currentEnemyCount, numberOfEnemies;
    public int numberOfEnemiesDied = 0;
    public int numberOfCoinsCollected = 0;
    public int prevNumberOfCoinsCollected = 0;
    private int i, integerTime;
    private int highScore;
    private int totalCoins;

    //float variables
    public float playerHealth = 100f;
    private float totalTimePlayed = 0f;

    //UI->Text holders
    private Text enemiesCount, scoreCount, coinCount, timeCount;
    private Text highScoreText;
    private Text totalCoinsText;
    private Text currentScoreText;

    //GameObjects
    public GameObject enemy;
    public GameObject coin;
    private GameObject[] enemyArray;
    private GameObject[] coinArray;

    //Tranforms for UI elements
    public Transform pauseScreenCanvas;
    public Transform gameOverCanvas;
    public Transform highScoreCanvas;

    //UI->Buttons
    public Button pauseButton;
    public Button restartButton;
    public Button highScoreRestartButton;
    public Button[] homeButton;

    //Star System
    public Image[] GameOverSccreenStarts;
    public Image[] HighScoreScreenStarts;

    //Strings 
    public string currentPlayer;

    //Shop Item Collection
    public ShopItems[] shopItems;

    // Audio 
    public AudioManager manager;
    public AudioManager starAudio;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        LoadPlayer();

        //highScore = PlayerPrefs.GetInt("highScore");
        //totalCoins = PlayerPrefs.GetInt("totalCoins");

        pauseButton = pauseButton.GetComponent<Button>();
        pauseButton.onClick.AddListener(pauseScreenListener);

        restartButton = restartButton.GetComponent<Button>();
        restartButton.onClick.AddListener(gameOverListerner);

        highScoreRestartButton = highScoreRestartButton.GetComponent<Button>();
        highScoreRestartButton.onClick.AddListener(highScoreListener);

        homeButton[0] = homeButton[0].GetComponent<Button>();
        homeButton[1] = homeButton[1].GetComponent<Button>();

        homeButton[0].onClick.AddListener(homeButtonListener);
        homeButton[1].onClick.AddListener(homeButtonListener);
        homeButton[2].onClick.AddListener(homeButtonListener);

        StartCoroutine(RandomEnemySpawner());
        StartCoroutine(RandomCoinSpawner());
    }

    private void LoadPlayer()
    {
        manager.Play("Theme");
        Time.timeScale = 0;
        Load();

        bool isSelectFound = false;
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i].isSelected)
            {
                currentPlayer = shopItems[i].itemName;
                isSelectFound = true;
            }
        }

        if (isSelectFound == false)
        {
            for (int i = 0; i < shopItems.Length; i++)
            {
                if (shopItems[i].isDefault)
                {
                    currentPlayer = shopItems[i].itemName;
                    isSelectFound = true;
                }
            }
        }
        //  Debug.Log(currentPlayer);
        Instantiate(Resources.Load(currentPlayer));
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreenListener();
        }
    }


    private void FixedUpdate()
    {
        totalTimePlayed += Time.deltaTime;

        if (numberOfCoinsCollected > prevNumberOfCoinsCollected)
        {
            manager.Play("Coin");
            prevNumberOfCoinsCollected = numberOfCoinsCollected;
        }
        coinCount = GameObject.Find("CoinsCollected").GetComponent<Text>();
        coinCount.text = numberOfCoinsCollected.ToString("00000");

        timeCount = GameObject.Find("TimePlayed").GetComponent<Text>();
        integerTime = (int)totalTimePlayed;
        timeCount.text = integerTime.ToString("00000");

        if (playerHealth == 0)
        {
            CanvasDecider();
        }
    }

    private void CanvasDecider()
    {
        if (integerTime > PlayerPrefs.GetInt("highScore"))
        {
            //3
            manager.Stop("Theme");
            manager.Play("HighScoreTheme");
            HighScoreHandler();
        }
        else
        {
            //5
            manager.Stop("Theme");
            manager.Play("GameOverTheme");
            GameOverHandler();
        }
    }

    // Event Listeners

    public void pauseScreenListener()
    {
        if (pauseScreenCanvas.gameObject.activeInHierarchy == false)
        {
            pauseScreenCanvas.gameObject.SetActive(true);
            //1
            manager.Stop("Theme");
            manager.Play("PauseScreenTheme");

            Time.timeScale = 0;
            SetTotalCoins();
        }
        else
        {

            pauseScreenCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;

            //2
            manager.Play("Theme");
            manager.Stop("PauseScreenTheme");
        }
    }


    private void highScoreListener()
    {
        if (highScoreCanvas.gameObject.activeInHierarchy == false)
        {

            highScoreCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            highScoreCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;


            for (int i = 0; i < HighScoreScreenStarts.Length; i++)
            {
                HighScoreScreenStarts[i].gameObject.SetActive(false);
            }


            //4
            manager.Stop("HighScoreTheme");
            manager.Play("Theme");
        }
    }

    private void homeButtonListener()
    {
        SceneManager.LoadScene(0);
    }


    private void gameOverListerner()
    {
        if (gameOverCanvas.gameObject.activeInHierarchy == false)
        {
            gameOverCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            gameOverCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;

            for (int i = 0; i < GameOverSccreenStarts.Length; i++)
            {
                GameOverSccreenStarts[i].gameObject.SetActive(false);
            }


            //6 
            manager.Stop("GameOverTheme");
            manager.Play("Theme");

        }
    }



    IEnumerator RandomEnemySpawner()
    {
        Vector3 position = UnityEngine.Random.onUnitSphere * 50;
        if (currentEnemyCount <= numberOfEnemies)
        {
            Instantiate(enemy, position, Quaternion.identity);
            currentEnemyCount++;
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine(RandomEnemySpawner());
    }

    IEnumerator RandomCoinSpawner()
    {
        Vector3 coinPosition = UnityEngine.Random.onUnitSphere * 20;
        Instantiate(coin, coinPosition, Quaternion.identity);
        yield return new WaitForSeconds(10f);
        StartCoroutine(RandomCoinSpawner());
    }

    // Game Over Screen

    private void GameOverHandler()
    {
        gameOverCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
        SetStar();
        SetTotalCoins();
        SetCurrentScore();
        ResetAll();
    }

    private void HighScoreHandler()
    {
        highScoreCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
        SetStar();
        SetHighScore();
        SetTotalCoins();
        ResetAll();
    }

    public void SetCurrentScore()
    {
        currentScoreText = GameObject.Find("CurrentScore").GetComponent<Text>();
        currentScoreText.text = "Your Score: \n" + integerTime.ToString("00000");
    }

    public void SetHighScore()
    {
        PlayerPrefs.SetInt("highScore", integerTime);
        highScoreText = GameObject.Find("HighScore").GetComponent<Text>();
        highScoreText.text = "New High Score: " + PlayerPrefs.GetInt("highScore").ToString("00000");
        PlayerPrefs.Save();
    }

    public void SetTotalCoins()
    {
        totalCoins = PlayerPrefs.GetInt("totalCoins") + numberOfCoinsCollected;
        PlayerPrefs.SetInt("totalCoins", totalCoins);
        // Debug.Log(totalCoins);
        totalCoinsText = GameObject.Find("TotalCoins").GetComponent<Text>();
        totalCoinsText.text = PlayerPrefs.GetInt("totalCoins").ToString("00000");
        PlayerPrefs.Save();
    }

    public void ResetAll()
    {
        playerHealth = 100;
        numberOfCoinsCollected = numberOfEnemiesDied = 0;
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        coinArray = GameObject.FindGameObjectsWithTag("Coin");

        for (i = 0; i < coinArray.Length; i++)
        {
            Destroy(coinArray[i]);
        }
        for (i = 0; i < enemyArray.Length; i++)
        {
            Destroy(enemyArray[i]);
        }
        currentEnemyCount = 0;
        totalTimePlayed = 0;
        prevNumberOfCoinsCollected = 0;
    }

    public void SetStar()
    {
        if (gameOverCanvas.gameObject.activeInHierarchy == true)
        {
            if (integerTime > 20 && integerTime < 35)
            {
                starAudio.Play("Star");
                GameOverSccreenStarts[0].gameObject.SetActive(true);
            }
            else if (integerTime >= 35 && integerTime < 50)
            {
                starAudio.Play("Star");

                GameOverSccreenStarts[0].gameObject.SetActive(true);

                StartCoroutine(StarEnable());
                starAudio.Play("Star");

                GameOverSccreenStarts[1].gameObject.SetActive(true);
            }
            else if (integerTime >= 50)
            {
                starAudio.Play("Star");

                GameOverSccreenStarts[0].gameObject.SetActive(true);

                StartCoroutine(StarEnable());

                starAudio.Play("Star");

                GameOverSccreenStarts[1].gameObject.SetActive(true);

                StartCoroutine(StarEnable());

                starAudio.Play("Star");

                GameOverSccreenStarts[2].gameObject.SetActive(true);
            }
        }
        else if (highScoreCanvas.gameObject.activeInHierarchy == true)
        {
            if (integerTime > 20 && integerTime < 35)
            {
                starAudio.Play("Star");

                HighScoreScreenStarts[0].gameObject.SetActive(true);
            }
            else if (integerTime >= 35 && integerTime < 50)
            {
                starAudio.Play("Star");

                HighScoreScreenStarts[0].gameObject.SetActive(true);

                StartCoroutine(StarEnable());

                starAudio.Play("Star");

                HighScoreScreenStarts[1].gameObject.SetActive(true);
            }
            else if (integerTime >= 50)
            {
                starAudio.Play("Star");

                HighScoreScreenStarts[0].gameObject.SetActive(true);

                starAudio.Play("Star");

                StartCoroutine(StarEnable());

                starAudio.Play("Star");

                HighScoreScreenStarts[1].gameObject.SetActive(true);

                StartCoroutine(StarEnable());

                starAudio.Play("Star");

                HighScoreScreenStarts[2].gameObject.SetActive(true);
            }

        }
    }


    public IEnumerator StarEnable()
    {
        yield return new WaitForSeconds(1f);
    }

    public void Load()
    {
        Debug.Log("in load Function");
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/planetData.dat"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/planetData.dat", FileMode.Open);
            shopItems = (ShopItems[])bf.Deserialize(file);
            file.Close();

            Debug.Log("File Loaded");
        }
        else
        {
            Debug.Log("File Not Fond");
        }
    }
}


