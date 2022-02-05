using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SceneConfigMain : SceneConfig
{
    public const string SCENE_NAME = "Main";

    public override string SceneName => SCENE_NAME;

    public override Dictionary<Type, Interactor> CreateAllInteractors()
    {
        var interactorsMap = new Dictionary<Type, Interactor>();

        CreateInteractor<CoinsInteractor>(interactorsMap);
        CreateInteractor<CrystalsInteractor>(interactorsMap);
        CreateInteractor<LevelInteractor>(interactorsMap);
        CreateInteractor<PlayerInteractor>(interactorsMap);
        CreateInteractor<CubesHitsHandler>(interactorsMap);
        CreateInteractor<BonusCubeInteractor>(interactorsMap);
        CreateInteractor<MonoPool>(interactorsMap);
        CreateInteractor<SpawnSystem>(interactorsMap);
        CreateInteractor<SaveSystem>(interactorsMap);
        CreateInteractor<UpgradesInteractor>(interactorsMap);
        CreateInteractor<BoxShopInteractor>(interactorsMap);
        CreateInteractor<SawShopInteractor>(interactorsMap);
        CreateInteractor<AchievementInteractor>(interactorsMap);

        return interactorsMap;
    }

    public override Dictionary<Type, Repository> CreateAllRepositories()
    {
        var repositoriesMap = new Dictionary<Type, Repository>();

        CreateRepository<CoinsRepository>(repositoriesMap);
        CreateRepository<CrystalsRepository>(repositoriesMap);
        CreateRepository<LevelRepository>(repositoriesMap);
        CreateRepository<PlayerRepository>(repositoriesMap);
        CreateRepository<UpgradesRepository>(repositoriesMap);
        CreateRepository<BoxShopRepository>(repositoriesMap);
        CreateRepository<BonusCubeRepository>(repositoriesMap);
        CreateRepository<SawShopRepository>(repositoriesMap);
        CreateRepository<AchievementRepository>(repositoriesMap);

        return repositoriesMap;
    }
}
