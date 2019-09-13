using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarField : MonoBehaviour
{
    //List<GameObject> stars = new List<GameObject>();
    Vector3 size = new Vector3(35, 65, 0);

    // Start is called before the first frame update
    void Start()
    {

        GameObject parent = this.gameObject;

        Object obj = (GameObject)Resources.Load("Star");

        for (int i=0; i< 20; i++)
        {
            GameObject o = (GameObject)Object.Instantiate(obj);
            o.transform.parent = parent.transform;

        }
        RandomlyPlaceStars();

    }

    void RandomlyPlaceStars()
    {
        Camera c = Camera.main;

        for (int i=0; i<transform.childCount; ++i)
        {
            Transform st = transform.GetChild(i);

            Vector3 pos = new Vector3(Random.value * size.x - size.x / 2 + c.transform.position.x,
                Random.value * size.y - size.y / 2 + c.transform.position.y, 0.0f);
            st.position = pos;
        }

    }
    // Update is called once per frame
    void Update()
    {

        Vector3 localCamPosition = Camera.main.transform.position - transform.position;
        Camera c = Camera.main;

        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform st = transform.GetChild(i);
            Vector3 pos = st.position - c.transform.position + size/2;

            //optimize here
            pos.x = pos.x % size.x;
            if (pos.x < 0)
            {
                pos.x += size.x;
            }
            pos.y = pos.y % size.y;
            if (pos.y < 0)
            {
                pos.y += size.y;
            }

            pos = pos + c.transform.position - size/2;
            pos.z = 0.0f;

            st.position = pos;
        }

    }
}
