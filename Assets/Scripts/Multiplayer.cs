using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplayer : MonoBehaviour
{
    public int count = 0;
    public GameObject Obj1;
    public GameObject Obj2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (count % 2 == 0)
        {
            Obj1.SetActive(true);
            Obj2.SetActive(false);
        }
        else
        {
            Obj1.SetActive(false);
            Obj2.SetActive(true);
        }
    }
}
