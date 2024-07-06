using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public GameObject treePrefab; // Aðaçlarý temsil eden prefab
    public Transform parentTransform;

    void Start()
    {
        // Terrain bileþenine eriþin
        Terrain terrain = Terrain.activeTerrain;

        if (terrain == null)
        {
            Debug.LogError("Terrain bulunamadý!");
            return;
        }

        // Terrain üzerindeki aðaçlarý al
        TreeInstance[] trees = terrain.terrainData.treeInstances;

        foreach (TreeInstance tree in trees)
        {
            // Aðaç pozisyonunu hesapla
            Vector3 worldPosition = Vector3.Scale(tree.position, terrain.terrainData.size) + terrain.transform.position;

            // Aðaç için prefab oluþtur
            GameObject treeColliderObject = Instantiate(treePrefab, worldPosition, Quaternion.identity);
            treeColliderObject.transform.SetParent(parentTransform);
            treeColliderObject.transform.position = worldPosition;

            // TreeCollider bileþenine eriþim saðla ve treeManager'ý ata
            ItemCollider treeCollider = treeColliderObject.GetComponent<ItemCollider>();
            if (treeCollider != null)
            {
                treeCollider.SetTreeManager(this);
            }
        }
    }
}