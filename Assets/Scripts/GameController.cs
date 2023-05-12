using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public float detectionDistance = .2f;
    [SerializeField]
    private LineRenderer LinePrefab;
    public PathGameObject pathGameObject;
    [SerializeField] private MoveObjectAlongPath[] objects;
    [SerializeField] private TargetObject[] targets;
    private int countDraw = 0;
    private int targetId;
    private int start_id;
    private int end_id;
    private bool canDraw = false;
    private bool canAddToList = false;
    private float intervalDistance = 0.4f; //interval distance that can be correct start points
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
            // Click, so start drawing a new line.
            Vector2 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            for (int i = 0; i < objects.Length; i++)
            {
                if (Mathf.Abs(newPoint.x - objects[i].transform.position.x) < intervalDistance &&
                    Mathf.Abs(newPoint.y - objects[i].transform.position.y) < intervalDistance)
                {
                    LinePrefab.startColor = objects[i].GetComponent<SpriteRenderer>().color;
                    LinePrefab.endColor = objects[i].GetComponent<SpriteRenderer>().color;
                    Debug.Log("inbound");
                    pathGameObject = null;
                    canDraw = true;
                    start_id = i + 1;
                }
            }
            Debug.Log(newPoint);
        }
        if (canDraw && Input.GetButton("Fire1"))
        {
            // Mouse is still down and we are dragging, so keep drawing.
            //if object haven't been went to target, draw
            if(end_id == 0)
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
            Debug.Log("end_id" + end_id);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            pathGameObject.SetId(start_id);
            Debug.Log("id path: " + pathGameObject.GetId());
            Debug.Log("id target: " + targetId);
            //check correct
            if (end_id == pathGameObject.GetId())
            {
               PathManager.Instance.AddPaths(pathGameObject);
            }
            else
            {
                Debug.Log("Vao day");
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

                    // Add the hit point to the path
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
}