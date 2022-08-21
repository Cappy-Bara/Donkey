using Donkey.Core.Repositories;
using Donkey.Core.ValueObjects.Accounts;
using MediatR;

namespace Donkey.Core.Actions.Queries.Account
{
    public record GetUserBasicData(string Email) : IRequest<UserBasicData>;

    public class GetBasicDataHandler : IRequestHandler<GetUserBasicData, UserBasicData>
    {
        private readonly IUsersRepository _usersRepo;

        public GetBasicDataHandler(IUsersRepository usersRepo)
        {
            _usersRepo = usersRepo;
        }

        async Task<UserBasicData> IRequestHandler<GetUserBasicData, UserBasicData>.Handle(GetUserBasicData request, CancellationToken cancellationToken)
        {
            var user = await _usersRepo.Get(request.Email);

            return new UserBasicData() { Email = user.Email };
        }
    }
}
