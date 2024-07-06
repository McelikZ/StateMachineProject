using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollider : MonoBehaviour
{
    private TreeManager treeManager;

    // Aðaç caný
    public int maxHealth = 3;
    private int currentHealth;

    // Vurulma ve yok olma efektleri
    public GameObject hitEffect;
    public GameObject destroyEffect;
    public GameObject spawnedObject;
    // Baltanýn ucundaki boþ obje
    public Transform hitPointObject;

    private void Start()
    {
        hitPointObject = GameObject.Find("HitPoint").transform;
    }
    public void SetTreeManager(TreeManager manager)
    {
        treeManager = manager;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Vurulma efekti oluþtur
        if (hitEffect != null && hitPointObject != null)
        {
            Instantiate(hitEffect, hitPointObject.position, Quaternion.identity);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Yok olma efekti oluþtur
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, hitPointObject.position, Quaternion.identity);
        }

        // Terrain bileþenine eriþin
        Terrain terrain = Terrain.activeTerrain;

        if (terrain == null)
        {
            Debug.LogError("Terrain bulunamadý!");
            return;
        }

        // Aðaç pozisyonunu hesapla
        Vector3 treePosition = transform.position - terrain.transform.position;
        Vector3 normalizedPosition = new Vector3(treePosition.x / terrain.terrainData.size.x, treePosition.y / terrain.terrainData.size.y, treePosition.z / terrain.terrainData.size.z);

        // Aðaçlarý bul
        TreeInstance[] trees = terrain.terrainData.treeInstances;
        List<TreeInstance> newTrees = new List<TreeInstance>();
        Vector3 treeWorldPosition = Vector3.zero;
        bool treeFound = false;

        foreach (TreeInstance tree in trees)
        {
            Vector3 worldTreePosition = Vector3.Scale(tree.position, terrain.terrainData.size) + terrain.transform.position;

            if (!treeFound && Vector3.Distance(worldTreePosition, transform.position) < 0.1f)
            {
                // Aðaç pozisyonunu kaydet ve biraz yukarý kaydýr
                treeWorldPosition = worldTreePosition + Vector3.up * 2f; // Örnek olarak yukarýda 2 birim yükseðe kaydýrdýk
                treeFound = true;
            }
            else
            {
                newTrees.Add(tree);
            }
        }

        if (!treeFound)
        {
            Debug.LogError("Aðaç pozisyonu bulunamadý!");
            return;
        }

        // Aðaçlarý güncelle
        terrain.terrainData.treeInstances = newTrees.ToArray();

        // Prefab objesini yok et
        Destroy(gameObject);

        // Yeni objeyi aðacýn eski pozisyonunda spawnla
        Instantiate(spawnedObject, treeWorldPosition, Quaternion.identity);
    }
}
