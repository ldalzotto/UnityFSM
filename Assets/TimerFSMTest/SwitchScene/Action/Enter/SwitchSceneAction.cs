
using UnityEngine.SceneManagement;

public class SwitchSceneAction : FromChallenge.FSMAction
{
    public override void ExecuteAction()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
