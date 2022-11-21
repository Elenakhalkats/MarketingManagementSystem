using static MarketingManagementSystem.Application.Exceptions.AppException;

namespace MarketingManagementSystem.Application.Exceptions;

public class DistributorNotFoundException : AppNotFoundException
{
    public DistributorNotFoundException()
        : base("Distributor Not Found!")
    {

    }
}
public class AlreadyRecommendedDistributorException : AppDeniedException
{
    public AlreadyRecommendedDistributorException()
        : base("This distributor already has a recommendator!")
    {

    }
}
public class HasNotRecommendAccessException : AppDeniedException
{
    public HasNotRecommendAccessException()
        : base("This distributor doesn't have recommend access!")
    {

    }
}
