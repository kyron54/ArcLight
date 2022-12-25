using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnObject : MonoBehaviour
{
    public float despawnTime;
    public float knockbackForce;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, despawnTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Vector2 direction = collision.transform.position - transform.position;

            collision.GetComponent<Rigidbody2D>().AddForceAtPosition(direction * knockbackForce, transform.position, ForceMode2D.Impulse);
        }
    }
}
