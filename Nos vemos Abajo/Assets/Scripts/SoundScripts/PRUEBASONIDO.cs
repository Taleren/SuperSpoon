using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRUEBASONIDO : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento
    public float distance = 5f; // Distancia máxima de desplazamiento

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        SoundManager.instance.PlaySound("ventisca", transform.position);
    }
    void Update()
    {
        // Movimiento continuo de un lado a otro usando Mathf.PingPong
        //float movement = Mathf.PingPong(Time.time * speed, distance);
        //transform.position = startPosition + new Vector3(0, 0, movement);

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SoundManager.instance.PlaySound("vientoFuerte", transform.position, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            SoundManager.instance.PlaySound("cogerFarol", transform.position, gameObject);
        }
    }
}
