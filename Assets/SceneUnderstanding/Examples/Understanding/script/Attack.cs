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
        Debug.Log("collision");
        EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
        GameObject collisionObject = collision.gameObject;
        if(collisionObject.CompareTag("nomal_enemy")){
            pun.AddScore(400);
            Debug.Log("collision normal");
            Destroy(collisionObject);
        }
        else if(collisionObject.CompareTag("rare_enemy")){
            pun.AddScore(800);
            Debug.Log("collision rare");
            Destroy(collisionObject);
        }
        if(enemyController != null)
            enemyController.DestroyEnemy();
    }
}
