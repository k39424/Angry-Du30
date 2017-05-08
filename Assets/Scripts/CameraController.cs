using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class CameraController : MonoBehaviour {

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


    private void Start()
    {
        resetVelSqr = resetVel * resetVel;
    }
    private void FixedUpdate()
    {
        FocusOnAmmo();
    }


    private void FocusOnAmmo()
    {
          if(target != null && spring == null) 
          {
            
            Vector3 newPosition = transform.position;
            newPosition.x = target.transform.position.x;
            
            newPosition.x = Mathf.Clamp(newPosition.x, leftBound.position.x, rightBound.position.x);
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothing * Time.deltaTime);
            transform.position = newPosition;
            if (transform.position.y < minY)
            {
                transform.position = new Vector3(transform.position.x, minY, -10f);
            }
           }
    }

    public void SetTarget(GameObject ammo)
    {
        target = ammo;
        spring = target.GetComponent<SpringJoint2D>();
        rigid = target.GetComponent<Rigidbody2D>();

        transform.position = new Vector3 (target.transform.position.x, minY, -10f);
    }

}
