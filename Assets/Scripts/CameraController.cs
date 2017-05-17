using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class CameraController : MonoBehaviour {

    public PlayerControl playerControl;
    public GameObject target;
    public Transform leftBound;
    public Transform rightBound;
    public float resetVel;
    public float resetVelSqr;
  //  public float targetsVel;
    public Rigidbody2D rigid;
    public SpringJoint2D spring;
    public float focusVel;
    public float smoothing = 0.5f;
    public float minY;
    public float swipeResistance = 200f;

    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float distance = -10.0f;
    private float sensitivityX = 0.0f;
    private float sensitivityY = 0.0f;
    public float speed = 0.05f;
    private Vector2 touchPosition;

    public bool ammoIsClicked;

    public GameObject cam;

    private Vector2 touchDeltaPosition;



    private void Start()
    {
        
        playerControl = GameObject.Find("SlingFront").GetComponent<PlayerControl>();
        resetVelSqr = resetVel * resetVel;
        ammoIsClicked = false;
    }
    //private void FixedUpdate()
    //{
    //    FocusOnAmmo();
    //}

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            
            if (hit.collider != null && hit.collider.name == "AmmoRange") // or &&hit.collider.tag == "Ammo"
            {
                //do nothing
            }

            else
            {
                //camera Control
                //Do Nothing

                if (ammoIsClicked == false && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    // touchPosition += Input.GetTouch(0).position;
                    touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                    Debug.Log("left: " + leftBound.transform.position.x + "rightBound: " + rightBound.transform.position.x);

                    //touchDeltaPosition.x = Mathf.Clamp(transform.position.x, leftBound.position.x, rightBound.position.x);

                    //if (transform.position.x < leftBound.transform.position.x)
                    //{
                    //    touchDeltaPosition.x = leftBound.transform.position.x;

                    //}
                    //else if (transform.position.x > rightBound.transform.position.x)
                    //{
                    //    touchDeltaPosition.x = rightBound.transform.position.x;

                    //}
                   //transform.position = new Vector3(Mathf.Clamp(cam.transform.position.x, leftBound.transform.position.x, rightBound.transform.position.x),0f,-10f);
                   //// if(transform.position.x > leftBound.transform.position.x && transform.position.x < rightBound.transform.position.x)
                        //transform.Translate(-touchDeltaPosition.x * speed, minY, 0);

                    

                    // Vector2 pos = Vector2.Lerp((Vector2) transform.position, (Vector2)target.transform.position, Time.fixedDeltaTime)
                    Debug.Log("val: " + touchDeltaPosition.x);
                }

            }
        }
        else if (Input.touchCount == 2)
        {
            //Vector2 touchOne = Input.GetTouch(0).position;
            //Vector2 touchTwo = Input.GetTouch(1).position;

            //float lastTouchMag = touchOne.x - touchTwo.x;



        }
    }
    private void LateUpdate()
    {
        // transform.position = new Vector3(touchPosition.x* speed * Time.deltaTime, 0f, -10f);
        transform.position = new Vector3(Mathf.Clamp(cam.transform.position.x, leftBound.transform.position.x, rightBound.transform.position.x), 0f, -10f);
        transform.Translate(-touchDeltaPosition.x * speed, minY, 0);

    }

    //private void FocusOnAmmo()
    //{
    //      if(target != null && spring == null) 
    //      {
            
    //        Vector3 newPosition = transform.position;
    //        newPosition.x = target.transform.position.x;
            
    //        newPosition.x = Mathf.Clamp(newPosition.x, leftBound.position.x, rightBound.position.x);
    //        transform.position = Vector3.Lerp(transform.position, newPosition, smoothing * Time.deltaTime);
    //        transform.position = newPosition;
    //        if (transform.position.y < minY)
    //        {
    //            transform.position = new Vector3(transform.position.x, minY, -10f);
    //        }
    //       }
    //}

    public void SetTarget(GameObject ammo)
    {
        target = ammo;
        spring = target.GetComponent<SpringJoint2D>();
        rigid = target.GetComponent<Rigidbody2D>();

       // transform.position = new Vector3 (target.transform.position.x, minY, -10f);
    }

}
