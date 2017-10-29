using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    public GameControllerScript controller;


    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            controller.numberOfCoinsCollected++;
        }
    }

}
