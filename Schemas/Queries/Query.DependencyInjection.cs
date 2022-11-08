using OurTube.API.Validators;

namespace OurTube.API.Schemas.Queries
{
    public partial class Query
    {
        private QueryValidators _validators;

        public Query(QueryValidators queryValidators)
        {
            _validators = queryValidators;
        }
    }
}
