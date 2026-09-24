using BuzzUp_API.Domain;

namespace BuzzUp_API.Implementation.UseCases
{
    public static class RegisteredUserUseCases
    {
        public static readonly int[] Ids =
        {
            4, 5, 6, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24
        };

        public static List<UserUseCase> Create()
        {
            return Ids.Select(id => new UserUseCase { UseCaseId = id }).ToList();
        }
    }
}
