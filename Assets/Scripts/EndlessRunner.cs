using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class EndlessRunner : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 10f;
    public float laneChangeSpeed = 5f;
    public float jumpForce = 8f;
    public int maxLanes = 3;
    
    [Header("Game Settings")]
    public float obstacleSpawnInterval = 2f;
    public float sceneChangeInterval = 30f;
    public float distanceBetweenObstacles = 10f;
    public GameObject[] obstaclePrefabs;
    public string[] sceneNames;
    
    private int currentLane = 1;
    private float targetX;
    private bool isJumping = false;
    private float laneWidth = 3f;
    private float gameTime = 0f;
    private float lastSceneChange = 0f;
    private Queue<GameObject> obstaclePool;
    private readonly int poolSize = 10;
    
    void Start()
    {
        InitializeObjectPool();
        StartCoroutine(SpawnObstacles());
        targetX = transform.position.x;
    }
    
    void InitializeObjectPool()
    {
        obstaclePool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]);
            obstacle.SetActive(false);
            obstaclePool.Enqueue(obstacle);
        }
    }
    
    void Update()
    {
        HandleMovement();
        HandleLaneChange();
        HandleJump();
        CheckSceneChange();
    }
    
    void HandleMovement()
    {
        // Move forward continuously
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * 31);
        
        // Smooth lane transition
        float currentX = transform.position.x;
        float newX = Mathf.Lerp(currentX, targetX, Time.deltaTime * laneChangeSpeed);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
    
    void HandleLaneChange()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(1);
        }
    }
    
    void MoveLane(int direction)
    {
        int newLane = currentLane + direction;
        if (newLane >= 0 && newLane < maxLanes)
        {
            currentLane = newLane;
            targetX = (currentLane - 1) * laneWidth;
        }
    }
    
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(Jump());
        }
    }
    
    IEnumerator Jump()
    {
        isJumping = true;
        float jumpTime = 0f;
        float startY = transform.position.y;
        
        while (jumpTime < 1f)
        {
            jumpTime += Time.deltaTime * 1.5f;
            float height = Mathf.Sin(jumpTime * Mathf.PI) * jumpForce;
            transform.position = new Vector3(transform.position.x, startY + height, transform.position.z);
            yield return null;
        }
        
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        isJumping = false;
    }
    
    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            GameObject obstacle = obstaclePool.Dequeue();
            obstacle.SetActive(true);
            obstacle.transform.position = new Vector3(
                (Random.Range(0, maxLanes) - 1) * laneWidth,
                0,
                transform.position.z + 30f
            );
            
            obstaclePool.Enqueue(obstacle);
            yield return new WaitForSeconds(obstacleSpawnInterval);
        }
    }
    
    void CheckSceneChange()
    {
        gameTime += Time.deltaTime;
        if (gameTime - lastSceneChange >= sceneChangeInterval && sceneNames.Length > 0)
        {
            lastSceneChange = gameTime;
            string nextScene = sceneNames[Random.Range(0, sceneNames.Length)];
            StartCoroutine(TransitionToScene(nextScene));
        }
    }
    
    IEnumerator TransitionToScene(string sceneName)
    {
        // Add fade effect or transition animation here
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Handle collision (game over, restart, etc.)
            Debug.Log("Game Over!");
        }
    }
}