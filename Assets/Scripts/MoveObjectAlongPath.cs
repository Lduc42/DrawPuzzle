using System.Collections;
using UnityEngine;
using Spine.Unity;

public class MoveObjectAlongPath : MonoBehaviour
{
    #region declare
    public float speed = 1f; // Tốc độ di chuyển của object
    private float travel_time = 10;
    private int currentPointIndex = 0; // Index hiện tại của điểm đang xét
    private Vector3 currentPoint; // Điểm hiện tại đang xét
    public PathGameObject pathGameObject;
    private bool isMoving = false; // Cờ hiệu để biết object đang di chuyển hay không
    private bool isMoved = false;
    [SerializeField]
    private CharacterStateManager state_manager;
    [SerializeField]
    private PointPath point_path;
    private bool not_move = false;
    #endregion
    private void Awake()
    {

    }
    private void Start()
    {

    }
    private void Update()
    {
        //if have path, move
        if (GetPathGameObject() != null && !isMoved)
        {
            MoveToTarget();
        }
        else
        {

        }
    }
    private void MoveToTarget()
    {
        // Kiểm tra nếu như object không di chuyển và đường vẽ đã được vẽ

        if (!isMoving && PathManager.Instance.IsEnough())
        {
            Debug.Log("move");
            state_manager.current_state = "Move";
            isMoving = true;
            Debug.Log("vao day");
            speed = pathGameObject.Count() * 1.0f / travel_time;
            Debug.Log("speed: " + speed + "count: " + pathGameObject.Count());
            StartCoroutine(Move());
            isMoved = true;
        }
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(.5f);
        
        currentPointIndex = 0;
        currentPoint = pathGameObject.GetPosition(currentPointIndex);
        // Vòng lặp di chuyển object
        while (isMoving)
        {
            //Debug.Log("currentpointindex: "+ currentPointIndex);
            // Nếu object đến được điểm cuối của đường vẽ, dừng di chuyển
            if (currentPointIndex >= pathGameObject.Count() - 1)
            {
                //Debug.Log("Moved to target");
                isMoving = false;
                if(point_path.CheckInActiveAllPoint())
                state_manager.current_state = "Win";
                else state_manager.current_state = "Lose";
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
                if (!not_move)
                {
                    t += Time.deltaTime;
                    transform.position = Vector3.Lerp(currentPoint, nextPoint, t / timeToMove);
                }
                yield return null;
            }

            // Cập nhật điểm hiện tại và index đang xét
            if (!not_move)
            {
                currentPointIndex++;
                currentPoint = nextPoint;
            }
     
            //Debug.Log("current index "+  currentPointIndex);
        }
    }
    public void SetMove(bool value)
    {
        not_move = value;
    }
    public bool GetMove()
    {
        return not_move;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Point"))
        {
            Debug.Log("object va cham voi point");
            // get object
            Point collidedObject = other.gameObject.GetComponent<Point>();
            if (collidedObject != null)
            {
                int collidedId = collidedObject.GetId();
                Debug.Log("collidedId: " + collidedId);
                if (collidedId == pathGameObject.GetId())
                {
                    Debug.Log("pass point");
                    collidedObject.gameObject.SetActive(false);
                    not_move = true;
                    state_manager.current_state = collidedObject.GetState();
                    StartCoroutine(Delay(0.8f));
                    
                }
                else
                {
                    //Debug.Log("id point:" + id);
                }
            }
        }

    }
    public PathGameObject GetPathGameObject()
    {
        return pathGameObject;
    }
    public void SetPathGameObject(PathGameObject value)
    {
        pathGameObject = value;
    }
    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        not_move = false;
    }
}
