﻿using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MicrosTestTask.Domain.Helpers;
using MicrosTestTask.BLL.Interfaces;
using MicrosTestTask.BLL.Models;
using MicrosTestTask.DAL.Entities;
using MicrosTestTask.DAL.Interfaces;
using MicrosTestTask.Domain.Enums;

namespace MicrosTestTask.BLL.Services;

public class AccountService : IAccountService
{
    private readonly IRepository<User> _userRepository;
    private readonly IProfileService _profileService;

    public AccountService(IRepository<User> userRepository, IProfileService profileService)
    {
        _userRepository = userRepository;
        _profileService = profileService;
    }

    public async Task<IdentityResult> Register(UserModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);

            if (user != null)
            {
                return new IdentityResult(
                    errors: new List<string>
                    {
                        "Пользователь с таким именем уже существует"
                    },
                    succeeded: false,
                    claims: null);
            }

            user = new User
            {
                Name = model.Name,
                Password = PasswordHashHelper.HashPassword(model.Password),
                Role = Role.User,
                IsBlocked = false,
                RegistrationDate = DateTime.Now,
            };

            await _userRepository.CreateAsync(user);
            await _profileService.Create(user.Name);

            var claims = GetClaimsIdentity(user);
            return new IdentityResult(
                    errors: new List<string>(),
                    succeeded: true,
                    claims: claims);
        }
        catch (Exception)
        {
            return new IdentityResult(
                    errors: new List<string>
                    {
                        "Не удалось зарегистрировать пользователя, попробуйте позже"
                    },
                    succeeded: false,
                    claims: null);
        }
    }

    public async Task<IdentityResult> Authenticate(UserModel model)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);

            if (user == null)
            {
                return new IdentityResult(
                    errors: new List<string>
                    {
                        "Пользователя с таким именем не существует"
                    },
                    succeeded: false,
                    claims: null);
            }
            else if (user.Password != PasswordHashHelper.HashPassword(model.Password))
            {
                return new IdentityResult(
                    errors: new List<string>
                    {
                        "Неверный логин или пароль"
                    },
                    succeeded: false,
                    claims: null);
            }
            else if (user.IsBlocked)
            {
                return new IdentityResult(
                    errors: new List<string>
                    {
                        "Пользователь заблокирован"
                    },
                    succeeded: false,
                    claims: null);
            }

            var claims = GetClaimsIdentity(user);
            return new IdentityResult(
                    errors: new List<string>(),
                    succeeded: true,
                    claims: claims); ;
        }
        catch (Exception)
        {
            return new IdentityResult(
                    errors: new List<string>
                    {
                        "Не удалось войти в аккаунт, попробуйте позже"
                    },
                    succeeded: false,
                    claims: null);
        }
    }

    public async Task<bool> ChangePassword(string username, string newPassword)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == username);

            if (user == null)
            {
                return false;
            }

            user.Password = PasswordHashHelper.HashPassword(newPassword);

            await _userRepository.UpdateAsync(user);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private ClaimsIdentity GetClaimsIdentity(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString().ToLower())
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}
