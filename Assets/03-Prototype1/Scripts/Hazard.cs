using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            Debug.Log("game over");
            //freeze player
            //Display you lose
            //Wait 5 sec
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
            //enable movement
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
