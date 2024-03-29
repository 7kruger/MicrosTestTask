﻿using Dropbox.Api.Files;
using Dropbox.Api;
using Microsoft.AspNetCore.Http;
using MicrosTestTask.BLL.Interfaces;

namespace MicrosTestTask.BLL.Services;

public class DropboxService : ICloudStorageService
{
	private const string notfound = "/images/person.svg";

	private const string refreshToken = "refresh_token";
	private const string appKey = "app_key";
	private const string appSecret = "app_secret";

	public async Task<string> UploadAsync(IFormFile image, string folder, string srcId)
	{
		try
		{
			if (image != null)
			{
				string extension = Path.GetExtension(image.FileName);
				if (extension == ".png" || extension == ".jpg")
				{
					return await UploadImage(image, folder, srcId);
				}
			}

			return notfound;
		}
		catch (Exception)
		{
			return notfound;
		}
	}

	public async Task<string> UpdateAsync(IFormFile image, string folder, string srcId)
	{
		try
		{
			string filename = $"img-{srcId}.jpg";

			var isExists = await IsExists(folder, filename);

			if (isExists)
			{
				await DeleteImage(folder, filename);
			}

			return await UploadImage(image, folder, srcId);
		}
		catch (Exception)
		{
			return notfound;
		}
	}

	public async Task DeleteAsync(string folder, string srcId)
	{
		try
		{
			string filename = $"img-{srcId}.jpg";
			await DeleteImage(folder, filename);
		}
		catch (Exception)
		{

		}
	}

	private async Task<string> UploadImage(IFormFile image, string folder, string srcId)
	{
		using (var dbx = new DropboxClient(refreshToken, appKey, appSecret))
		{
			string filename = $"img-{srcId}.jpg";
			using (var mem = image.OpenReadStream())
			{
				await dbx.Files.UploadAsync($"/micros/{folder}/{filename}", WriteMode.Overwrite.Instance, body: mem);
				var result = await dbx.Sharing.CreateSharedLinkWithSettingsAsync($"/micros/{folder}/{filename}");
				return result.Url.Replace("dl=0", "raw=1");
			}
		}
	}

	private async Task DeleteImage(string folder, string filename)
	{
		using (var dbx = new DropboxClient(refreshToken, appKey, appSecret))
		{
			await dbx.Files.DeleteAsync($"/micros/{folder}/{filename}");
		}
	}

	private async Task<bool> IsExists(string folder, string filename)
	{
		try
		{
			using (var dbx = new DropboxClient(refreshToken, appKey, appSecret))
			{
				var response = await dbx.Files.ListFolderAsync($"/micros/{folder}");

				var result = response.Entries.FirstOrDefault(i => i.Name == filename);

				return result == null ? false : true;
			}
		}
		catch (Exception)
		{
			return false;
		}
	}
}
