using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class Basket : MonoBehaviour
{
    [Header("Set Dynamically")]

    int score = 0;

    public TextMeshProUGUI scoreGt;

    void Start()
    {
        // Find a reference to the ScoreCounter
        GameObject scoreGO = GameObject.FindGameObjectWithTag("Score");
        // Get the Text Component of that GameO
        scoreGt = scoreGO.GetComponent<TextMeshProUGUI>();
        // Set the starting number of points to
        scoreGt.text = "Score: 0";
    }

    void Update()
    {
        // Get the current screen position of the mouse from Input
        Vector3 mousePos2D = new Vector3 (Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0);
        // The Camera's z position sets how far to push the mouse into 3d
        mousePos2D.z = -Camera.main.transform.position.z;
        // Convert the point from 2D screen space into 3d game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        // Move the x position of this Basket to the x position of the Mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    void OnCollisionEnter(Collision coll)
    {
        // Find out what hit this basket
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "Apple")
        {
            Destroy(collidedWith);
            score += 100;
            scoreGt.text = "Score: " + score.ToString();
            if (score > HighScore.score)
            {
                HighScore.score = score;
            }
        }
    }

}
