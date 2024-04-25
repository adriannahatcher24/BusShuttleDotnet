using DomainModel;
namespace WebMvc.Service;

public interface LoopServiceInterface
{
    List<LoopModel> GetLoops();
    void UpdateLoopByID(int id, string name);
    void CreateLoop(string name);
    LoopModel? FindLoopByID(int id);
    void DeleteLoop(int id);
}