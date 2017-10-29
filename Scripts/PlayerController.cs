using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public MobilePlayerController mobileController;
    public float moveSpeed;
    private Vector3 moveDirection;
    public GameControllerScript controller;



    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        mobileController = GameObject.FindGameObjectWithTag("MobileJostick").GetComponent<MobilePlayerController>();


    }

    void Update()
    {
        moveDirection = new Vector3(mobileController.Horizontal(), 0, mobileController.Vertical()).normalized;
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            controller.playerHealth = 0;
        }
    }


}