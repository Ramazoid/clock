using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public Transform marker;
    public Camera cam;
    [Range(0, 1)]
    public float corrector;
    public ARotate MArrow, HArrow;
    private RaycastHit hit;
    private Ray ray;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Clock.editMode) return;

        if (Input.GetMouseButton(0))
        {
            if (CheckHit("ArrowH") && !MArrow.rot)
            {
                HArrow.transform.SetAsLastSibling();
                if (CheckHit("Back"))
                {
                    HArrow.rot = true;
                    MArrow.rot = false;
                    marker.position = hit.point + Vector3.up * corrector;
                }
            } else HArrow.rot = false;
            if (CheckHit("ArrowM")&& !HArrow.rot)
            {
                MArrow.transform.SetAsLastSibling();
                if (CheckHit("Back"))
                {
                    MArrow.rot = true;
                    HArrow.rot = false;
                    marker.position = hit.point + Vector3.up * corrector;
                }
            } else MArrow.rot = false;
        }
    }
    public bool CheckHit(string slayer)
    {
        
        ray = cam.ScreenPointToRay(Input.mousePosition);
        int ilayer = 1 << LayerMask.NameToLayer(slayer);

        return Physics.Raycast(ray, out hit, float.PositiveInfinity, ilayer);
    }
}
