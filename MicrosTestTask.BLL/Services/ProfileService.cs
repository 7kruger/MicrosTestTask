using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MicrosTestTask.BLL.Interfaces;
using MicrosTestTask.BLL.Models;
using MicrosTestTask.DAL.Entities;
using MicrosTestTask.DAL.Interfaces;
using MicrosTestTask.Domain.Enums;

namespace MicrosTestTask.BLL.Services;

public class ProfileService : IProfileService
{
    private readonly IRepository<Operation> _operationRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Profile> _profileRepository;
    private readonly ICloudStorageService _cloudStorageService;

    public ProfileService(IRepository<Operation> operationRepository,
                          IRepository<User> userRepository,
                          IRepository<Profile> profileRepository,
                          ICloudStorageService cloudStorageService)
    {
        _operationRepository = operationRepository;
        _userRepository = userRepository;
        _profileRepository = profileRepository;
        _cloudStorageService = cloudStorageService;
    }

    public async Task<ProfileModel> Get(string username)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            var profile = await _profileRepository.GetAll().FirstOrDefaultAsync(x => x.User.Name == username);

            var operations = await _operationRepository.GetAll()
                .Where(x => x.User.Name == username)
                .ToListAsync();

            var allTimeIncome = operations.Where(x => x.Category.CategoryType == CategoryType.Income).Sum(x => x.Sum);
            var allTimeExpense = operations.Where(x => x.Category.CategoryType == CategoryType.Expense).Sum(x => x.Sum);

            return new ProfileModel
            {
                Username = profile.User.Name,
                ImgRef = profile.ImgRef,
                AllTimeIncome = allTimeIncome,
                AllTimeExpense = allTimeExpense,
                Difference = allTimeIncome - allTimeExpense
            };
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task Create(string username)
    {
        var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == username);

        if (user != null)
        {
            var profile = new Profile
            {
                ImgRef = "/images/person.svg",
                UserId = user.Id
            };

            await _profileRepository.CreateAsync(profile);
        }
    }

    public async Task<bool> Update(string username, IFormFile image)
    {
        try
        {
            var profile = await _profileRepository.GetAll().FirstOrDefaultAsync(x => x.User.Name == username);

            if (profile == null)
            {
                return false;
            }

            if (image != null)
            {
                profile.ImgRef = await _cloudStorageService.UpdateAsync(image, "profiles", profile.Id.ToString());
                await _profileRepository.UpdateAsync(profile);
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
