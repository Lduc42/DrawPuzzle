using System.Collections;
using UnityEngine;
using Spine.Unity;

public class MoveObjectAlongPath : MonoBehaviour
{
    #region declare
    public float speed = 1f; // Tốc độ di chuyển của object
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, move;
    public string current_state;
    public string current_animation;
    private float travel_time = 10;
    private int currentPointIndex = 0; // Index hiện tại của điểm đang xét
    private Vector3 currentPoint; // Điểm hiện tại đang xét
    public PathGameObject pathGameObject;
    private bool isMoving = false; // Cờ hiệu để biết object đang di chuyển hay không
    private bool isMoved = false;
    #endregion
    private void Awake()
    {

    }
    private void Start()
    {
        current_state = "Idle";
        //SetCharacterState(current_state);
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

        if (!isMoving && PathManager.Instance.Count() == 2)
        {
            Debug.Log("move");
            
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
        //SetCharacterState("Move");
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
                //SetCharacterState("Idle");
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
            //Debug.Log("current index "+  currentPointIndex);
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
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float time_scale)
    {
        if (animation.name.Equals(current_animation)) return;
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = time_scale;
        current_animation = animation.name;
    }
    public void SetCharacterState(string state)
    {
        if (state.Equals("Idle"))
        {
            SetAnimation(idle, true, 1f);
        }
        else if (state.Equals("Move"))
        {
            SetAnimation(move, true, 1f);
        }
    }
}
