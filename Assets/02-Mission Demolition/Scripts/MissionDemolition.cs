using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S;

    [Header("Set in Inspecter")]
    public Text uitLevel;
    public Text uitShots;
    public Text uitButton;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Set Dynamically")]
    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
        levelMax = castles.Length;
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGui();

        if ((mode == GameMode.playing) && Goal.goalMet == true)
        {
            mode = GameMode.levelEnd;
            SwitchView("Show Both");
            Invoke("NextLevel", 2f);
        }
    }

    void StartLevel()
    {
        if (castle != null) Destroy(castle);

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos) Destroy(pTemp);

        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        SwitchView("Show Both");
        ProjectileLine.s.Clear();

        Goal.goalMet = false;

        UpdateGui();

        mode = GameMode.playing;
    }

    void UpdateGui()
    {
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax) level = 0;
        StartLevel();
    }

    public void SwitchView(string eView = "")
    {

        showing = eView;
        switch(eView)
        {
            case "Show Slingshot":
                FollowCam.poi = null;
                uitButton.text = "Show Castle";
                break;

            case "Show Castle":
                FollowCam.poi = S.castle;
                uitButton.text = "Show Both";
                break;

            case "Show Both":
                FollowCam.poi = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }
    public static void ShotFired() { S.shotsTaken++; }
}
