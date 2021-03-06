using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
    // шаблон яблока
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
        Invoke("DropApple", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        // перемещение
        Vector3 position = transform.position;
        position.x += speed * Time.deltaTime;
        transform.position = position;
        // изменение направления
        if (position.x < -leftAndRightEdge) {
            speed = Mathf.Abs(speed);
        } else if (position.x > leftAndRightEdge) {
            speed = - Mathf.Abs(speed);
        }      
    }

    void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirection) {
            speed *= -1;
        }       
    }

    void DropApple() {
        GameObject apple = Instantiate<GameObject> (applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", secondsBetweenAppleDrops);
    }
}
