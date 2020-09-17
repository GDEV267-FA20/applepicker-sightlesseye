using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]

    // Prefab for instantiating apples
    public GameObject applePrefab;

    // Speed at which the AppleTree moves
    public float speed = 1f;

    // Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;

    // Chance that the AppleTree will change directions
    public float chanceToChangeDirections = 0.1f;

    // Rate at which Apples will be instantiated
    public float secondsBetweenAppleDrops = 1f;

    // Rate at which game gets more difficult
    public float secondsBetweenDifficultyIncrease = 5f;

    void Start() {
        // Dropping apples every second
        Invoke("DropApple", 2f);
        Invoke("Harder", 7);
    }

    void Update() {
        // Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Changing Direction
        if (pos.x < -leftAndRightEdge) {
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftAndRightEdge) {
            speed = -Mathf.Abs(speed);
        }
    }

    void FixedUpdate() {
        //There are 50 FixedUpdates per second
        if (Random.value < chanceToChangeDirections) {   
            speed *= -1;
        }

    }

    void DropApple()
    {   
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", secondsBetweenAppleDrops);
    }

    void Harder() {
        speed = speed * 1.2f;
        secondsBetweenAppleDrops = secondsBetweenAppleDrops * .9f;
        if(secondsBetweenAppleDrops <= 0f) {
            secondsBetweenAppleDrops = .05f;
        }
        secondsBetweenDifficultyIncrease += 1f;
        Invoke("Harder", secondsBetweenDifficultyIncrease);
    }

}
