using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public static float offsetX;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (PlaneScript.instance != null)
        {
            if (PlaneScript.instance.isAlive)
            {
                MoveTheCamera();
            }
        }
	}

    void MoveTheCamera()
    {
        Vector3 temp = transform.position;
        temp.x = PlaneScript.instance.GetPositionX() + offsetX;
        transform.position = temp;
    }
}
