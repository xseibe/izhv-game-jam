using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeArena : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject Molecule;
    [SerializeField] GameObject WaterDrop;
    [SerializeField] List<GameObject> Collectibles;

    [Header("Settings")]
    [SerializeField] int MoleculeInitCount = 20;
    [SerializeField] float MoleculeInitForce = 10f;
    [SerializeField] float CollectibleSpawnInterval = 15f;
    [SerializeField] int MaxCollectiblesSpawned = 3;

    public int CollectiblesSpawned { get; set; } = 0;

    private float collectibleSpawnTimer = 0f;

    private LevelManager levelManager;
    private BoxCollider ObjCollider;

    private float moleculesCount = 0;
    private float maxMoleculesCount = 200;

    // Start is called before the first frame update
    void Start()
    {
        collectibleSpawnTimer = CollectibleSpawnInterval;

        levelManager = FindObjectOfType<LevelManager>();
        ObjCollider = GetComponent<BoxCollider>();
        
        // Init spawn.
        SpawnMolecule(ObjCollider.bounds, MoleculeInitForce, MoleculeInitCount);
        SpawnWater();

        // Init text
        levelManager.CutsceneStart();
    }

    private void Update()
    {
        // If ESC is pressed
        if (levelManager.GamePaused && Input.GetKeyDown(KeyCode.Escape))
        {

        }

        // Spawn collectible in interval defined in SerializeField
        TryToSpawnCollectible();
    }

    public void SpawnMolecule(Bounds bounds, float initForce, int cnt)
    {
        if (moleculesCount >= maxMoleculesCount)
            return;

        for (int i = 0; i < cnt; ++i)
        {
            GameObject mol = Instantiate(Molecule, RandomPointInBounds(bounds), transform.rotation);
            mol.GetComponent<Rigidbody>().AddForce(Random.insideUnitCircle.normalized * initForce, ForceMode.VelocityChange);
        }

        moleculesCount += cnt;
    }

    public void SpawnCollectible(Bounds bounds)
    {
        GameObject collectible = Collectibles[Random.Range(0, Collectibles.Count)];
        Instantiate(collectible, RandomPointInBounds(bounds), collectible.transform.rotation);
    }

    public void SpawnWater()
    {
        Instantiate(WaterDrop, RandomPointInBounds(ObjCollider.bounds), WaterDrop.transform.rotation);
    }
    private void TryToSpawnCollectible()
    {
        collectibleSpawnTimer -= Time.deltaTime;

        if (collectibleSpawnTimer < 0f && CollectiblesSpawned < MaxCollectiblesSpawned)
        {
            SpawnCollectible(ObjCollider.bounds);
            collectibleSpawnTimer = CollectibleSpawnInterval;
        }
    }
    private Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        Random.Range(bounds.min.y, bounds.max.y),
        Random.Range(bounds.min.z, bounds.max.z));
    }
}
