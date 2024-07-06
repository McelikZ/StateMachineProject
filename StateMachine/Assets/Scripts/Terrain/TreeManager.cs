using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public GameObject treePrefab; // A�a�lar� temsil eden prefab
    public Transform parentTransform;

    void Start()
    {
        // Terrain bile�enine eri�in
        Terrain terrain = Terrain.activeTerrain;

        if (terrain == null)
        {
            Debug.LogError("Terrain bulunamad�!");
            return;
        }

        // Terrain �zerindeki a�a�lar� al
        TreeInstance[] trees = terrain.terrainData.treeInstances;

        foreach (TreeInstance tree in trees)
        {
            // A�a� pozisyonunu hesapla
            Vector3 worldPosition = Vector3.Scale(tree.position, terrain.terrainData.size) + terrain.transform.position;

            // A�a� i�in prefab olu�tur
            GameObject treeColliderObject = Instantiate(treePrefab, worldPosition, Quaternion.identity);
            treeColliderObject.transform.SetParent(parentTransform);
            treeColliderObject.transform.position = worldPosition;

            // TreeCollider bile�enine eri�im sa�la ve treeManager'� ata
            ItemCollider treeCollider = treeColliderObject.GetComponent<ItemCollider>();
            if (treeCollider != null)
            {
                treeCollider.SetTreeManager(this);
            }
        }
    }
}