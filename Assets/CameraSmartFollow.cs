using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmartFollow : MonoBehaviour
{
    // Start is called before the first frame update
    Ship ship = null;
    Rigidbody2D shiprb = null;
    float fZoom = 0.0f;
    Vector3 initialOffset = Vector3.zero;

    void Start()
    {
    }
    private void Awake()
    {
        ship = GameObject.Find("SpaceShip").GetComponent<Ship>();
        shiprb = ship.GetComponent<Rigidbody2D>();
        initialOffset = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Find Movement goal and slightly move camera in that direction
         * Also zoom out when the speed increases and zoom in if the ship is idling
         * */


        //ship
        Vector3 target = ship.transform.position + new Vector3(shiprb.velocity.x, shiprb.velocity.y, 0.0f) * 0.1f;
        fZoom = (shiprb.velocity.magnitude - fZoom) * Time.deltaTime + fZoom ;

        Camera.main.transform.position = initialOffset + target + new Vector3(0,0,-fZoom);


    }
}
