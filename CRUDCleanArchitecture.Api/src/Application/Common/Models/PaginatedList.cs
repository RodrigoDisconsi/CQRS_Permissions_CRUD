using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace CRUDCleanArchitecture.Application.Common.Models;
public class PaginatedList<T>
{
    public List<T> Items { get; private set; }
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }
    public int TotalCount { get; private set; }
    public PaginatedList() { }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }

    public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }

    public PaginatedList<U> Map<U>() where U : class
    {
        IMapper _mapper = new Mapper(new MapperConfiguration(config => config.CreateMap<T, U>()));
        PaginatedList<U> result = new();
        result.Items = _mapper.Map<List<U>>(this.Items);
        result.TotalPages = this.TotalPages;
        result.PageIndex = this.PageIndex;
        result.TotalCount = this.TotalCount;

        return result;
    }

    public PaginatedList<U> Map<U>(IConfigurationProvider config) where U : class
    {
        PaginatedList<U> result = new();
        result.Items = this.Items.AsQueryable().ProjectTo<U>(config).ToList();
        result.TotalPages = this.TotalPages;
        result.PageIndex = this.PageIndex;
        result.TotalCount = this.TotalCount;

        return result;
    }
}