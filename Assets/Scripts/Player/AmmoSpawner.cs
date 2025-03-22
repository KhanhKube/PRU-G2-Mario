using System.Collections;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject ammoPackPrefab; // Gán Prefab AmmoPack trong Inspector
    public Transform player; // Gán Object Player vào đây
    public float spawnInterval = 15f; // Thời gian spawn
    public float spawnHeight = 5f; // Khoảng cách rơi từ trên xuống

    private void Start()
    {
        StartCoroutine(SpawnAmmoPack());
    }

    IEnumerator SpawnAmmoPack()
    {
        while (true) // Vòng lặp vô hạn để luôn spawn AmmoPack
        {
            yield return new WaitForSeconds(spawnInterval); // Chờ 15s
            Spawn();
            Debug.Log("spaw Ammo sucess");

        }
    }

    void Spawn()
    {
        if (player != null && ammoPackPrefab != null)
        {
            Vector3 spawnPosition = new Vector3(player.position.x, player.position.y + spawnHeight, 0);
            Instantiate(ammoPackPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("spaw sucess");
        }
    }
}
