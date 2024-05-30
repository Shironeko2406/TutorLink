using System.Collections.Generic;
using DataLayer.Entities;

namespace TutorLinkAPI.BusinessLogics.IServices
{
    public interface IPostRequestService
    {
        List<PostRequest> GetAllPostRequests();
        PostRequest GetPostRequestById(Guid postrequestId);
        void CreatePostRequest(PostRequest postRequest);
        void UpdatePostRequest(PostRequest postRequest);
        void DeletePostRequest(Guid postrequestId);
    }
}

}
