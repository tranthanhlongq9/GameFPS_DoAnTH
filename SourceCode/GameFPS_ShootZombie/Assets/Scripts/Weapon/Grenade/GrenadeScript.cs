using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    public GameObject explosionEffect;
    public float delay = 3f;

    public float explosionForce = 10f;
    public float radius = 20f;

    //public LayerMask zombieLayer;
    public int attackDamage = 100;
    //public GameObject zombieLayer;

    public Animator hitAnimator;

    public RewardText rewardText;
    public FundSystem fundSystem;
    public LevelSystem levelSystem;

    void Start()
    {       
        rewardText = GameObject.Find("Player").GetComponent<RewardText>();
        fundSystem = GameObject.Find("Player").GetComponent<FundSystem>();
        levelSystem = GameObject.Find("Player").GetComponent<LevelSystem>();

        hitAnimator = GameObject.Find("UI/InGameUI/Hit").GetComponent<Animator>();

        Invoke("Explode", delay); //sẽ gọi sau 1 khoảng thời gian delay
    }

    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        
        foreach(Collider near in colliders)
        {
            Rigidbody rig = near.GetComponent<Rigidbody>();

            if (rig != null) 
            {
                rig.AddExplosionForce(explosionForce, transform.position, radius, 1f, ForceMode.Impulse); // những vật thể có Rigidbody sẽ bị đẩy văng
            }

            
            AIExample aIExample = near.GetComponent<AIExample>();
            KillReward killReward = near.GetComponent<KillReward>();
            HealthManager healthManager = near.GetComponent<HealthManager>();
           
            

            if (aIExample != null )
            {
                aIExample.OnHit(attackDamage); //set damage trừ máu
                

                int exp = killReward.exp;
                int fund = killReward.fund;

                levelSystem.GiveExp(exp);
                fundSystem.AddFund(fund);

                rewardText.Show(exp, fund);
                
            }
            else if (near.gameObject.CompareTag("Enemy"))
            {
                healthManager.damageBoom(attackDamage);

                //if (near.gameObject.CompareTag("Enemy")) 
                //{
                    int exp = killReward.exp;
                    int fund = killReward.fund;

                    levelSystem.GiveExp(exp);
                    fundSystem.AddFund(fund);

                    rewardText.Show(exp, fund);
                //}              
            }
            else if (near.gameObject.CompareTag("Player"))
            {
                healthManager.damageBoom(10);
                healthManager.onHit.AddListener(() => { hitAnimator.SetTrigger("Show"); });
            }
            
            //HealthManager healthManager = near.GetComponent<HealthManager>();
            ////if(healthManager != null) {
            //    healthManager.ApplyDamage(attackDamage);
            //    int exp = killReward.exp;
            //    int fund = killReward.fund;

            //    levelSystem.GiveExp(exp);
            //    fundSystem.AddFund(fund);

            //    rewardText.Show(exp, fund);

            //}
            
        }
       
        GameObject explosionEffects = Instantiate(explosionEffect, transform.position, transform.rotation); //sinh ra effect(hiệu úng) nổ tại vị trí quả bom
        Destroy(explosionEffects, 5f); //phá hủy effect sau 5s
        Destroy(gameObject);
    }

}
