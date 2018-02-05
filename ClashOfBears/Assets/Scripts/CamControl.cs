
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamControl : MonoBehaviour {

    public float minZoom = 8;
    public float maxZoom = 24;
    public float zoomSpeed = 5;
    public float panSpeed = 1;
    public float rotateSpeed = 3;

    private Collider[] nearObjects;

    private Camera cam;

    private float curZoom = 16;

	void Start () {
        cam = Camera.main;
	}
	

	void Update () {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //RightClick Pan

        if (Input.GetMouseButton(1))
        {
            transform.Translate(-mouseX * panSpeed, 0, -mouseY * panSpeed);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit ;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject.name);
                nearObjects = Physics.OverlapSphere(hit.point, 10);
            }

            foreach (Collider pawn in nearObjects)
            {
                pawn.SendMessage("IncreaseSpeed");
            }

        }

        //Mid-Click rotate
        if (Input.GetMouseButton(2))
        {
            transform.Rotate(Vector3.up, mouseX * rotateSpeed);
        }

        //Zoom with scroll
        float scrollW = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        curZoom = Mathf.Clamp(curZoom - scrollW, minZoom, maxZoom);
        cam.orthographicSize = curZoom;
        

        //LeftClick to spawn BaseObject
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) //If i am clicking on a UI element
                return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                SpawnObject(hit.point);
        }
	}

    private void SpawnObject(Vector3 point)
    {
        //TODO: Update to select based on game mode
        BaseObject prefab = Resources.Load<BaseObject>((Globals.isAttacking ? "Units/" : "Buildings/") + Globals.objectToSpawn);
        BaseObject clone = Instantiate(prefab, point, Quaternion.identity);
    }
}
