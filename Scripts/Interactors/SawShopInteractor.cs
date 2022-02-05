using UnityEngine;

public class SawShopInteractor : Interactor
{
    public int CurrentIndex => repository.CurrentIndex;

    private SawShopRepository repository;


    public override void Initialize()
    {
        repository = Game.GetRepository<SawShopRepository>();
    }

    public void SetSaw(int index)
    {
        repository.CurrentIndex = index;
        Game.GetInteractor<PlayerInteractor>().Saw.GetComponent<SpriteRenderer>().sprite = repository.sawSprites[index];
    }
}
