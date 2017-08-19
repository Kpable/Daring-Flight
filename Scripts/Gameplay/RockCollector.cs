using UnityEngine;
using System.Collections;

public class RockCollector : MonoBehaviour {
    private GameObject[] rockHolders;
    private float distance = 3f;
    private float lastRocksX;
    private float rockMin = -2.3f;
    private float rockMax = 2.3f;

    void Awake()
    {
        rockHolders = GameObject.FindGameObjectsWithTag("RockHolder");

        for (int i = 0; i < rockHolders.Length; i++)
        {
            Vector3 temp = rockHolders[i].transform.position;
            temp.y = Random.Range(rockMin, rockMax);
            rockHolders[i].transform.position = temp;
        }

        lastRocksX = rockHolders[0].transform.position.x;

        for (int i = 1; i < rockHolders.Length; i++)
        {
            if (lastRocksX < rockHolders[i].transform.position.x)
            {
                lastRocksX = rockHolders[i].transform.position.x;
            }

        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        Debug.Log("collided with " + target);
        if (target.tag == "RockHolder")
        {
            Vector3 temp = target.transform.position;
            temp.x = lastRocksX + distance;
            temp.y = Random.Range(rockMin, rockMax);
            target.transform.position = temp;
            lastRocksX = temp.x;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
