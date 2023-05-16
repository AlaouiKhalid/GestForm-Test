namespace Contonso.SampleApi.Application.GestForm.Queries
{
    using MediatR;

    public class TestArrayQuery : IRequest<IList<TestNumberQueryResult>>
    {
        public IList<short> Items { get; set; }

        public TestArrayQuery(IList<short> list)
        {
            var invalidNumbers = list.Where(x => Math.Abs(x) > 1000).ToList();

            if (invalidNumbers.Any())
            {
                throw new ArgumentException("the following Array contains invalid numbers : [ " + string.Join(", ", invalidNumbers) + "]");
            }

            this.Items = list;
        }
    }
}
