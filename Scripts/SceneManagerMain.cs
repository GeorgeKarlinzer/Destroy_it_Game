using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public sealed class SceneManagerMain : SceneManagerBase
{
    protected override void InitScenesMap()
    {
        this.sceneConfigMap[SceneConfigMain.SCENE_NAME] = new SceneConfigMain();
    }
}
