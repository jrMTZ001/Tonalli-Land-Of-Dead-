using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    
    private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float xPosition;
    private float lenght;
    
    /*
    private Transform theCam;
    public Transform sky, treeline;
    [Range(0f, 1f)]
    public float parallaxSpeed;
    */
    // Start is called before the first frame update
    void Start()
    {   
        
        cam = GameObject.Find("Main Camera");
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
        
        /*
        theCam = Camera.main.transform;
        */
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);
        if(distanceMoved > xPosition + lenght)
        {
            xPosition = xPosition + lenght;
        }
        else if(distanceMoved < xPosition - lenght)
        {
            xPosition = xPosition - lenght;
        }
        
        /*
        sky.position = new Vector3(theCam.position.x, theCam.position.y, sky.position.z);
        treeline.position = new Vector3(theCam.position.x * parallaxSpeed, theCam.position.y, treeline.position.z);
        */
    }
}
