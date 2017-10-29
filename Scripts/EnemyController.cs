using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameControllerScript controller;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            Vector3 position = gameObject.transform.position;

            Instantiate(Resources.Load("Explosion"), position, Quaternion.identity);
            Destroy(gameObject);
            

            Instantiate(Resources.Load("Enemy"), position, Quaternion.identity);

        }
        else if (collision.gameObject.tag == "Player")
        {
            if ((controller.playerHealth - 25) >= 0)
            {
                controller.playerHealth = controller.playerHealth - 25;
            }
            else
            {
                controller.playerHealth = 0;
            }
            Destroy(gameObject);
        }

    }
}
