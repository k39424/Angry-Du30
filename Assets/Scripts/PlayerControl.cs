using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public GameObject slingShot;
    public Rigidbody2D myRigid;
    public LineRenderer slingBack;
    public LineRenderer slingFront;

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
    public float fTime;
    private Vector2 startPos;
    private Ray ammoToSling;
    public float hSpeed;
    public float vSpeed;
    public GameObject dotObj;

    public CameraController cameraController;

    private bool clicked;

    private void Start()
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
       
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClick();

        if (Input.GetMouseButtonUp(0))
            OnMouseUp();

        if (clicked == true)
            Dragging();

        LineRendererUpdate();

        if (spring != null)
        {
            if (ammoRigid.isKinematic == false && ammoRigid.velocity.sqrMagnitude < prevVelocity.sqrMagnitude)
            {
                Destroy(spring);
                ammoRigid.velocity = prevVelocity;
                Debug.Log("Must disable input here");
            }

            else if (!clicked)
                prevVelocity = ammoRigid.velocity;

        }
        else
        {
            slingFront.enabled = false;
            slingBack.enabled = false;
        }

        if (spring == null && ammoRigid.velocity.sqrMagnitude < resetVelSqr)
            Reload();
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
        spring.enabled = true;
        ammoRigid.isKinematic = false;
    }

    private void Dragging()
    {

        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 slingToMouse = mousePoint - slingShot.transform.position;

        if (slingToMouse.sqrMagnitude > maxLengthSqr)
        {
            rayToMouse.direction = slingToMouse;
            mousePoint = rayToMouse.GetPoint(maxLength);
        }

        mousePoint.z = 0;
        ammo.transform.position = mousePoint;
        ammoToSling.origin = mousePoint;
       // DrawTrajec();
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

        slingBack.SetWidth(0.2f, 0.1f);
        slingFront.SetWidth(0.2f, 0.1f);
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

    private void DrawTrajec()
    {
        vi = prevVelocity.magnitude;

        ammoToSling.direction = slingShot.transform.position - ammo.transform.position;
        startPos = ammoToSling.direction;
        Debug.DrawRay(ammoToSling.origin, ammoToSling.direction);

        angle = Mathf.Atan2(ammoToSling.direction.y, ammoToSling.direction.x) * Mathf.Rad2Deg;


        for (float i = 0.5f; i < 3f; i += 0.5f)
        {
            hSpeed = startPos.x + ( Mathf.Cos(angle) * vi * i);

            vSpeed = startPos.y + (Mathf.Sin(angle) * vi - (0.5f * Physics.gravity.magnitude * i * i));


             Instantiate(dotObj, new Vector3(hSpeed, vSpeed, 0f), Quaternion.identity);

        }
    }
}