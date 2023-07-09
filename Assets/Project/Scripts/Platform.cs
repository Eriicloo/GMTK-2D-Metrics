using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Vector2 startPos;
    List<Rigidbody2D> objectsInPlatform;
    float totalMass = 0;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D collisionRb =  collision.gameObject.GetComponent<Rigidbody2D>();
        foreach (Rigidbody2D rb in objectsInPlatform)
        {
            if(rb == collisionRb)
            {
                return;
            }
            else
            {
                totalMass += collision.gameObject.GetComponent<Rigidbody2D>().mass;
                objectsInPlatform.Add(collisionRb);
            }
        }
        Vector2 posPlatformMove;
        switch (totalMass)
        {
            case 10:
                posPlatformMove = new Vector2(transform.gameObject.transform.position.x, transform.gameObject.transform.position.y - 1);
                MovePlatform(posPlatformMove, 1.0f);
                break;
            case 20:
                posPlatformMove = new Vector2(transform.gameObject.transform.position.x, transform.gameObject.transform.position.y - 2);
                MovePlatform(posPlatformMove, 1.0f);
                break;
            case 30:
                posPlatformMove = new Vector2(transform.gameObject.transform.position.x, transform.gameObject.transform.position.y - 3);
                MovePlatform(posPlatformMove, 1.0f);
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        totalMass -= other.gameObject.GetComponent<Rigidbody2D>().mass;
        objectsInPlatform.Remove(other.GetComponent<Rigidbody2D>());
        Vector2 posPlatformMove;
        switch (totalMass)
        {
            case 10:
                posPlatformMove = new Vector2(transform.gameObject.transform.position.x, transform.gameObject.transform.position.y - 1);
                MovePlatform(posPlatformMove, 1.0f);
                break;
            case 20:
                posPlatformMove = new Vector2(transform.gameObject.transform.position.x, transform.gameObject.transform.position.y - 2);
                MovePlatform(posPlatformMove, 1.0f);
                break;
            case 30:
                posPlatformMove = new Vector2(transform.gameObject.transform.position.x, transform.gameObject.transform.position.y - 3);
                MovePlatform(posPlatformMove, 1.0f);
                break;
            default:
                MovePlatform(startPos, 1.0f);
                break;
        }
    }

    private void MovePlatform(Vector2 position, float time)
    {
        transform.position = Vector2.Lerp(transform.position, position, time);
    }
}
