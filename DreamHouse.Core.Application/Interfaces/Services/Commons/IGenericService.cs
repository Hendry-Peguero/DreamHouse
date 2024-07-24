using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.Core.Application.Interfaces.Services.Commons
{
    public interface IGenericService<SaveViewModel, ViewModel, Entity>
        where SaveViewModel : class
        where ViewModel : class
        where Entity : class
    {
        Task<SaveViewModel?> AddAsync(SaveViewModel svm);
        Task DeleteAsync(int entityId);
        Task<List<ViewModel>> GetAllAsync();
        Task<SaveViewModel> GetByIdAsync(int entityId);
        Task<SaveViewModel?> UpdateAsync(SaveViewModel svm, int svmId);
    }
}
