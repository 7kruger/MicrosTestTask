﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosTestTask.BLL.Interfaces;
using MicrosTestTask.BLL.Models;
using MicrosTestTask.Models.Enums;
using MicrosTestTask.Models.Shared;
using MicrosTestTask.ViewModels.Admin;

namespace MicrosTestTask.Controllers
{
	[Authorize(Roles = "admin")]
	public class AdminController : Controller
	{
		private readonly IUserService _userService;
		private readonly ICategoryService _categoryService;

		public AdminController(IUserService adminService, ICategoryService categoryService)
		{
			_userService = adminService;
			_categoryService = categoryService;
		}

		[HttpGet]
		public IActionResult Index() => View();

		[HttpGet]
		public async Task<IActionResult> Users(int? pageId)
		{
			var page = pageId ?? 1;
			var pageSize = 10;

			var users = (await _userService.GetUsers())
				.Skip(1)
				.Select(x => new UserViewModel
				{
					Id = x.Id,
					Name = x.Name,
					Role = x.Role,
					RegistrationDate = x.RegistrationDate,
					IsBlocked = x.IsBlocked,
				});

			var listOfUsersViewModel = new ListOfUsersViewModel();
			listOfUsersViewModel.Users = users.Skip((page - 1) * pageSize).Take(pageSize);
			listOfUsersViewModel.Pagination = new Pagination(users.Count(), page, pageSize);

			return View(listOfUsersViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Users(ActionType type, IEnumerable<int> selectedUsers)
		{
			switch (type)
			{
				case ActionType.Unlock:
					await _userService.Unlock(selectedUsers);
					break;
				case ActionType.Block:
					await _userService.Block(selectedUsers);
					break;
				case ActionType.AddToAdmin:
					await _userService.AddToAdmin(selectedUsers);
					break;
				case ActionType.RemoveFromAdmin:
					await _userService.RemoveFromAdmin(selectedUsers);
					break;
				case ActionType.Delete:
					await _userService.Delete(selectedUsers);
					break;
			}

			return RedirectToAction("Users");
		}

		[HttpGet]
		public async Task<IActionResult> Categories(int? pageId)
		{
			var page = pageId ?? 1;
			var pageSize = 5;

			var categories = (await _categoryService.GetCategories())
				.Select(x => new CategoryViewModel { Id = x.Id, Name = x.Name, })
				.Skip(2); // пропускаем первые категории, чтобы категории 'другое' и 'иные доходы' нельзя было удалить или изменить

			var listOfCategoriesViewModel = new ListOfCategoriesViewModel();
			listOfCategoriesViewModel.Categories = categories.Skip((page - 1) * pageSize).Take(pageSize);
			listOfCategoriesViewModel.Pagination = new Pagination(categories.Count(), page, pageSize);

			return View(listOfCategoriesViewModel);
		}

        [HttpGet]
        [Route("[controller]/categories/get")]
        public async Task<IActionResult> Get(int? id)
        {
			if (id == null)
			{
				return BadRequest();
			}

            var category = (await _categoryService.GetCategories()).FirstOrDefault(x => x.Id == id);

            if (category != null)
            {
                return Ok(category);
            }

            return BadRequest("Категория не найдена");
        }

        [HttpPost]
		[Route("[controller]/categories/create")]
		public async Task<IActionResult> Create(CategoryViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var exists = await _categoryService.CategoryExists(model.Name);
			if (exists)
			{
				return BadRequest("Категория уже существует");
			}

			var created = await _categoryService.CreateAsync(new CategoryModel { Name = model.Name, CategoryType = model.CategoryType });

			if (created)
			{
				return Ok();
			}

			return BadRequest("Произошла ошибка при создании категории");
		}

        [HttpPost]
		[Route("[controller]/categories/update")]
		public async Task<IActionResult> Update(CategoryViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var updated = await _categoryService.UpdateAsync(new CategoryModel 
			{ 
				Id = model.Id, 
				Name = model.Name, 
				CategoryType = model.CategoryType 
			});

			if (updated)
			{
				return Ok();
			}

			return BadRequest("Не удалось обновить категорию");
		}

		[HttpPost]
		[Route("[controller]/categories/delete")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return BadRequest("Неверный id категории");
			}

			var deleted = await _categoryService.DeleteAsync((int)id);

			if (deleted)
			{
				return Ok();
			}

			return BadRequest("Не удалось удалить категорию");
		}
	}
}
