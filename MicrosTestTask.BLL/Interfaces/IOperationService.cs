using MicrosTestTask.BLL.Models;

namespace MicrosTestTask.BLL.Interfaces;

public interface IOperationService
{
	Task<IEnumerable<OperationModel>> GetAll();
	Task<bool> Create(OperationModel model, string username);
	Task<bool> Update(OperationModel model);
	Task<bool> Delete(int id);
}
