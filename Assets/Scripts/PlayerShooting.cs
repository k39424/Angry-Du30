using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
    public GameObject ammo;

    public float maxLength = 3.0f;
    public Vector2 prevVelocity;
    public float maxLengthSqr;
    public Transform catapult;
    public LineRenderer cataBack;
    public LineRenderer cataFront;
    public SpringJoint2D spring;
    public Rigidbody2D rigid;
    public float rad;//radius
    public int layerNum;
    
    private Ray rayToMouse;
    private Ray leftCataToProjectile;
    private bool clickedOn;
    public CircleCollider2D circle;


    private void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        catapult = spring.connectedBody.transform;
        rigid = GetComponent<Rigidbody2D>();
        circle = ammo.GetComponent<CircleCollider2D>();
    }
    private void Start()
    {
        LineRendererSetUp();
        rayToMouse = new Ray(catapult.position, Vector3.zero);
        maxLengthSqr = maxLength * maxLength;
        leftCataToProjectile = new Ray(cataFront.transform.position, Vector3.zero);      
        rad = circle.radius;
        
        cataBack.SetColors(Color.black, Color.black);
        cataFront.SetColors(Color.black, Color.black);

        cataFront.SetWidth(0.1f, 0.1f);
        cataBack.SetWidth(0.1f, 0.1f);
    }

    private void Update()
    {
        OnMouseDown();
        OnMouseUp();
        if (clickedOn)
        {
            Dragging();
        }
        LineRendererUpdate();

        if (spring != null)
        {
            if (!rigid.isKinematic && prevVelocity.sqrMagnitude > rigid.velocity.sqrMagnitude)
            {
                Destroy(spring);
                rigid.velocity = 1f * prevVelocity;
            }

            if (!clickedOn)
            {
                prevVelocity = rigid.velocity;
            }
        }
        else
        {
            cataFront.enabled = false;
            cataBack.enabled = false;

        }
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spring.enabled = false;
            clickedOn = true;
        }
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            spring.enabled = true;
            rigid.isKinematic = false;
            clickedOn = false;
        }
    }

    private void LineRendererUpdate()
    {
        Vector2 cataToProjectile = transform.position - cataFront.transform.position;
        leftCataToProjectile.direction = cataToProjectile;
        Vector3 holdPoint = leftCataToProjectile.GetPoint(cataToProjectile.magnitude + 0.31f);
        holdPoint.z = 1;
        cataFront.SetPosition(1, holdPoint);
        holdPoint.z = -1;
        cataBack.SetPosition(1, holdPoint);
    }

    private void LineRendererSetUp()
    {
        cataBack.SetPosition(0, cataBack.transform.position);
        cataFront.SetPosition(0, cataFront.transform.position);

        cataBack.sortingLayerName = "ForeGround";
        cataFront.sortingLayerName = "ForeGround";

        cataBack.sortingOrder = 1;
        cataFront.sortingOrder = 3;
       
    }

    private void Dragging()
    {
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);  
        Vector2 cataToMouse = mousePoint - catapult.position;

        if (cataToMouse.sqrMagnitude > maxLengthSqr)
        {
            rayToMouse.direction = cataToMouse;
            mousePoint = rayToMouse.GetPoint(maxLength);
            float angle = Mathf.Atan2(mousePoint.y, mousePoint.x) * Mathf.Rad2Deg; //Try to clamp rotation of ball

            angle = Mathf.Clamp(angle, -10f, 210f);
            Debug.Log(angle);

        }

        mousePoint.z = 0;
        transform.position = mousePoint;
    }
}
