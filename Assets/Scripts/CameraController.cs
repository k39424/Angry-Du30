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
        rigid = target.GetComponent<Rigidbody2D>();
        resetVelSqr = resetVel * resetVel;
        spring = target.GetComponent<SpringJoint2D>();
    }
    private void FixedUpdate()
    {
        FocusOnAmmo();
    }


    private void FocusOnAmmo()
    {
        //if (rigid != null && rigid.velocity.sqrMagnitude > focusVel * focusVel)
          if(target != null && spring == null) 
          {
            Vector3 newPosition = transform.position;
            newPosition.x = target.transform.position.x;
            newPosition.x = Mathf.Clamp(newPosition.x, leftBound.position.x, rightBound.position.x);
            //transform.position = Vector3.Lerp(transform.position, newPosition, smoothing * Time.deltaTime);
            transform.position = newPosition;
            if (transform.position.y < minY)
            {
                transform.position = new Vector3(transform.position.x, minY, transform.position.z);
            }
 
                if (spring == null && rigid.velocity.sqrMagnitude < resetVelSqr)
                {
                    ResetTarget();
                }
           }
    }

    public void ResetTarget()
    {
                string sceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(sceneName);
    }

    public void SetTarget(GameObject ammo)
    {
        if (target == null)
        {
            target = ammo;
        }
    }

}
