using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb2d;
    private GameObject[] player;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.MovePosition(player[0].transform.position * speed * Time.deltaTime);
    }
}
