using UnityEngine;

public class RockCollider : MonoBehaviour
{
    // Kayan�n can de�eri
    public int maxHealth = 3;
    private int currentHealth;

    // Vurulma ve yok olma efektleri
    public GameObject hitEffect;
    public GameObject destroyEffect;
    public GameObject spawnedObject;

    // HitPoint noktas�
    public Transform hitPoint;

    void Start()
    {
        currentHealth = maxHealth; // Ba�lang��ta kayan�n maksimum can�n� ayarla
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            TakeDamage(1); // Kayaya 1 hasar ver
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage; // Hasar� kayan�n sa�l���ndan d���r

        // Vurulma efekti olu�tur
        if (hitEffect != null && hitPoint != null)
        {
            Instantiate(hitEffect, hitPoint.position, Quaternion.identity);
        }

        if (currentHealth <= 0)
        {
            Die(); // E�er kayan�n sa�l��� 0 veya daha az ise, kayay� yok et
        }
    }

    void Die()
    {
        // Yok olma efekti olu�tur
        if (destroyEffect != null && hitPoint != null)
        {
            Instantiate(destroyEffect, hitPoint.position, Quaternion.identity);
        }

        // Yeni objeyi kayan�n pozisyonunda spawnla (biraz yukar�da)
        if (spawnedObject != null)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * 2f; // Yukar�ya 2 birim hareket ettir
            Instantiate(spawnedObject, spawnPosition, Quaternion.identity);
        }

        // Nesneyi yok et
        Destroy(gameObject); // Bu sat�r, scriptin ba�l� oldu�u prefab nesnesini yok eder
    }
}
