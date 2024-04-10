using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    private int multiplier;
    GameObject player;
    [SerializeField] private PlayerStats playerStats;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        multiplier = Random.Range(10, 20);
        Debug.Log("GameObject: " + player.name + "\nCoordinates: (" + player.transform.position.x + "," + player.transform.position.y + "," + player.transform.position.z + ")");
        
    }

    public void FixedUpdate()
    {
        transform.Translate(new Vector3(0f, 0f, -(0.1f*multiplier)));
        transform.Rotate(new Vector3(0f, 0f, 10f));

        if (transform.position.z < -10f) {
            Destroy(gameObject);
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 5f)
        {
            Destroy(gameObject);
            playerStats.shipSpeed++;
        }
    }
}
