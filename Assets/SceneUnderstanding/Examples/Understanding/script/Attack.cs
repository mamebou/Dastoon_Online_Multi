using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject connectManager;
    private SimplePun pun;
    // Start is called before the first frame update
    void Start()
    {
        pun = connectManager.GetComponent<SimplePun>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision){
        EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
        if(collision.gameObject.name == "nomal_enemy"){
            pun.AddScore(400);
        }
        else if(collision.gameObject.name == "nomal_enemy"){
            pun.AddScore(800);
        }
        if(enemyController != null)
            enemyController.DestroyEnemy();
    }
}
