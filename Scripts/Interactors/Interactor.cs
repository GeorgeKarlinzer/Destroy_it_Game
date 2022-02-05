public abstract class Interactor
{
    public virtual void OnCreate() { } // After all repos and interactors were created

    public virtual void Initialize() { } // After all repos and interactors were onCreated

    public virtual void OnStart() { } // After all repos and interactors were initialized
}
