using BuzzUp_API.Application.DTO.Users;

namespace BuzzUp_API.Implementation.UseCases.Queries.Users
{
    public static class UserPrivacy
    {
        public static void HidePrivateDetails(UserMiniDTO dto)
        {
            dto.Email = null;
            dto.CountryId = null;
            dto.CountryName = null;
            dto.City = null;
            dto.Workplace = null;
            dto.University = null;
            dto.Website = null;
            dto.Bio = null;
            dto.DateOfBirth = null;
        }
    }
}
