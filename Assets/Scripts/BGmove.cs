using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BGmove : MonoBehaviour
{
    private float speed;
    private GameManager scripts;
    private float initialSpeed = 9f;
    private float speedIncreaseRate = 0.01f;
    private float elapsedTime = 0f;

    void Start()
    {
        speed = initialSpeed;
        scripts = Camera.main.GetComponent<GameManager>();
    }

    void Update()
    {
        if (!scripts.GetCanCreate)
        {
            elapsedTime += Time.deltaTime;
            speed = initialSpeed + (elapsedTime * speedIncreaseRate);
        }

        if (transform.position.x >= -17.5f)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else
        {
            scripts.GetCanCreate = true;
            Destroy(gameObject);
        }
    }
}

