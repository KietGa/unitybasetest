using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float newScaleZ = 4f;
    [SerializeField] private float expandSpeed = 2f;

    private MeshFilter meshFilter;
    private Vector3 anchor;
    private bool isExpanding;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();

        anchor = new Vector3(
            0, 0, meshFilter.sharedMesh.bounds.min.z
        );
    }

    private void Update()
    {
        if (isExpanding)
        {
            Expand();
        }
    }

    public void StartExpand()
    {
        isExpanding = true;
    }

    private void Expand()
    {
        Vector3 oldWorldPos = transform.TransformPoint(anchor);

        Vector3 scale = transform.localScale;

        scale.z = Mathf.MoveTowards(
            scale.z,
            newScaleZ,
            expandSpeed * Time.deltaTime
        );

        transform.localScale = scale;

        Vector3 newWorldPos = transform.TransformPoint(anchor);

        transform.position += oldWorldPos - newWorldPos;

        if (Mathf.Approximately(scale.z, newScaleZ))
        {
            isExpanding = false;
        }
    }
}