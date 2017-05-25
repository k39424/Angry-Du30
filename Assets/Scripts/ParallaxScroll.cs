using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour {

    private Camera camera;
    private Vector3 previousCameraTransform;
    public float speed, parallaxFactor;
    Vector3 delta;

    private void Start()
    {
        camera = Camera.main;
        parallaxFactor = speed;
    }

    private void Update()
    {
        delta = camera.transform.position - previousCameraTransform;

        delta.y = 0; delta.z = 0;
       // transform.position = Vector3.Lerp(transform.position, (delta/parallaxFactor), Time.deltaTime * 9f);
       transform.position += (delta / parallaxFactor * Time.deltaTime) ;
        previousCameraTransform = camera.transform.position;
        parallaxFactor = speed;
    }
   
}
