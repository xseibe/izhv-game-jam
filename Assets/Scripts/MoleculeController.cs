using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoleculeController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject CanvasObj;
    [SerializeField] MoleculeArena MoleculeArenaScript;

    [Header("Settings")]
    [SerializeField] float MovementSpeed = 10f;
    [SerializeField] int MaxWater = 20;

    private int playerHealth = 5;

    private SettingsHelper settings;
    private CharacterController controller;
    private float sensitivity;
    private float rotationX, rotationY;
    private Vector3 moveDirection = Vector3.zero;

    private bool isImmune = false;
    private float immuneTimer = 0f;

    private bool isPaused = false;
    private float pauseTimer = 0f;

    private SphereCollider sphereCollider;

    // UI
    private List<GameObject> healthPointList = new List<GameObject>();
    private TMP_Text waterText;
    private int collectedWater = 0;
    private GameObject shieldImage;
    private bool shieldActive = false;

    private LevelManager levelManager;

    private void Start()
    {
        settings = SettingsHelper.GetInstance();
        sensitivity = settings.MouseSensitivity;
        controller = GetComponent<CharacterController>();
        sphereCollider = GetComponent<SphereCollider>();

        // Get health points references.
        GameObject healthBar = CanvasObj.transform.Find("HealthBar").gameObject;
        healthPointList = Enumerable.Range(1, playerHealth)
            .Select(index => healthBar.transform.Find($"H{index}").gameObject)
            .ToList();

        levelManager = FindObjectOfType<LevelManager>();

        waterText = CanvasObj.transform.Find("WaterBar/WaterText").GetComponent<TMP_Text>();
        shieldImage = CanvasObj.transform.Find("HealthBar/ShieldImage").gameObject;
    }

    void Update()
    {
        HandlePausedEffect();

        HandleImmunity();

        Movement();
    }

    private void Movement()
    {
        // If game is paused, block moving
        if (levelManager.GamePaused)
            return;

        moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
            moveDirection += -transform.right;

        if (Input.GetKey(KeyCode.D))
            moveDirection += transform.right;

        if (Input.GetKey(KeyCode.W))
            moveDirection += transform.forward;

        if (Input.GetKey(KeyCode.S))
            moveDirection += -transform.forward;

        if (Input.GetKey(KeyCode.Space))
            moveDirection += transform.up;

        if (Input.GetKey(KeyCode.LeftControl))
            moveDirection += -transform.up;

        // Character movement
        controller.Move(MovementSpeed * moveDirection * Time.unscaledDeltaTime);

        rotationX -= Input.GetAxis("Mouse Y") * sensitivity * Time.unscaledDeltaTime;
        rotationX = Mathf.Clamp(rotationX, -65f, 65f);

        rotationY += Input.GetAxis("Mouse X") * sensitivity * Time.unscaledDeltaTime;

        // Character rotation
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }

    private void HandleImmunity()
    {
        immuneTimer -= Time.deltaTime;

        // Deactivates the shield UI image
        if (shieldActive && immuneTimer <= 0)
        {
            shieldImage.SetActive(false);
            shieldActive = false;
        }

        isImmune = immuneTimer > 0;
    }

    private void HandlePausedEffect()
    {
        if (!isPaused)
            return;

        pauseTimer -= Time.unscaledDeltaTime;
        if (pauseTimer < 0)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Do the chain reaction and lower player's health
        if (collision.gameObject.tag == "MoleculeEnemy" && !isImmune)
        {
            healthPointList[--playerHealth].SetActive(false);

            // Resets current level if player health is 0
            if (playerHealth == 0)
                levelManager.ResetLevel();

            isImmune = true;
            immuneTimer = 2f;

            MoleculeArenaScript.SpawnMolecule(sphereCollider.bounds, 20, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            // Heal to full health
            case "CHeart":
                Destroy(other.gameObject);
                MoleculeArenaScript.CollectiblesSpawned--;
                healthPointList.ForEach(h => h.SetActive(true));
                playerHealth = 5;
                MoleculeArenaScript.CollectiblesSpawned--;
                break;
            // Make player immune for 5 seconds
            case "CShield":
                Destroy(other.gameObject);
                isImmune = true;
                shieldActive = true;
                immuneTimer = 5;
                shieldImage.SetActive(true);
                MoleculeArenaScript.CollectiblesSpawned--;
                break;
            // Stops time for 5 seconds
            case "CTime":
                Destroy(other.gameObject);
                Time.timeScale = 0;
                pauseTimer = 5f;
                isPaused = true;
                MoleculeArenaScript.CollectiblesSpawned--;
                break;
            // Collect water drop, spawns another one
            case "CWater":
                Destroy(other.gameObject);
                if (++collectedWater == MaxWater)
                {
                    SceneManager.LoadScene(2);
                }
                MoleculeArenaScript.SpawnWater();
                waterText.text = $"{collectedWater}/{MaxWater}";
                break;
        }
    }
}