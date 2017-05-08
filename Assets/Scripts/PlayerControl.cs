using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public GameObject slingShot;
    public Transform slingTrans;
    public Rigidbody2D myRigid;
    public LineRenderer slingBack;
    public LineRenderer slingFront;
    public BoxCollider2D slingBox;

    public GameObject ammoPrefab;
    public GameObject ammo;
    public Transform ammoRespawn;
    public SpringJoint2D spring;
    public Rigidbody2D ammoRigid;

    public float ammoCount;
    public float maxAmmo;
    public float maxLength;
    public float maxLengthSqr;
    public Vector2 prevVelocity;
    private Ray rayToMouse; //for limiting length
    private Ray leftSlingToAmmo; // for rendering lines from slingshot to end of circle;

    public float resetVel;
    private float resetVelSqr;

    public float angle;
    public float vi;

    Vector3 mousePoint;

    //private Vector2 startPos;
    private Ray ammoToSling;
    
    public GameObject dotObj;

    public List<GameObject> trajectDots;
    public float numOfDots;
    public CameraController cameraController;
    Vector2 initVel;
    private bool clicked;

    private void Awake()
    {
        slingShot = this.gameObject;
        slingFront = GetComponent<LineRenderer>();
        //  slingFront = GetComponentInChildren<LineRenderer>();
        //getComponentInchildren doesnt work, so I edited its value in editor

        myRigid = GetComponent<Rigidbody2D>();

        maxLengthSqr = maxLength * maxLength;
        ammoCount = maxAmmo;
        Reload();

        if (resetVel == 0)
            resetVel = 0.3f;

        resetVelSqr = resetVel * resetVel;

        if (ammo == null)
        {
            ammo = GameObject.FindWithTag("Ammo");
        }

        ammoToSling = new Ray(ammo.transform.position, slingShot.transform.position - ammo.transform.position);

        //rayToMouse = new Ray(slingShot.transform.position, Vector3.zero);
        //leftSlingToAmmo = new Ray(slingFront.transform.position, Vector3.zero);

        //instantiate dots gameObj to list
        for (int i = 0; i < numOfDots; i++)
        {
            GameObject dot = (GameObject)Instantiate(dotObj);
            dot.SetActive(false);
            trajectDots.Insert(i, dot);
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            OnClick();


        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
            //for (int i = 0; i < numOfDots; i++)
            //{
            //    trajectDots[i].SetActive(false);
            //}
        }
#else
            If(Input.GetTouch(0).position)
            OnClick();
#endif




        if (clicked == true)
        {
            Dragging();

            LineRendererUpdate();
        }
           

       
        
        if (spring == null && ammoRigid.velocity.sqrMagnitude < resetVelSqr)
            Reload();
    }

    private Vector2 GetForceFrom(Vector2 toPos, Vector2 fromPos)
    {//Get Force From: ToPosition - FromPosition * power
     //return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * (new Vector2(ammoToSling.direction.x - ammoToSling.origin.x, ammoToSling.origin.y - ammoToSling.origin.y).magnitude) * 3f/ammoRigid.mass;
        Vector2 velDir = new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y);
       
        return (velDir) * slingShot.transform.InverseTransformPoint(ammo.transform.position).magnitude;
       
        
    }

    public void Launch()
    {
        
            //if (ammoRigid.isKinematic == false && ammoRigid.velocity.sqrMagnitude < initVel.sqrMagnitude)
            //{
                //Destroy(spring);
                Debug.LogWarning("initVel: " + initVel);
                ammoRigid.velocity = initVel;
        //if (slingShot.transform.position.x < ammoRigid.transform.position.x)
        //    ammoRigid.AddForce(initVel,ForceMode2D.Force);
        // ammoRigid.AddForce(GetForceFrom(ammoRigid.transform.position, slingShot.transform.position));

        //Destroy(spring);

        //ammoRigid.AddForce(initVel,ForceMode2D.Force);
        // ammoRigid.velocity = prevVelocity;
        // ammoRigid.AddForce(GetForceFrom(slingShot.transform.position, ammoRigid.transform.position));


        //}

        //else if (!clicked)
        //    ammoRigid.AddForce(initVel, ForceMode2D.Impulse);
        //prevVelocity = ammoRigid.velocity;
        // ammoRigid.AddForce(GetForceFrom(slingShot.transform.position, ammoRigid.transform.position) );//no forceMode2d





        //else
        //{
        //    slingFront.enabled = false;
        //    slingBack.enabled = false;
        //}

    }

    public void Reload()
    {
        if (ammoCount < 0)
            return;

        ammo = (GameObject)Instantiate(ammoPrefab, ammoRespawn.position, Quaternion.identity);
        ammoCount -= 1;
        SetAmmo();
    }

    private void SetAmmo()
    {
        if (ammo == null)
            return;

        spring = ammo.GetComponent<SpringJoint2D>();
        ammoRigid = ammo.GetComponent<Rigidbody2D>();
        spring.connectedBody = myRigid.GetComponent<Rigidbody2D>();
        cameraController.SetTarget(ammo);

        rayToMouse = new Ray(slingShot.transform.position, Vector3.zero);
        leftSlingToAmmo = new Ray(slingFront.transform.position, Vector3.zero);
        
        ammoRigid.isKinematic = true;
        LineRendererSetUp();
        LineRendererUpdate();

    }

    private void OnClick()
    {
        clicked = true;
        spring.enabled = false;

    }
    private void OnMouseUp()
    {
        clicked = false;

        if(spring != null)
            spring.enabled = true;

        ammoRigid.isKinematic = false;
        LineRendererUpdate();
        StartCoroutine(ReleaseDelay());
    }

    private void Dragging()
    {//have to clamp angles
        
       mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 slingToMouse = mousePoint - slingShot.transform.position;
        angle = Mathf.Atan2(mousePoint.x, mousePoint.y) * Mathf.Rad2Deg;

        //if (angle < 70)
        //{
        //    float angleX = Mathf.Cos(angle) * slingToMouse.magnitude;
        //    float angleY = Mathf.Sin(angle) * slingToMouse.magnitude;

        //    mousePoint = new Vector3(angleX, angleY, 0f);
        //}
        //for clamping angles
      //  float mouseAngle = Mathf.Atan2(mousePoint.y, mousePoint.x) * Mathf.Rad2Deg;
        if (slingToMouse.sqrMagnitude > maxLengthSqr)
        {
            rayToMouse.direction = slingToMouse;
            mousePoint = rayToMouse.GetPoint(maxLength);
        }

        mousePoint.z = 0;
        ammo.transform.position = mousePoint;
        ammoToSling.origin = mousePoint;
        ammoToSling.direction = slingShot.transform.position - ammo.transform.position;

       initVel = GetForceFrom(slingTrans.transform.position, ammoRigid.transform.position);
        Debug.Log("InitVel" + initVel);
       
        DrawTraject(new Vector2(ammoRigid.transform.position.x, ammoRigid.transform.position.y), initVel);
    }
    

    private void LineRendererSetUp()
    {
        slingFront.enabled = true;
        slingBack.enabled = true;
        slingBack.SetPosition(0, slingBack.transform.position);
        slingFront.SetPosition(0, slingFront.transform.position);

        slingBack.sortingLayerName = "ForeGround";
        slingFront.sortingLayerName = "ForeGround";

        slingBack.sortingOrder = 3;
        slingFront.sortingOrder = 1;

        slingBack.startWidth = 0.2f;
        slingBack.endWidth = 0.1f;

        slingFront.startWidth = 0.2f;
        slingFront.endWidth = 0.1f;
    }

    private void LineRendererUpdate()
    {
        Vector2 slingToAmmo = ammo.transform.position - slingFront.transform.position;
        leftSlingToAmmo.direction = slingToAmmo;
        Vector3 holdPoint = leftSlingToAmmo.GetPoint(slingToAmmo.magnitude + 0.31f);
        holdPoint.z = 1;
        slingFront.SetPosition(1, new Vector3(holdPoint.x, holdPoint.y, -1.0f));
        slingBack.SetPosition(1, holdPoint);
    }

    private void DrawTraject(Vector2 pStartPos,Vector2 pVel)
    {
        Debug.Log("pVel.x = " + pVel.x + "/pVel.y = " + pVel.y);
       
        vi = Mathf.Sqrt((pVel.x * pVel.x) + (pVel.y * pVel.y));
     

        Debug.Log("vi = " + vi);
       
       
        angle = Mathf.Atan2(pVel.y, pVel.x) * Mathf.Rad2Deg;
      

        float fTime = 0;
        Debug.Log("angle = " + angle);
        //fTime += 0.06f;

        for (int i = 0; i < numOfDots; i ++)
        {
           float hSpeed = vi * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);

           float vSpeed = vi * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime/2.0f);
            Debug.Log("hSpeed = " + hSpeed);
            Vector3 pos = new Vector3(pStartPos.x + hSpeed, pStartPos.y + vSpeed, 0.0f);
           
            trajectDots[i].transform.position = pos;
            trajectDots[i].SetActive(true);
           trajectDots[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVel.y - (Physics.gravity.magnitude) * fTime, pVel.x) * Mathf.Rad2Deg);
            fTime += 0.06f;
        }
    }

    public IEnumerator ReleaseDelay()
    {

        yield return new WaitForSeconds(.01f);

        Destroy(spring);
       
        
        //yield return new WaitForSeconds(.0f);
        Launch();
    }
}