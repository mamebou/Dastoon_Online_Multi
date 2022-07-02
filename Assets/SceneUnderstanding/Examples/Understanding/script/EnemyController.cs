using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject enemy;  //①動かしたいオブジェクトをインスペクターから入れる。
    public int speed = 5;  //オブジェクトが自動で動くスピード調整
    Vector3 movePosition;  //②オブジェクトの目的地を保存

    void Start()
    {
        movePosition = moveRandomPosition();  //②実行時、オブジェクトの目的地を設定
    }

    void Update()
    {
        if(movePosition == enemy.transform.position)  //②playerオブジェクトが目的地に到達すると、
        {
            movePosition = moveRandomPosition();  //②目的地を再設定
        }
        this.enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, movePosition, speed * Time.deltaTime);  //①②playerオブジェクトが, 目的地に移動, 移動速度
    }

    private Vector3 moveRandomPosition()  // 目的地を生成、xとyのポジションをランダムに値を取得 
    {
        Vector3 randomPosi = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 5);
        return randomPosi;
    }

    public void DestroyEnemy(){
        Destroy(this.gameObject);
    }
}
