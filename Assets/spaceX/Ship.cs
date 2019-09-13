using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
 
    public Text topScoreText = null;
    public Text scoreText = null;
    public Text mainText = null;
    public Text notesText = null;
    public Transform spawn = null;
    public GameObject spaceShip = null;
    public ParticleSystem flame = null;

    Controllers.IController control = null;
    public Rigidbody2D body = null;

    // Start is called before the first frame update
    void Start()
    {
        PhysicsOn(true);

        topScoreText = GameObject.Find("TOPSCORE").GetComponent<Text>();
        scoreText = GameObject.Find("SCORE").GetComponent<Text>();
        mainText = GameObject.Find("MAINTEXT").GetComponent<Text>();
        notesText = GameObject.Find("NOTES").GetComponent<Text>();

        spawn = GameObject.Find("Spawn").transform;
        spaceShip = GameObject.Find("SpaceShip");

        body = spaceShip.GetComponent<Rigidbody2D>();
        flame = spaceShip.GetComponentInChildren<ParticleSystem>();

        SetController(new Controllers.ControllerPreStart());
    }

    public void StartGame()
    {
        SetController(new Controllers.GameController());
    }

    public void PhysicsOn(bool b)
    {
        //body.
        //Physics.autoSimulation = b;
    }

    public void SetMainText(string v)
    {
        mainText.text = v;
    }

    public void SetThrust(Vector2 force)
    {
        body.AddForce(force);
        ParticleSystem.EmissionModule m = flame.emission;
        m.enabled = (force.SqrMagnitude()) > 0.05f;



    }

    void SetController(Controllers.IController c)
    {
        if (control != null) 
        {
            control.Stop();
            control = null;
        }
        control = c;
        c.Start(this);
    }

    public void PutToStart()
    {
        transform.position = spawn.position;
        transform.rotation = spawn.rotation;


        body.velocity = Vector2.zero;

    }

    // Update is called once per frame
    void Update()
    {
        if(control != null) 
        { 
            control.Update(Time.deltaTime); 
        }
        notesText.text = "String\n\rOtherSrting";
    }
}
