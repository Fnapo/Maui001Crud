using CommunityToolkit.Mvvm.Input;
using Maui001Crud.Models;

namespace Maui001Crud.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}