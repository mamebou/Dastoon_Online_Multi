using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
	private int wait = 3;

    [SerializeField]
	private GameObject enemy;

	private int waitCount;

    private GameObject parentObject;


    // Start is called before the first frame update
    void Start()
    {
        parentObject = GameObject.Find("SceneRoot");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0.001f, 0, 0);
    }

    public void createEnemy(GameObject sceneRoot){

    }
}
