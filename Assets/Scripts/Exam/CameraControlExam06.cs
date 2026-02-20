using UnityEngine;
 
public class CameraControlExam06 : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public float offset;
    public Camera targetCamera;
 
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 player1Pos = player1.transform.position;
        Vector3 player2Pos = player2.transform.position;
 
        // Student code ...
 
Vector3 middlePoint = (player1Pos + player2Pos) * 0.5f;
 
targetCamera.transform.position = middlePoint + new Vector3(0,40, offset);
 
if (targetCamera != null )
{
     if (targetCamera.orthographic)
     {
         float distance = Vector3.Distance(player1Pos, player2Pos);
         float aspectRatio = targetCamera.aspect;
         targetCamera.orthographicSize = Mathf.Max(distance * 0.5f, distance / (2 * aspectRatio)) + 10f;
     }
     else
     {
         float distance = Vector3.Distance(player1Pos, player2Pos);
         targetCamera.fieldOfView = Mathf.Lerp(60f, 90f, distance / 20f);
     }
}
 
       
    }
}
 
 