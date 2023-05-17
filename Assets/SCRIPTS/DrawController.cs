﻿using UnityEngine;

public class DrawController : MonoBehaviour
{
    #region declare
    public static DrawController Instance;
    public float detectionDistance = .2f;
    [SerializeField]
    private LineRenderer LinePrefab;
    public PathGameObject pathGameObject;
    [SerializeField] private MoveObjectAlongPath[] objects;
    [SerializeField] private TargetObject[] targets;
    [SerializeField] private float obstacle_size;
    [SerializeField] private ObstacleScript[] obstacles;
    private int countDraw = 0;
    private int targetId;
    private int start_id;
    private int end_id;
    private bool canDraw = false;
    private bool canAddToList = false;
    private float intervalDistance = 0.2f; //interval distance that can be correct start points
    #endregion
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {

    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //check start point
            CheckStartValidPoint();
        }
        if (canDraw && Input.GetButton("Fire1"))
        {
            // Mouse is still down and we are dragging, so keep drawing.
            //if object haven't been went to target, draw
            DrawToTarget();
        }
        if (Input.GetButtonUp("Fire1"))
        {

            //process after draw
            CompleteDraw();
        }
    }

    public void Draw(Vector3 position)
    {
        // Create a plane and see where the mouse click intersects it. 
        Plane plane = new Plane(Camera.main.transform.forward * -1, position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float distance))
        {
            if (pathGameObject == null)
            {
                // Starting a new line. Instantiate our "Path Object"
                pathGameObject =
                    UnityEngine.Object.Instantiate(LinePrefab).GetComponent<PathGameObject>();
            }
            else
            {
                Vector3 hitpoint = ray.GetPoint(distance);
                Vector3 lastPosition = pathGameObject.GetLastPosition();
                // Kiểm tra va chạm với vật cản
                /*                float distanceToMouse = Vector3.Distance(pathGameObject.GetLastPosition(), hitpoint);
                                Vector3 temp = (hitpoint - lastPosition);
                                Debug.Log(temp);
                                //RaycastHit hit;
                                RaycastHit2D hit = Physics2D.Raycast(pathGameObject.GetLastPosition(), hitpoint - lastPosition, distanceToMouse);
                                    if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
                                    {
                                        Debug.Log("vat can");
                                        return;
                                    }
                                    // Nếu không có va chạm hoặc va chạm với vật cản, không thêm điểm vào đường vẽ

                                // Add the hit point to the path*/
                Vector2 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D collision = collision = Physics2D.OverlapPoint(newPoint);
                RaycastHit2D hit = Physics2D.Linecast(lastPosition, newPoint);
                if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
                {
                    return;
                }
                pathGameObject.AddPosition(hitpoint);
            }
        }
    }
    public MoveObjectAlongPath GetObject(int index)
    {
        return objects[index];
    }
    public PathGameObject GetPath()
    {
        return pathGameObject;
    }
    private void CheckStartValidPoint()
    {
        Vector2 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (int i = 0; i < objects.Length; i++)
        {
            if (Mathf.Abs(newPoint.x - objects[i].transform.position.x) < intervalDistance &&
                Mathf.Abs(newPoint.y - objects[i].transform.position.y) < intervalDistance)
            {
               /* LinePrefab.startColor = objects[i].GetComponent<SpriteRenderer>().color;
                LinePrefab.endColor = objects[i].GetComponent<SpriteRenderer>().color;*/
                Debug.Log("inbound");
                pathGameObject = null;
                canDraw = true;
                start_id = i + 1;
            }
            else
            {
                Debug.Log(objects[i].transform.position);
                Debug.Log("mouse: " + newPoint);
                Debug.Log("invalid input");
            }
        }
    }
    private void DrawToTarget()
    {
        // Mouse is still down and we are dragging, so keep drawing.
        //if object haven't been went to target, draw
        if (end_id == 0)
            Draw(Input.mousePosition);
        Vector2 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (int i = 0; i < targets.Length; i++)
        {
            if (Mathf.Abs(newPoint.x - targets[i].transform.position.x) < intervalDistance &&
                Mathf.Abs(newPoint.y - targets[i].transform.position.y) < intervalDistance)
            {
                end_id = i + 1;
            }
        }
    }
    private void CompleteDraw()
    {
        if(pathGameObject != null)
        pathGameObject.SetId(start_id);
        //check correct
        if (end_id == pathGameObject.GetId() && pathGameObject != null)
        {
            PathManager.Instance.AddPaths(pathGameObject);
        }
        else
        {
            pathGameObject.points.Clear();
            Destroy(pathGameObject.gameObject);
        }
        canDraw = false;
        countDraw++;
        Debug.Log("draw = " + countDraw);
        if (countDraw == 2)
        {

        }
        end_id = 0;
        pathGameObject = null;
    }
    private bool IsObstaclePoint()
    {
        Vector2 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (Mathf.Abs(newPoint.x - obstacles[i].transform.position.x) < obstacles[i].GetHalfWidth() &&
                Mathf.Abs(newPoint.y - obstacles[i].transform.position.y) < obstacles[i].GetHalfHeight())
            {
                Debug.Log("va cham chuong ngai vat");
                return true;
            }
        }
        return false;
    }
}