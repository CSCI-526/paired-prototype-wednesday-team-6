using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScaler : MonoBehaviour
{
    public float targetHeight = 6f;
    public float growSpeed = 0.5f;
    private bool isGrowing = false; // Initially not growing

    void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
    }

    void Update()
    {
        if (isGrowing)
        {
            float newHeight = Mathf.MoveTowards(transform.localScale.y, targetHeight, growSpeed * Time.deltaTime);
            transform.localScale = new Vector3(transform.localScale.x, newHeight, transform.localScale.z);

            if (transform.localScale.y >= targetHeight)
            {
                isGrowing = false; 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartGrowing();
        }
    }

    public void StartGrowing()
    {
        isGrowing = true;
    }
}
