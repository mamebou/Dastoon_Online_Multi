using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeCheck : MonoBehaviour
{
    // 実行後、Inspector表示用
    public Vector3 originalSize;
    public Vector3 realSize;

    // Start is called before the first frame update
    void Start()
    {
        // メッシュの（バウンズ）サイズを取得
        Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
        Bounds bounds = mesh.bounds;
        originalSize = bounds.size;

        // スケールを掛け合わせた実際のサイズを取得
        realSize = bounds.size;
        realSize.x *= transform.localScale.x;
        realSize.y *= transform.localScale.y;
        realSize.z *= transform.localScale.z;

        // Consoleへ表示
        print(string.Format("name = {0} : Original Size(m) = ({1}, {2}, {3}), Real Size(m) = ({4}, {5}, {6})",
            this.name,
            originalSize.x, originalSize.y, originalSize.z,
            realSize.x, realSize.y, realSize.z));
    }

    // Update is called once per frame
    void Update()
    {
        // メッシュの（バウンズ）サイズを取得
        Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
        Bounds bounds = mesh.bounds;
        originalSize = bounds.size;

        // スケールを掛け合わせた実際のサイズを取得
        realSize = bounds.size;
        realSize.x *= transform.localScale.x;
        realSize.y *= transform.localScale.y;
        realSize.z *= transform.localScale.z;

        // Consoleへ表示
        print(string.Format("name = {0} : Original Size(m) = ({1}, {2}, {3}), Real Size(m) = ({4}, {5}, {6})",
            this.name,
            originalSize.x, originalSize.y, originalSize.z,
            realSize.x, realSize.y, realSize.z));

    }
}