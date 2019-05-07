using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllers
{
    public class IController
    {
        public virtual void Start(Ship p)
        { 
        }

        public virtual void Stop()
        {

        }
        public virtual void Update(float dt)
        { 
        }
    }

    public class GameController : IController
    {
        float Time;
        Ship ship = null;
        float rotSpeed = 0.0f;

        float mousePrevPos = 0.0f;

        FloatingJoystick stick = null;

        public override void Start(Ship p)
        {
            ship = p;

            ship.PhysicsOn(true);

            ship.PutToStart();
            ship.SetMainText("");
            mousePrevPos = Input.mousePosition.x;

            stick = GameObject.Find("stick").GetComponent<FloatingJoystick>();
 
        }

        public override void Stop()
        {
            ship.SetMainText("");
            ship.PhysicsOn(false);

        }
        public override void Update(float dt)
        {
            /*
             * mouse movement - rotation
             * button - thrust           
             * * */

            //do the rotation
            float rotate = (mousePrevPos - Input.mousePosition.x);
            Vector2 sd = stick.Direction;
            float deadZoneStick = 0.2f;
            if (sd.x < -deadZoneStick || Input.GetKey(KeyCode.A))
            {
                rotate = 4.0f;
            }
            if (sd.x > deadZoneStick || Input.GetKey(KeyCode.D))
            {
                rotate = -4.0f;
            }

            rotSpeed += rotate;

            float maxRotSpeed = 30.0f;
            if (rotSpeed > maxRotSpeed)
                rotSpeed = maxRotSpeed;
            if (rotSpeed < -maxRotSpeed)
                rotSpeed = -maxRotSpeed;


            ship.transform.Rotate(new Vector3(0.0f, 0.0f, rotSpeed * dt));
            //slowly decrease rotation speed
            rotSpeed = rotSpeed - rotSpeed * dt;

            //ship.body.rotation sAddTorque(rotSpeed);

            //ship.body.AddForceAtPosition(new Vector2(rotate, 0), new Vector2(0, 0));
            mousePrevPos = Input.mousePosition.x;

            //the up force
            float forceMul = 0.0f;
            if (sd.y > deadZoneStick || Input.GetKey(KeyCode.W))
            {
                forceMul = 0.5f;
            }

            Vector2 force = ship.transform.rotation * Vector2.up * forceMul;
            //ship.body.AddForce(force);

            ship.SetThrust(force);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ship.StartGame();
            }
        }

    }
    public class ControllerPreStart : IController
    {
        float Time;
        Ship ship = null;
        public override void Start(Ship p)
        {
            ship = p;

            ship.PutToStart();
            ship.SetMainText("");
            ship.SetMainText("Press to start");

        }

        public override void Stop()
        {
            ship.SetMainText("");

        }
        public override void Update(float dt)
        {
            //start countdown
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                ship.StartGame();

            }
        }

    }


}
