
using UnityEngine;

public class ExplosionController : MonoBehaviour
{

    public float life = 2;

    // Use this for initialization
    void Start()
    {
        life = life + Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (life <= Time.time)
        {
            Destroy(gameObject);
            //Debug.Log("Explosion Complete");
        }

    }
}
