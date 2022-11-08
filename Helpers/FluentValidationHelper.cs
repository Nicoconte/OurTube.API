using GreenDonut;

namespace OurTube.API.Helpers
{
    public class FluentValidationHelper
    {
        public static String FlatErrors(List<FluentValidation.Results.ValidationFailure> errors)
        {
            return String.Join(" / ", errors);
        }

        public static void RaiseGraphQLException(List<FluentValidation.Results.ValidationFailure> errors)
        {
            throw new GraphQLException(new Error(FluentValidationHelper.FlatErrors(errors)));
        }

        public static void RaiseGraphQLException(string error)
        {
            throw new GraphQLException(new Error(error));
        }
    }
}
