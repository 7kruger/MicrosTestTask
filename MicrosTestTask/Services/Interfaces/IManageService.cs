using MicrosTestTask.DAL.Enums;
using MicrosTestTask.ViewModels.Manage;

namespace MicrosTestTask.Services.Interfaces;

public interface IManageService
{
	HistoryViewModel GetHistoryViewModel(string username, DateTime? startDate, DateTime? endDate, CategoryType? categoryType);
}
