using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemImg : MonoBehaviour
{
    public static ItemImg dragingImg;
    public Vector3 lastPosition, actualPosition;

    private void OnEnable()
    {
        lastPosition = transform.position;
    }

    public void FollowCursor()
    {
        dragingImg = this;
        actualPosition = gameObject.transform.position = Input.mousePosition;
    }


}
