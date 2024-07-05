using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField, Header("要生成的預製物")]
    private GameObject prefabToSpawn;
    [SerializeField]
    private Transform spawnParent;
    [SerializeField] private int maxMushrooms = 3;
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private float lockoutTime = 5f; 

    private int currentMushrooms = 0;
    private Camera mainCamera;
    private bool isLocked = false; 


    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isLocked && currentMushrooms < maxMushrooms)
        {
            SpawnMushroom();
            currentMushrooms++;

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
        countdownTimer.gameObject.SetActive(true);
        countdownTimer.Start();
        yield return new WaitForSeconds(lockoutTime);
        currentMushrooms = 0;
        isLocked = false;
    }
}
