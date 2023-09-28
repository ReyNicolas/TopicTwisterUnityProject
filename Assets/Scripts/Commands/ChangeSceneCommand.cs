using UnityEngine.SceneManagement;

namespace Commands
{
    public class ChangeSceneCommand : ICommand
    {
        private readonly string sceneName;
        public ChangeSceneCommand(string sceneName)
        {
            this.sceneName = sceneName;
        }
        

        public void Execute()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}