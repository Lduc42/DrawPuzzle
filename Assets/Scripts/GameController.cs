using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField]
    private LineRenderer LinePrefab;
    public PathGameObject pathGameObject;
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
            // Mouse is still down and we are dragging, so keep drawing.
            Draw(Input.mousePosition);
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
                // TODO: Check distance between points. This just adds all of them, even
                // if you hold the mouse still.
                Vector3 hitpoint = ray.GetPoint(distance);
                pathGameObject.AddPosition(hitpoint);
            }
        }
    }
    public MoveObjectAlongPath GetObject(int index)
    {
        return objects[index];
    }
}