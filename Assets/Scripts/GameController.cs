using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public float detectionDistance = .2f;
    [SerializeField]
    private LineRenderer LinePrefab;
    private PathGameObject pathGameObject;
    [SerializeField] private MoveObjectAlongPath[] objects;
    private int countDraw = 0; 

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
            pathGameObject = null;
        }
        if (Input.GetButton("Fire1"))
        {
           
               Draw(Input.mousePosition);
            
                // Mouse is still down and we are dragging, so keep drawing.
                
        }
        if (Input.GetButtonUp("Fire1"))
        {
            
            PathManager.Instance.AddPaths(pathGameObject);
            countDraw++; 
            Debug.Log("draw = " + countDraw);
            if (countDraw == 2)
            {
               
            }
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

                // Check if the hit point is close to any of the objects
                foreach (MoveObjectAlongPath obj in objects)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(hitpoint, obj.transform.position - hitpoint, out hit))
                    {
                        if (hit.distance <= detectionDistance)
                        {
                            // The hit point is close to the object, so add it to the path
                            pathGameObject.AddPosition(obj.transform.position);
                            break;
                        }
                    }
                }

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