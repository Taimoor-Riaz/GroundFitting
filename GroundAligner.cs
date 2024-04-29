using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAligner : MonoBehaviour
{
    [Header("Fitter")]
    public Vector3 raycastOffset;
    public LayerMask layerMask;

    private bool positionSet = false;

    private void LateUpdate()
    {
        AlignToGround();
    }
    private void AlignToGround()
    {
        Ray ray = new Ray(transform.position + raycastOffset, -transform.up);
        if (Physics.Raycast(ray, out RaycastHit hit, 3f, layerMask))
        {
            var slopeRotation = Quaternion.FromToRotation(transform.up, hit.normal);
            transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation * transform.rotation, 10 * Time.deltaTime);
            positionSet = false;
        }
        else
        {
            if (!positionSet)
            {
                transform.localRotation = Quaternion.identity;
                positionSet = true;
            }
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 10 * Time.deltaTime);
        }
    }
    public void ResetRotation()
    {
        transform.localRotation = Quaternion.identity;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + raycastOffset, (transform.position + raycastOffset) - new Vector3(0, 2, 0));
    }
}
