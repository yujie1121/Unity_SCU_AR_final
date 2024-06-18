using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField, Header("要生成的預製物")]
    private GameObject prefabToSpawn;
    [SerializeField]
    private Transform spawnParent;
    [SerializeField] private int maxMushrooms = 3; 
    [SerializeField] private float lockoutTime = 5f; 

    private int currentMushrooms = 0;
    private Camera mainCamera;
    private bool isLocked = false; 


    private void Start()
    {
        mainCamera = Camera.main;
        // UpdateMushroomCountText();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isLocked && currentMushrooms < maxMushrooms)
        {
            SpawnMushroom();
            currentMushrooms++;
            // UpdateMushroomCountText();

            if (currentMushrooms >= maxMushrooms)
            {
                StartCoroutine(Lockout());
            }
        }
    }

    private void SpawnMushroom()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Instantiate(prefabToSpawn, hit.point, Quaternion.identity, spawnParent);
        }
    }

    private IEnumerator Lockout()
    {
        isLocked = true;
        yield return new WaitForSeconds(lockoutTime);
        currentMushrooms = 0;
        isLocked = false;
        // UpdateMushroomCountText();
    }

    /*
    private void UpdateMushroomCountText()
    {
        if (mushroomCountText != null)
        {
            mushroomCountText.text = isLocked
                ? "Mushroom spawning locked!"
                : $"Mushrooms: {currentMushrooms}/{maxMushrooms}";
        }
    }
    */
}
