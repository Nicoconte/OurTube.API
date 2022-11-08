using OurTube.API.Validators;

namespace OurTube.API.Schemas.Mutations
{
    public partial class Mutation
    {
        private MutationValidators _validators;

        public Mutation(MutationValidators mutationValidators)
        {
            _validators = mutationValidators;
        }
    }
}
