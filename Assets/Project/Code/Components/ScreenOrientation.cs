using UnityEngine;
using Zenject;

public class ScreenOrientation : MonoBehaviour
{
    [SerializeField] private OrientationType screenOrientationType;
    
    [Inject]
    public void Construct(ScreenService service)
    {
        service.SetOrientation(screenOrientationType);
    }
}
