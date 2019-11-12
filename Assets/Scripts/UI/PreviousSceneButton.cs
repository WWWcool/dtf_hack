using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousSceneButton : MonoBehaviour
{
    public void OnClick()
    {
        ServiceLocator.Get<SceneListManager>().LoadPreviousScene();
    }
}
