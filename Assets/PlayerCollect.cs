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

    public GameObject platformPrefab;
    public GameObject player;



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


    public void TryCreatePlatformWithoutShooting()
    {
        Debug.Log("C button pressed - attempting to create platform without shooting.");

        if (fireElementCount > 0 && waterElementCount > 0)
        {
            fireElementCount--;
            waterElementCount--;

            fireCountText.text = "Fire element count: " + fireElementCount;
            waterCountText.text = "Water element count: " + waterElementCount;

            CreateAndRaisePlatform();
        }
        else
        {
            Debug.Log("Not enough elements to create a platform.");
        }
    }

    private void CreateAndRaisePlatform()
    {
        Vector3 referencePosition = player.transform.position;
        Vector3 referenceScale = player.transform.localScale;

        Vector3 spawnPosition = new Vector3(
            referencePosition.x, // X position is the same as the player
            referencePosition.y + 2, // Y position is just above the player
            referencePosition.z  // Z position should be the same as the player for a 2D game
        );

        GameObject platformInstance = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        PlatformScaler scaler = platformInstance.GetComponent<PlatformScaler>();
        if (scaler != null)
        {
            scaler.targetHeight = referenceScale.y / 2;
            scaler.StartGrowing();
        }
        else
        {
            Debug.LogError("PlatformScaler component not found on the instantiated platform!");
        }
    }


    void Update()
    { 

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ShootElement("Fire");

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ShootElement("Water");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            TryCreatePlatformWithoutShooting(); ;
        }

    }


}
