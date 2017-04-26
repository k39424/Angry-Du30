using UnityEngine;
using System.Collections;

public class DrawTrajectory : MonoBehaviour {
    public PlayerShooting playerShooting;

    public Transform startPos;
    public float Vi;
    public float totalD;
    public float displaceX;
    public float displaceY;
    public GameObject dots;
    public GameObject relativePos;
    public float nextDot;
    public float dotRate;
  
    public float time;

    private void GetDisplacement() {

        // Instantiate(dots, relativePos.transform.position, Quaternion.identity);
        if (Time.time > nextDot)
        {
            for (time = 1; time < 5; time++)
            {
               
                nextDot += dotRate;
                displaceX = Vi * time;
                displaceY = Vi * time + (0.5f * Physics.gravity.magnitude * (time * time));
                
                Instantiate(dots, new Vector3(displaceX, displaceY), Quaternion.identity);
                Debug.Log(displaceX + "and" + displaceY);
                }
            }
    }

    private void Update() {
        GetDisplacement();
    }
}
