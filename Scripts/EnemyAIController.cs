using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    GameControllerScript controller;
    private Transform Player;
    public int MoveSpeed = 10;
    public int MaxDist = 10;
    public int MinDist = 5;
    float life = 20;
    public AudioClip Spawn;
    public AudioClip Death;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        gameObject.GetComponent<AudioSource>().clip = Spawn;
        gameObject.GetComponent<AudioSource>().Play();
        //Debug.Log("start");

        life = Time.time + 20;
    }

    void Update()
    {
        transform.LookAt(Player);

        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }
        }

        if (life <= Time.time)
        {

            gameObject.GetComponent<AudioSource>().clip = Death;
            gameObject.GetComponent<AudioSource>().Play();
            //Debug.Log("Death");


            Destroy(gameObject);
            controller.currentEnemyCount = controller.currentEnemyCount - 1;
            controller.numberOfEnemiesDied++;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           //Debug.Log("Death");

            controller.playerHealth = 0;


            Destroy(gameObject);
        }
    }

}
