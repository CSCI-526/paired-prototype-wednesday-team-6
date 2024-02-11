using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollect : MonoBehaviour
{

    public int fireElementCount = 0;
    public int waterElementCount = 0;

    public TextMeshProUGUI fireCountText;
    public TextMeshProUGUI waterCountText;

    public GameObject fireElementProjectilePrefab;
    public GameObject waterElementProjectilePrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FireElement"))
        {
            fireElementCount++;
            fireCountText.text = "Fire element count: " + fireElementCount;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("WaterElement"))  
        {
            waterElementCount++;
            waterCountText.text = "Water element count: " + waterElementCount;
            Destroy(other.gameObject);
        }
    }

    public void ShootElement(string elementType)
    {
        GameObject projectilePrefab = elementType == "Fire" ? fireElementProjectilePrefab : waterElementProjectilePrefab;
        int elementCount = elementType == "Fire" ? fireElementCount : waterElementCount;

        if (elementCount > 0)
        {
         
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Shooting>().elementType = elementType; 

    
            if (elementType == "Fire")
            {
                fireElementCount--;
                fireCountText.text = "Fire element count: " + fireElementCount;
            }
            else if (elementType == "Water")
            {
                waterElementCount--;
                waterCountText.text = "Water element count: " + waterElementCount;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ShootElement("Fire"); 
                                  
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            ShootElement("Water");

        }
    }



}
