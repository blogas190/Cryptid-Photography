using UnityEngine;

public class CryptidSpawner : MonoBehaviour
{
    public GameObject[] cryptidPrefabs;
    public BoxCollider[] spawnZones ;
    public int maxCryptids = 1;

    void Start()
    {
        SpawnCryptid();
    } 

    public void SpawnCryptid()
    {
        for(int i = 0; i < maxCryptids; i++)
        {
            Vector3 spawnPoint = GetRandomPositionInZone();
            GameObject cryptid = cryptidPrefabs[Random.Range(0, cryptidPrefabs.Length)];
            Instantiate(cryptid, spawnPoint, Quaternion.identity);
        }
    }

    public Vector3 GetRandomPositionInZone()
    {
        BoxCollider zone = spawnZones[Random.Range(0, spawnZones.Length)];
        Vector3 center = zone.center + zone.transform.position;
        Vector3 size = zone.size;

        float x = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float y = center.y;
        float z = Random.Range(center.z - size.z / 2f, center.z + size.z / 2f);

        return new Vector3 (x, y, z);
    }
}
