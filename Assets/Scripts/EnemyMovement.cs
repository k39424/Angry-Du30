using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public GameObject enemyObj;
    public Rigidbody2D enemyRigid;
    public float speed;

    private void FixedUpdate() {
        transform.Translate( Vector3.right* Time.deltaTime);
    }
}
