using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneButton : MonoBehaviour
{
    public void OnClick()
    {
        ServiceLocator.Get<SceneListManager>().LoadNextScene();
    }
}
