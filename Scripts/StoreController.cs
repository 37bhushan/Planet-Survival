using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;


public class StoreController : MonoBehaviour
{
    
    //Text
    public Text coinsText;

    //Canvas Elements
    public Transform storeCanvas;

    //List of Items in Shop
    public ShopItems[] shopItems;
    // public ShopItem[] defaultShopItems;
    public ShopItems currentItem;

    //integer Variables
    public int totalCoins;

    //mapping buttons to shop items
    public Button[] purchaseButtons = new Button[5];
    public Button[] selectButtons;

    //Save Data;    


    private void Awake()
    {
        if (File.Exists(Application.persistentDataPath + "/planetData.dat"))
        {
            Debug.Log("awake load" + Application.persistentDataPath);

            Load();
        }
        else
        {
            Save();
        }



    }
    
    public void ShopInit()
    {
        if (File.Exists(Application.persistentDataPath + "/planetData.dat"))
        {
            Debug.Log("awake load" + Application.persistentDataPath);

            Load();
        }
        else
        {
            Save();
        }
    }


    private void Start()
    {
        ButtonMapper();
        totalCoins = PlayerPrefs.GetInt("totalCoins");
        coinsText.text = PlayerPrefs.GetInt("totalCoins").ToString("00000");
        CurrentPlayerFinder();
        ButtonInteractivity();

    }

    private void FixedUpdate()
    {
     //   Load();
        ButtonInteractivity();
        coinsText.text = totalCoins.ToString("00000");

    }


    public void Purchase(int itemIndex)
    {
        Time.timeScale = 0;

        totalCoins = PlayerPrefs.GetInt("totalCoins") - shopItems[itemIndex].itemPrice;
        PlayerPrefs.SetInt("totalCoins", totalCoins);

        shopItems[itemIndex].isPurchased = true;
        purchaseButtons[itemIndex].interactable = false;
        selectButtons[itemIndex].interactable = true;
        //1
        Save();
        ButtonInteractivity();
        Time.timeScale = 1;
    }

    public void Select(int itemIndex)
    {
        Time.timeScale = 0;

        shopItems[itemIndex].isSelected = true;
        selectButtons[itemIndex].interactable = false;
        currentItem.isSelected = false;

        CurrentPlayerFinder();
        ButtonInteractivity();
        //2.
        Save();
        Time.timeScale = 1;
    }

    void CurrentPlayerFinder()
    {

        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i].isSelected == true)
            {
                currentItem = shopItems[i];
                selectButtons[i].interactable = false;
                purchaseButtons[i].interactable = false;
            }
        }
    }


    void ButtonMapper()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            purchaseButtons[i] = GameObject.Find(shopItems[i].itemName + "Purchase").GetComponent<Button>();
            selectButtons[i] = GameObject.Find(shopItems[i].itemName + "Select").GetComponent<Button>();
        }
    }



    void ButtonInteractivity()
    {
        //Load();
        //Purchase Button
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (totalCoins > shopItems[i].itemPrice && shopItems[i].isPurchased == false)
            {
                purchaseButtons[i].interactable = true;
                selectButtons[i].interactable = false;
            }
            else
            {
                purchaseButtons[i].interactable = false;
            }
        }
        //Select Button

        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i].isPurchased == true && shopItems[i].isSelected == false)
            {
                purchaseButtons[i].interactable = false;
                selectButtons[i].interactable = true;
            }
            else
            {
                selectButtons[i].interactable = false;
            }
        }

        //Save();

    }


    public void BackButtonListener()
    {
        storeCanvas.gameObject.SetActive(false);
    }


    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/planetData.dat");

        bf.Serialize(file, shopItems);
        file.Close();

        Debug.Log("Data Saved");


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


