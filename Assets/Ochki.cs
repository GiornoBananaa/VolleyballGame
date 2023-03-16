using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ochki : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _ball;
    public float myTime = 5f;
    float timeleft;
   
    private void Start()
    {
        _ball = GetComponent<Rigidbody2D>();
        timeleft = myTime;
    }
    private void Update()
    {
      if(timeleft > 0)
        {
            timeleft -= Time.deltaTime;
        }
        else
        {
            _ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "FirstZone")
        {
            _ball.position = new Vector2(6.94f, 2.68f);
            _ball.velocity = new Vector2(0, 0);
        }
        else if(collision.gameObject.name == "SecondZone")
        {
            _ball.position = new Vector2(-4.1f, 2.68f);
            _ball.velocity = new Vector2(0, 0);
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name == "FirstZone")
    //    {
    //        _ball.position = new Vector2(6.94f, 2.68f);
    //        _ball.velocity = new Vector2(0,0);
    //    }
    //    else if (collision.gameObject.name == "SecondZone")
    //    {
    //        _ball.position = new Vector2(-4.1f, 2.68f);
    //        _ball.velocity = new Vector2(0, 0);
    //    }
    //}
}
