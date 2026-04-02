using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;
    private float leftbound = -10;
    private PlayerController playerControllerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        
        if(gameObject.CompareTag("Obstacle") && transform.position.x < leftbound)
        {
            Destroy(gameObject);
        }
    }
}
