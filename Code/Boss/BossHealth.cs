using UnityEngine;
using System.Collections;

public class BossHealthHandler : MonoBehaviour
{
    public GameObject[] pickupPrefabs;
    public BossStats stats;
    public Animator anim;
    public AudioClip deathSound;
    private AudioSource audioSource;
    private GameObject root;
    private bool isDead = false;   

    void Start()
    {
        if (stats == null) stats = GetComponentInParent<BossStats>();
        if (anim == null) anim = GetComponentInParent<Animator>();

        root = transform.root.gameObject;
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void CheckDeath()
    {
        if (isDead) return;
               
        if (stats.health <= 0)
        {
            Debug.Log("BOSS IS DEAD");
            if (audioSource != null && deathSound != null)
                audioSource.PlayOneShot(deathSound);
            var controller = root.GetComponent<BossController>();
            if (controller != null)
                controller.StopBoss();

            anim.SetTrigger("BossDead");

            ScoreManager.instance.AddScore(stats.scoreValue);
            FindFirstObjectByType<EnemySpawner>().BossDied();

            StartCoroutine(DeathSequence());
        }
    }

    private IEnumerator DeathSequence()
    {
        foreach (var col in root.GetComponentsInChildren<Collider2D>())
            col.enabled = false;

        yield return new WaitForSeconds(1.2f);

        DropBossPickups();

        Destroy(root);
    }

    private void DropBossPickups()
    {
        if (pickupPrefabs == null || pickupPrefabs.Length == 0)
        {
            Debug.LogWarning("❌ WALAY PICKUP PREFABS SA BOSS!");
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            var offset = Random.insideUnitCircle * 0.8f;

            Instantiate(
                pickupPrefabs[Random.Range(0, pickupPrefabs.Length)],
                (Vector2)root.transform.position + offset,
                Quaternion.identity
            );
        }
    }
}
