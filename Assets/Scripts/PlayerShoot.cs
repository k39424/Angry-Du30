using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {
    public GameObject obj;
    public GameObject gunBarrel;
    public float coolDown;
    public float nextFire;


	private void FixedUpdate () {
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.Normalize();

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, -20f, 90f);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
        MouseClick();
	}

    private void MouseClick() {
        if (Time.time > nextFire){
            if (Input.GetButtonDown("Fire1")){
                Instantiate(obj, gunBarrel.transform.position, transform.rotation);
                nextFire = coolDown + Time.time;
            }
            else{

            }
        }
    }

}
