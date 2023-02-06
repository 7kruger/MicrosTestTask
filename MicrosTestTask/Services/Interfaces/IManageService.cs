using MicrosTestTask.DAL.Enums;
using MicrosTestTask.ViewModels.Manage;

namespace MicrosTestTask.Services.Interfaces;

public interface IManageService
{
	Task<HistoryViewModel> GetHistoryViewModel(string username, DateTime? startDate, DateTime? endDate, CategoryType? categoryType);
	Task<StatisticsViewModel> GetStatisticsViewModel(string username, int? month, int? year);
}
