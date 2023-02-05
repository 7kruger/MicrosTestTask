using MicrosTestTask.BLL.Models;

namespace MicrosTestTask.BLL.Interfaces;

public interface IOperationService
{
	IEnumerable<OperationModel> GetAll(string username);
	Task<bool> Create(OperationModel model, string username);
	Task Update(OperationModel model);
	Task Delete(int id);
}
