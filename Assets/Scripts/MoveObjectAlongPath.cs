﻿using System.Collections;
using UnityEngine;

public class MoveObjectAlongPath : MonoBehaviour
{

    public float speed = 1f; // Tốc độ di chuyển của object

    private int currentPointIndex = 0; // Index hiện tại của điểm đang xét
    private Vector3 currentPoint; // Điểm hiện tại đang xét
    public PathGameObject pathGameObject;
    private bool isMoving = false; // Cờ hiệu để biết object đang di chuyển hay không
    private void Awake()
    {
       
    }

    private void Update()
    {
        if (GameController.Instance.pathGameObject != null)
        {
            // Kiểm tra nếu như object không di chuyển và đường vẽ đã được vẽ


            if (!isMoving && PathManager.Instance.Count() == 2 )
            {
                Debug.Log("move");
                isMoving = true;
                Debug.Log("vao day");
                StartCoroutine(Move());
            }
        }

        else
        {
            //Debug.Log(PathManager.Instance.Count());
        }
    }

    IEnumerator Move()
    {
        currentPointIndex = 0;
        currentPoint = pathGameObject.GetPosition(currentPointIndex);
        // Vòng lặp di chuyển object
        while (isMoving)
        {
            // Nếu object đến được điểm cuối của đường vẽ, dừng di chuyển
            if (currentPointIndex >= pathGameObject.Count() - 1)
            {
                isMoving = false;
                break;
            }

            // Tính toán khoảng cách cần di chuyển tới điểm tiếp theo
            Vector3 nextPoint = pathGameObject.GetPosition(currentPointIndex + 1);
            float distanceToNextPoint = Vector3.Distance(currentPoint, nextPoint);

            // Tính toán thời gian cần để di chuyển từ điểm hiện tại tới điểm tiếp theo
            float timeToMove = distanceToNextPoint / speed;

            // Di chuyển object từ điểm hiện tại tới điểm tiếp theo trong thời gian timeToMove
            float t = 0f;
            while (t < timeToMove)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(currentPoint, nextPoint, t / timeToMove);
                yield return null;
            }

            // Cập nhật điểm hiện tại và index đang xét
            currentPointIndex++;
            currentPoint = nextPoint;
        }
    }
}