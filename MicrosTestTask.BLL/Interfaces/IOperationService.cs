﻿using MicrosTestTask.BLL.Models;

namespace MicrosTestTask.BLL.Interfaces;

public interface IOperationService
{
	Task<IEnumerable<OperationModel>> GetAll();
	Task<bool> Create(OperationModel model, string username);
	Task Update(OperationModel model);
	Task Delete(int id);
}
