using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 怪物生成 : MonoBehaviour
{
    private float timePass = 0f;
    [SerializeField]
    float CD = 10f;

    [SerializeField] GameObject 骷髅兵预制体;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timePass > CD && 骷髅兵预制体 != null)
        {
            var position = transform.position;
            Vector3 point = new Vector3(position.x + Random.Range(-10f, 10f), position.y,
                position.z + Random.Range(-10f, 10f));
            Instantiate(骷髅兵预制体, point, Quaternion.identity);
            timePass = 0f;
        }

        timePass += Time.deltaTime;
    }
}