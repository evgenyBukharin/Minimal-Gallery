using UnityEngine;

public class ScreenService : MonoBehaviour
{
    public void SetOrientation(OrientationType orientationType)
    {
        switch (orientationType)
        {
            case OrientationType.Portrait:
                Screen.orientation = UnityEngine.ScreenOrientation.Portrait;
                break;
            
            default:
                Debug.Log("Auto rotation");
                Screen.orientation = UnityEngine.ScreenOrientation.AutoRotation;
                break;
        }
    }
}
