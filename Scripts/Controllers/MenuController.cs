using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
    public static MenuController instance;

    [SerializeField]
    private GameObject[] planes;

    //[SerializeField] enable for debug purposes
    private bool isGreenPlaneUnlocked, isBluePlaneUnlocked, isYellowPlaneUnlocked;


    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

	// Use this for initialization
	void Start () {

        planes[GameController.instance.GetSelectedPlane()].SetActive(true);
        CheckUnlockedPlanes();
	}

    void CheckUnlockedPlanes()
    {
        if (GameController.instance.IsGreenPlaneUnlocked() == 1)
        {
            isGreenPlaneUnlocked = true;
        } 
        if (GameController.instance.IsBluePlaneUnlocked() == 1)
        {
            isBluePlaneUnlocked = true;
        }
        if (GameController.instance.IsYellowPlaneUnlocked() == 1)
        {
            isYellowPlaneUnlocked = true;
        }
    }
    public void ConnectOnGooglePlayGames()
    {
        //LeaderboardsController.instance.ConnectOrDisconnectOnGooglePlayGames();
    }

    public void OpenLeaderboardsScoreUI()
    {
        //LeaderboardsController.instance.OpenLeaderboardsScore();
    }
    public void ChangePlane()
    {
        //TODO show next available plane because if i unlock blue plane but dont have green plane unlocked i cant 
        // cycle to it.
        if (GameController.instance.GetSelectedPlane() == 0) // red plane selected
        {
            if (isGreenPlaneUnlocked)
            {
                planes[0].SetActive(false); // disable red plan 
                GameController.instance.SetSelectedPlane(1); //select green plan
                planes[GameController.instance.GetSelectedPlane()].SetActive(true); //enable green plane
            }
        }
        else if (GameController.instance.GetSelectedPlane() == 1) // green plane selected
        {
            if (isBluePlaneUnlocked)
            {
                planes[1].SetActive(false);// disable green plane
                GameController.instance.SetSelectedPlane(2); //select blue plane
                planes[GameController.instance.GetSelectedPlane()].SetActive(true); //enable blue plane
            }
            else
            {
                planes[1].SetActive(false); //disable green plane
                GameController.instance.SetSelectedPlane(0);  //select red plane
                planes[GameController.instance.GetSelectedPlane()].SetActive(true); // enable red plane
            }
        }
        else if (GameController.instance.GetSelectedPlane() == 2) //blue plane selected
        {
            if (isYellowPlaneUnlocked)
            {
                planes[2].SetActive(false); //disable blue plane
                GameController.instance.SetSelectedPlane(3); //select yellow plane
                planes[GameController.instance.GetSelectedPlane()].SetActive(true); //enable yellow plane
            }
            else
            {
                planes[2].SetActive(false); //disable blue plane
                GameController.instance.SetSelectedPlane(0);  //select red plane
                planes[GameController.instance.GetSelectedPlane()].SetActive(true); // enable red plane
            }
        }
        else if (GameController.instance.GetSelectedPlane() == 3) // yellow plane selected
        {
                planes[3].SetActive(false); // disable yellow plane
                GameController.instance.SetSelectedPlane(0); //select red plane
                planes[GameController.instance.GetSelectedPlane()].SetActive(true); //enable red plane
        }

    }

    public void PlayGame()
    {
        SceneFader.instance.LoadLevel("Gameplay");
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
	}
}
