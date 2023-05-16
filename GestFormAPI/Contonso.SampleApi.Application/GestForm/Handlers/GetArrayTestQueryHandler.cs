namespace Contonso.SampleApi.Application.GestForm.Handlers;

using AutoMapper;
using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Application.GestForm.Queries;
using MediatR;

public class GetArrayTestQueryHandler : IRequestHandler<TestArrayQuery, IList<TestNumberQueryResult>>
{
    private readonly IAppDbContext dbContext;

    private readonly IMapper mapper;

    public GetArrayTestQueryHandler(IAppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<IList<TestNumberQueryResult>> Handle(TestArrayQuery request, CancellationToken cancellationToken)
    {
        IList<TestNumberQueryResult> result = new List<TestNumberQueryResult>();

        foreach (var item in request.Items)
        {
            result.Add(new TestNumberQueryResult
            {
                Number = item,
                Result = TestGestForm(item),
            });
        }

        return Task.FromResult(result);
    }

    private static string TestGestForm(short n)
    {
        if (EstDivisiblePar5(n) && EstDivisiblePar3(n))
            return "Gestform";

        if (EstDivisiblePar3(n))
            return "Geste";

        if (EstDivisiblePar5(n))
            return "Forme";

        return n.ToString();
    }

    private static bool EstDivisiblePar3(short n) => n % 3 == 0;

    private static bool EstDivisiblePar5(short n) => n % 5 == 0;

}
