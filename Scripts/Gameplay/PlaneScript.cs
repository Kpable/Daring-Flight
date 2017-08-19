using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlaneScript : MonoBehaviour {

    public static PlaneScript instance;

    [SerializeField]
    private Rigidbody2D myBody;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private AudioSource audio;

    [SerializeField]
    //private AudioClip fly, point, died;

    private float forwardSpeed = 3f;
    private float bounceSpeed = 4f;

    private bool didFly;
    public bool isAlive;

    private Button flyButton;
    public int score = 0;

	void Awake () {
        if (instance == null)
        {
            instance = this;
        }

        isAlive = true;

        flyButton = GameObject.FindGameObjectWithTag("FlyButton").GetComponent<Button>();
        flyButton.onClick.AddListener(() => FlyThePlane());
        SetCamerasX();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isAlive)
        {
            Vector3 temp = transform.position;
            temp.x += forwardSpeed * Time.deltaTime;
            transform.position = temp;

            if (didFly)
            {
                didFly = false;
                myBody.velocity = new Vector3(0, bounceSpeed);
                //audio.PlayOneShot(fly);
                //anim.SetTrigger("Fly");
            }
        }

        if (myBody.velocity.y >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            float angle = 0;
            angle = Mathf.Lerp(0, -90, -myBody.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
	}

    void SetCamerasX()
    {
        CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x);
    }

    public float GetPositionX()
    {
        return transform.position.x;
    }

    public void FlyThePlane()
    {
        didFly = true;
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Ground" || target.gameObject.tag == "Rock")
        {
            if (isAlive)    //The player has died!
            {
                isAlive = false;
                //audio.PlayOneShot(died);
                GameplayController.instance.PlayerDiedShowScore(score);
            }
           
        }
        
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "RockHolder")
        {
            if(isAlive) score++;
            //audio.PlayOneShot(point);
            GameplayController.instance.SetScore(score);
        }

    }
}
