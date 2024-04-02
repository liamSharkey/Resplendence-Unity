using System.Collections;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;
    public GameObject BurstPrefab;
    public float spawnInterval = 2f;

    private Vector3 cloudPosition;

    public bool doThing = false;

    private void Start()
    {
        // Start the coroutine to spawn clouds
        StartCoroutine(SpawnClouds());
    }

    void Update(){
        if(doThing){
            makeBurst(cloudPosition);
            doThing = false;
        }
    }

    void makeBurst(Vector3 position)
    {
        GameObject burst1 = Instantiate(BurstPrefab, position, Quaternion.identity);
        GameObject burst2 = Instantiate(BurstPrefab, position, Quaternion.identity);
        GameObject burst3 = Instantiate(BurstPrefab, position, Quaternion.identity);
        GameObject burst4 = Instantiate(BurstPrefab, position, Quaternion.identity);
        GameObject burst5 = Instantiate(BurstPrefab, position, Quaternion.identity);
        GameObject burst6 = Instantiate(BurstPrefab, position, Quaternion.identity);
        GameObject burst7 = Instantiate(BurstPrefab, position, Quaternion.identity);
        GameObject burst8 = Instantiate(BurstPrefab, position, Quaternion.identity);

        burst1.transform.eulerAngles = new Vector3(0, 0, 0);
        burst2.transform.eulerAngles = new Vector3(0, 0, 45);
        burst3.transform.eulerAngles = new Vector3(0, 0, 90);
        burst4.transform.eulerAngles = new Vector3(0, 0, 135);
        burst5.transform.eulerAngles = new Vector3(0, 0, 180);
        burst6.transform.eulerAngles = new Vector3(0, 0, 225);
        burst7.transform.eulerAngles = new Vector3(0, 0, 270);
        burst8.transform.eulerAngles = new Vector3(0, 0, 315);

    }

    private IEnumerator SpawnClouds()
    {

        

            yield return new WaitForSeconds(spawnInterval);

            // Instantiate a cloud prefab at the spawner's position
            GameObject cloud = Instantiate(cloudPrefab, transform.position, Quaternion.identity);
            StartCoroutine(Tracker(cloud));
        
    }

    private IEnumerator Tracker(GameObject cloud)
{


    yield return new WaitUntil(() => cloud.GetComponent<Cloud>().popped == true);



    cloudPosition = cloud.transform.position;

    Destroy(cloud);
    doThing =true;

    

    // Start spawning clouds again
    StartCoroutine(SpawnClouds());
}
}
