using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{

    public GameObject basicEnemy;
    public GameObject mediumEnemy;
    public GameObject hardEnemy;
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    public GameObject bossText;
    public Transform[] spawnPoints;
    public TextMeshProUGUI roundText;
    private int round = 1;
    private Queue<GameObject> enemyQueue = new Queue<GameObject>();
    private int enemiesKilled;
    private int enemiesThisRound;
    private int bossCount = 0;

    public float spawnDelay = 1f;

    void Start()
    {
        UpdateRoundUI();
        StartRound();
    }

    void StartRound()
    {
        UpdateRoundUI();
        enemiesKilled = 0;

        if (round % 20 == 0)
        {
            BossRound();
            return;
        }

        enemiesThisRound = 3 + (round - 1);

        BuildEnemyQueue();
        InvokeRepeating(nameof(SpawnNextFromQueue), 1f, spawnDelay);
    }

    void BuildEnemyQueue()
    {
        enemyQueue.Clear();

        for (int i = 0; i < enemiesThisRound; i++)
        {
            enemyQueue.Enqueue(DetermineEnemyToSpawn());
        }
    }

    void SpawnNextFromQueue()
    {
        if (enemyQueue.Count == 0)
        {
            CancelInvoke(nameof(SpawnNextFromQueue));
            return;
        }

        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemyPrefab = enemyQueue.Dequeue();
        GameObject enemyObj = Instantiate(enemyPrefab, spawn.position, Quaternion.identity);

      
        float multiplier = 1f + (0.05f * (round - 1));

        Enemy1 e1 = enemyObj.GetComponent<Enemy1>();
        if (e1 != null)
        {
            e1.maxHealth = Mathf.RoundToInt(e1.maxHealth * multiplier);
            e1.bulletDamage = Mathf.RoundToInt(e1.bulletDamage * multiplier);
        }

        Enemy2 e2 = enemyObj.GetComponent<Enemy2>();
        if (e2 != null)
        {
            e2.maxHealth = Mathf.RoundToInt(e2.maxHealth * multiplier);

            EnemyBullet2 b2 = enemyObj.GetComponentInChildren<EnemyBullet2>();
            if (b2 != null) b2.damage = Mathf.RoundToInt(b2.damage * multiplier);

            e2.bulletDamage = Mathf.RoundToInt(e2.bulletDamage * multiplier); 
        }

        Enemy3 e3 = enemyObj.GetComponent<Enemy3>();
        if (e3 != null)
        {
            e3.maxHealth = Mathf.RoundToInt(e3.maxHealth * multiplier);

            EnemyBullet3 b3 = enemyObj.GetComponentInChildren<EnemyBullet3>();
            if (b3 != null) b3.damage = Mathf.RoundToInt(b3.damage * multiplier);

            e3.bulletDamage = Mathf.RoundToInt(e3.bulletDamage * multiplier);
        }
    }

    void BossRound()
    {
        if (bossText != null) bossText.SetActive(true);
        Invoke(nameof(SpawnBoss), 4f);
    }

    void SpawnBoss()
    {
        if (bossText != null) bossText.SetActive(false);

        bossCount = round / 20;

        Transform spawn = (bossSpawnPoint != null) ? bossSpawnPoint : spawnPoints[0];
        GameObject boss = Instantiate(bossPrefab, spawn.position, Quaternion.identity);

        BossStats stats = boss.GetComponent<BossStats>();
        if (stats != null)
        {
            int bossIndex = round / 20;
            int baseHealth = 1000;
            int baseDamage = 30;
            int normalHealth = baseHealth + (bossIndex - 1) * 20;
            int normalDamage = baseDamage + (bossIndex - 1) * 2;
            int multiplier = (int)Mathf.Pow(2, bossIndex - 1);

            stats.health = normalHealth * multiplier;
            stats.damage = normalDamage * multiplier;
            stats.fireRate = Mathf.Max(0.2f, 1.5f - (bossIndex - 1) * 0.05f);

            BossBullet[] bullets = boss.GetComponentsInChildren<BossBullet>();
            foreach (var b in bullets)
            {
                b.SetDamage(stats.damage);
            }
        }
    }

    GameObject DetermineEnemyToSpawn()
    {
        if (round < 10) return basicEnemy;
        if (round < 20) return (Random.value < 0.7f) ? basicEnemy : mediumEnemy;
        if (round < 30) return (Random.value < 0.5f) ? basicEnemy : mediumEnemy;
        if (round < 40) return (Random.value < 0.2f) ? basicEnemy : mediumEnemy;
        if (round < 50) return (Random.value < 0.7f) ? mediumEnemy : hardEnemy;

        return (Random.value < 0.4f) ? mediumEnemy : hardEnemy;
    }

    public void EnemyDied()
    {
        enemiesKilled++;

        if (enemiesKilled >= enemiesThisRound)
        {
            round++;
            Invoke(nameof(StartRound), 2f);
        }
    }

    public void BossDied()
    {
        round++;
        Invoke(nameof(StartRound), 2f);
    }

    void UpdateRoundUI()
    {
        if (roundText != null)
            roundText.text = "Round " + round;
    }
}
