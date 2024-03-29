﻿namespace MicrosTestTask.Models.Shared;

public class Pagination
{
	public int PageNumber { get; private set; }
	public int TotalPages { get; private set; }

	public Pagination(int count, int pageNumber, int pageSize)
	{
		PageNumber = pageNumber;
		TotalPages = (int)Math.Ceiling(count / (double)pageSize);
	}

	public bool HasPreviousPage => PageNumber > 1;

	public bool HasNextPage => PageNumber < TotalPages;
}
