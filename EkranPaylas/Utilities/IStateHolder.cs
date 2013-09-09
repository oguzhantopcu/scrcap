using EkranPaylas.ViewModels;

namespace EkranPaylas.Utilities
{
    public interface IStateHolder
    {
        ScreenGrabberState State { get; }
    }
}