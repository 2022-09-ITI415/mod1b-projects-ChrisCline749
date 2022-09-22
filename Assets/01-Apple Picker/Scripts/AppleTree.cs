using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
   
    public GameObject applePrefab;

    public float speed = 1f;
    public float leftAndRightEdge = 10f;
    public float chanceToChangeDirection;
    public float secondsBetweenAppleDrops;

    public static float bottomY = -20f;

    void Start()
    {
        // Dropping apples every second
        Invoke("DropApple", 2f);
    }

    void Update()
    {
        // Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Changing Direction
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed);
        }

    }

    void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirection)
        {
            speed *= -1;
        }
    }

    void DropApple()
    {
        GameObject apple = Instantiate <GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", secondsBetweenAppleDrops);
    }

}
