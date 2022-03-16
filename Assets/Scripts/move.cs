using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class move : MonoBehaviour
{
    [SerializeField]
    Slider Strikerproxy;
    public float speed = 0.1F;
    private Rigidbody2D rb;
    public LineRenderer Line;
    private Transform selftransform;
    CircleCollider2D coll;
    Vector2 direction;
    Vector3 Mousepos;
    Vector3 Mousepos2;
    Vector2 startpos;
    Transform Arrowtransorm;
    public GameObject Arrowdir;
    bool hasstriked = false;
    bool positionset = false;
    public GameObject  Board;
    bool No = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
        //Strikerproxy.onValueChanged.AddListener(StikerXpos);
        selftransform = transform;
        startpos = transform.position;
        Arrowtransorm = Arrowdir.transform;
    }
    // Update is called once per frame
    void Update()
    {
        Arrowdir.SetActive(false);
        Line.enabled = false;
        Mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 inverseMouPos = new Vector3(Screen.width - Input.mousePosition.x, Screen.height - Input.mousePosition.y, Input.mousePosition.z);
        Mousepos2 = Camera.main.ScreenToWorldPoint(inverseMouPos);
        Mousepos2.y = Mousepos2.y - 3;
        
        if (selftransform.position.x != 0)
        {
            Mousepos2.x = Mousepos2.x + (selftransform.position.x * 2);
        }

        if (!hasstriked && !positionset)
        {
            coll.isTrigger = true;
            selftransform.position = new Vector2(Strikerproxy.value, startpos.y);
        }

        if (Input.GetMouseButtonUp(0) && rb.velocity.magnitude == 0 && positionset)
        {
            StrikerMove();

        }
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (Input.GetMouseButtonDown(0) && No)
                {
                    if (!positionset)
                    {
                        positionset = true;
                    coll.isTrigger = false;
                    }
                }
            }
            if (positionset && rb.velocity.magnitude == 0)
            {
                Arrowdir.SetActive(true);
                Line.enabled = true;
                Line.SetPosition(0, selftransform.position);
                Line.SetPosition(1, Mousepos2);
            float angle = AngleBetweenTwoPoints(Arrowtransorm.position, Mousepos2);
            Arrowtransorm.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle  +90));
            }
     
    }
    public void StrikerMove()
    {
        float x = 0;
        if (positionset && rb.velocity.magnitude == 0)
        {
            x = Vector2.Distance(transform.position, Mousepos);
        }
        direction = (Vector2)(Mousepos2 - transform.position);
        direction.Normalize();
        rb.AddForce(direction * x * 300);
        hasstriked = true;
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cointag")
        {
            No = false;
            print("Striker overlapstoken");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cointag")
        {
            No = true;
        }
        if (collision.gameObject.tag == "Holes")
        {
            Strikerrest();
        }
    }
    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < 0.1f && rb.velocity.magnitude != 0)
        {
            Strikerrest();
            Board.GetComponent<Multiplayer>().count++;
        }
    }
    public void Strikerrest()
    {
        rb.velocity = Vector2.zero;
        hasstriked = false;
        positionset = false;
        Line.enabled = true;
    }
    
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

}