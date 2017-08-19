using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public static GameController instance;

    public static string HIGH_SCORE = "High Score";
    public static string SELECTED_PLANE = "Selected Plane";
    public static string GREEN_PLANE = "Green Plane";
    public static string BLUE_PLANE = "Blue Plane";
    public static string YELLOW_PLANE = "Yellow Plane";

    void Awake()
    {
        MakeSingleton();
    }
    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
       InitPrefs();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitPrefs()
    {
        if (!PlayerPrefs.HasKey("Game Initialized"))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
            PlayerPrefs.SetInt(SELECTED_PLANE, 0);
            PlayerPrefs.SetInt(GREEN_PLANE, 0);
            PlayerPrefs.SetInt(BLUE_PLANE, 0);
            PlayerPrefs.SetInt(YELLOW_PLANE, 0);

            PlayerPrefs.SetInt("Game Initialized", 1);
        }
    }

    public void SetHighScore(int score){
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }

    public void SetSelectedPlane(int selectedPlane)
    {
        PlayerPrefs.SetInt(SELECTED_PLANE, selectedPlane);
    }

    public int GetSelectedPlane()
    {
        return PlayerPrefs.GetInt(SELECTED_PLANE);
    }

    public void UnlockGreenPlane()
    {
        PlayerPrefs.SetInt(GREEN_PLANE, 1);
    }

    public int IsGreenPlaneUnlocked()
    {
        return PlayerPrefs.GetInt(GREEN_PLANE);
    }

    public void UnlockBluePlane()
    {
        PlayerPrefs.SetInt(BLUE_PLANE, 1);
    }

    public int IsBluePlaneUnlocked()
    {
        return PlayerPrefs.GetInt(BLUE_PLANE);
    }

    public void UnlockYellowPlane()
    {
        PlayerPrefs.SetInt(YELLOW_PLANE, 1);
    }

    public int IsYellowPlaneUnlocked()
    {
        return PlayerPrefs.GetInt(YELLOW_PLANE);
    }
}
