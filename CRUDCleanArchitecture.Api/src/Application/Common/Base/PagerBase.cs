namespace CRUDCleanArchitecture.Application.Common.Base;
public class PagerBase
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public OrderCriteria? OrderCriteria { get; set; }
    public PagerBase()
    {
        OrderCriteria = new OrderCriteria();
    }
}

public class OrderCriteria
{
    public string? Column { get; set; }
    public bool Ascending { get; set; }
}
