using UnderstandingStructureApp.Models;
using UnderstandingStructureApp.Models.ViewModel;

namespace UnderstandingStructureApp.Interface
{
    public interface IPizzaService
    {
        List<PizzaImageViewModel> GetAllPizzas();
        List<PizzaImageViewModel> GetPizzaByID(int pid);
    }
}   
