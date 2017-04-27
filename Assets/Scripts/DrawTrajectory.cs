using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawTrajectory : MonoBehaviour {
    public GameObject dots;
    //public GameObject player;

    public GameObject ball;
    private bool isPressed, isBallThrown;
    private float power = 25;
    private int numOfDots = 20;
    private List<GameObject> trajectoryPoints;

    private Renderer renderer2D;
   

    private void Start() {
        trajectoryPoints = new List<GameObject>();
        isPressed = isBallThrown = false;

        
        //instantiate dots
        for (int i = 0; i < numOfDots; i++) {
            GameObject dotObj = (GameObject)Instantiate(dots);
            
            dotObj.GetComponent<Renderer>().enabled = false;
            trajectoryPoints.Insert(i, dotObj);
        }

        
    }

    private void Update() {
        if (isBallThrown) {
            return;
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    isPressed = true;

        //    if (!ball)
        //        CreateBall();
        //}

        if (Input.GetMouseButtonDown(0)) {
            Vector3 vel = GetForceFrom(ball.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
            SetTrajectory(transform.position, vel / ball.GetComponent<Rigidbody2D>().mass);
            }
    }

    private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y));

    }

    //private void CreateBall() {
    //    ball = (GameObject)Instantiate(ball);
    //    Vector3 pos = transform.position;
    //    pos.z = 1;
    //    ball.transform.position = pos;
    //    ball.SetActive(false);
    //}

    private void SetTrajectory(Vector3 pStartPosition, Vector3 pVelocity) {
        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;

        fTime += 0.1f;

        for (int i = 0; i < numOfDots; i++) {
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
            Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, 2);
            //Debug.Log(dx +" + "+ dy + " + " + velocity);
            //trajectoryPoints[i].renderer.position = pos; - obsolete
            trajectoryPoints[i].transform.position = pos;
            trajectoryPoints[i].GetComponent<Renderer>().enabled = true;
            trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            fTime += 0.1f;
        }
    }
}
