using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    // шаблон дерева
    public GameObject applePrefab;
    // скорость перемещения
    public float speed = 1f;
    // расстояние от края
    public float leftAndRightEdge = 10f;
    // вероятность смены направления
    public float chanceToChangeDirection = 0.1f;
    // частота падения яблок
    public float secondsBetweenAppleDrops = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
