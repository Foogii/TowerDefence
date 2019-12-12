using UnityEngine;

public static class UtilityMethods
{

    public static void MoveUiElementToWorldPosition(RectTransform rectTransform, Vector3 worldPosition)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Game")
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPosition);
            rectTransform.position = screenPoint;
        }


        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameVR")
        {
            //1 
            rectTransform.position = worldPosition + new Vector3(0, 3);
            //2 // Needed to rotate UI the right way 
            rectTransform.LookAt(Camera.main.transform.position + Camera.main.transform.forward * 10000);
            //3 
            ScaleRectTransformBasedOnDistanceFromCamera(rectTransform);
        }
    }

    // 1
    public static Quaternion SmoothlyLook(Transform fromTransform,
     Vector3 toVector3)
    {

        //2
        if (fromTransform.position == toVector3)
        {
            return fromTransform.localRotation;
        }
        //3
        Quaternion currentRotation = fromTransform.localRotation;
        Quaternion targetRotation = Quaternion.LookRotation(toVector3 -
        fromTransform.position);
        //4
        return Quaternion.Slerp(currentRotation, targetRotation,
        Time.deltaTime * 10f);
    }

    private static void ScaleRectTransformBasedOnDistanceFromCamera(RectTransform rectTransform)
    {
        float distance = Vector3.Distance(Camera.main.transform.position, rectTransform.position);
        rectTransform.localScale = new Vector3(distance / UIManager.vrUiScaleDivider, distance / UIManager.vrUiScaleDivider, 1f);

    }
}