using UnityEngine;

public class RockCollider : MonoBehaviour
{
    // Kayanýn can deðeri
    public int maxHealth = 3;
    private int currentHealth;

    // Vurulma ve yok olma efektleri
    public GameObject hitEffect;
    public GameObject destroyEffect;
    public GameObject spawnedObject;

    // HitPoint noktasý
    public Transform hitPoint;

    void Start()
    {
        currentHealth = maxHealth; // Baþlangýçta kayanýn maksimum canýný ayarla
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
        currentHealth -= damage; // Hasarý kayanýn saðlýðýndan düþür

        // Vurulma efekti oluþtur
        if (hitEffect != null && hitPoint != null)
        {
            Instantiate(hitEffect, hitPoint.position, Quaternion.identity);
        }

        if (currentHealth <= 0)
        {
            Die(); // Eðer kayanýn saðlýðý 0 veya daha az ise, kayayý yok et
        }
    }

    void Die()
    {
        // Yok olma efekti oluþtur
        if (destroyEffect != null && hitPoint != null)
        {
            Instantiate(destroyEffect, hitPoint.position, Quaternion.identity);
        }

        // Yeni objeyi kayanýn pozisyonunda spawnla (biraz yukarýda)
        if (spawnedObject != null)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * 2f; // Yukarýya 2 birim hareket ettir
            Instantiate(spawnedObject, spawnPosition, Quaternion.identity);
        }

        // Nesneyi yok et
        Destroy(gameObject); // Bu satýr, scriptin baðlý olduðu prefab nesnesini yok eder
    }
}
