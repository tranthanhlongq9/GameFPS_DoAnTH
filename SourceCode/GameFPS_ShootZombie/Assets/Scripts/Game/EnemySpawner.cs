using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour {
	[Header("Enemy Spawn Management")]
	public GameObject zombiePrefab;
	public int maxZombies = 25;
	public float respawnDuration = 15.0f;
	public List<GameObject> spawnPoints = new List<GameObject>();
	
	[Header("Enemy Status")]
	public float startHealth = 100f;
	public float startMoveSpeed = 1f;
	public float startDamage = 15f;
	public int startEXP = 3;
	public int startFund = 4;
	public float upgradeDuration = 60f; // Tăng tất cả các chỉ số của kẻ thù sau mỗi 60 giây

	private float upgradeTimer;
	[SerializeField] private float currentHealth;
	[SerializeField] private float currentMoveSpeed;
	[SerializeField] private float currentDamage;
	[SerializeField] private int currentEXP;
	[SerializeField] private int currentFund;

	private bool activate = true;
	private float spawnTimer;


	void Start() {
		currentHealth = startHealth;
		currentMoveSpeed = startMoveSpeed;
		currentDamage = startDamage;
		currentEXP = startEXP;
		currentFund = startFund;

		spawnTimer = respawnDuration;	// Spawn ra
	}

	void Update() {
		if(!activate) return;

		if(spawnTimer < respawnDuration) {
			spawnTimer += Time.deltaTime;
		}
		else {
			SpawnEnemy();
		}

		if(upgradeTimer < upgradeDuration) {
			upgradeTimer += Time.deltaTime;
		}
		else {
			UpgradeEnemy();
		}
	}

	void SpawnEnemy() {
		if(spawnTimer < respawnDuration) return;

		int spawnCount = 0;
		int maxSpawnCount = 5;
		int zombiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

		foreach(GameObject spawnPoint in spawnPoints) {
			// If zombies were spawned too many, just stop.( nếu zombies sinh ra quá nhiều thì dừng lại )
			if(zombiesCount >= maxZombies) break;

			// Kiểm tra xem có bao nhiêu thây ma sinh ra một lần 
			else if (spawnCount >= maxSpawnCount) break;

			GameObject zombie = Instantiate(zombiePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation); //Instantiate để gọi 1 Prefab được sinh ra tại 1 vị trí 

			zombie.GetComponent<HealthManager>().SetHealth(currentHealth);

			KillReward killReward = zombie.GetComponent<KillReward>();
			killReward.SetReward(currentEXP, currentFund);

			// tăng tốc độ quay
			float rotateSpeed = 120f + currentMoveSpeed;
			rotateSpeed = Mathf.Max(rotateSpeed, 200f);	// Max 200f

			Chasing chasing = zombie.GetComponent<Chasing>();
			chasing.SetDamage(currentDamage);
			chasing.SetSpeed(currentMoveSpeed, rotateSpeed);

			spawnCount++;
			zombiesCount++;
		}
		
		spawnTimer = 0f;
	}

	void UpgradeEnemy() {
		currentHealth += 5;

		if(currentMoveSpeed < 6.0f) {
			currentMoveSpeed += 0.4f;
		}
		if(currentDamage < 90f) {
			currentDamage += 2f;
		}
		else {
			currentDamage = 90;
		}
		
		currentEXP++;
		currentFund++;

		upgradeTimer = 0;
	}
}
